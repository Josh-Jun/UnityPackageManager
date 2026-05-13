Shader "UI/RoundedRectangleBlur"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        // 圆角矩形属性
        _Radius ("Corner Radius", Range(0, 0.5)) = 0.1
        _Size ("Size", Vector) = (100, 100, 0, 0)

        // 毛玻璃效果开关及强度
        [Toggle(USEBLUR)] _UseBlur ("Use Blur", Float) = 0
        _ColorPower ("Color Power", Float) = 1

        // UI 模板属性
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP
            #pragma shader_feature USEBLUR

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                float4 screenPos : TEXCOORD2;   // 用于采样模糊纹理的屏幕坐标
                float2 localPos : TEXCOORD3;    // 用于圆角 SDF 计算
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            sampler2D _GlobalFullScreenBlurTexture; // 全局模糊纹理，通过脚本设置
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _Radius;
            float2 _Size;
            float _ColorPower;

            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

                OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                // 与圆角矩形 shader 保持一致：顶点色伽马校正后乘以 Tint
                OUT.color = pow(v.color, 2.2) * _Color;

                // 屏幕坐标（用于毛玻璃）
                OUT.screenPos = ComputeScreenPos(OUT.vertex);

                // 局部坐标（用于圆角计算）
                OUT.localPos = (v.texcoord - 0.5) * _Size;

                return OUT;
            }

            // 圆角矩形 SDF 函数，与原始圆角 shader 逻辑完全相同
            float roundedRectSDF(float2 pos, float2 size, float radius)
            {
                float2 absPos = abs(pos);
                float2 d = absPos - size * 0.5;
                d = d + radius;
                float2 maxD = max(d, 0.0);
                float len = length(maxD);
                float minMaxD = min(max(d.x, d.y), 0.0);
                float dist = len + minMaxD - radius;
                return dist;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // 主纹理颜色（用于遮罩或着色）
                half4 mainTexColor = tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd;

                half4 color;

                #ifdef USEBLUR
                    // 采样全局模糊纹理
                    float2 screenUV = IN.screenPos.xy / IN.vertex.w;
                    half4 blurColor = tex2D(_GlobalFullScreenBlurTexture, screenUV) + _TextureSampleAdd;

                    // 模糊颜色 × 顶点色 × 主纹理色
                    color = blurColor * IN.color;
                    color *= mainTexColor;
                    // 颜色强度调整（仅 RGB）
                    color.rgb *= _ColorPower;
                #else
                    // 无模糊：仅使用主纹理与顶点色
                    color = mainTexColor * IN.color;
                #endif

                // 计算圆角遮罩
                float dist = roundedRectSDF(IN.localPos, _Size, _Radius);
                float roundedAlpha = 1.0 - smoothstep(0.0, 0.01, dist);

                // 将圆角遮罩应用于最终 Alpha
                color.a *= roundedAlpha;

                // UI 矩形裁剪（如果启用）
                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                // Alpha 裁剪（如果启用）
                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                return color;
            }
            ENDCG
        }
    }
}