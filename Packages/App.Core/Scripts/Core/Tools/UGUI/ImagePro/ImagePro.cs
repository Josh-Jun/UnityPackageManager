/* *
 * ===============================================
 * author      : Josh@macbook
 * e-mail      : shijun_z@163.com
 * create time : 2025年3月17 16:44
 * function    :
 * ===============================================
 * */

using System;

namespace UnityEngine.UI
{
    public class ImagePro : Image
    {
        [SerializeField] [Range(0, 1)] private float m_Radius;

        [SerializeField] private bool m_UseBlur;
        [SerializeField] [Range(1, 20)] private float m_ColorPower;
        
        public bool UseBlur => m_UseBlur;

        private static readonly int SizeID = Shader.PropertyToID("_Size");
        private static readonly int RadiusID = Shader.PropertyToID("_Radius");
        private static readonly int Blur = Shader.PropertyToID("_UseBlur");
        private static readonly int ColorPower = Shader.PropertyToID("_ColorPower");

        private Shader _shader = null;
        private Material _material = null;

        private Material Material
        {
            get
            {
                if (!_shader)
                {
                    _shader = Shader.Find($"UI/RoundedRectangleBlur");
                }

                if (_material) return _material;
                _material = new Material(_shader)
                {
                    name = "Rounded Rectangle Blur"
                };
                
                return _material;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Refresh();
        }
        
        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            Refresh();
        }

        private void Refresh()
        {
            if (material != Material)
            {
                material = Material;
            }
            var maxRadius = Mathf.Min(rectTransform.rect.size.x, rectTransform.rect.size.y) / 2;
            material.SetFloat(RadiusID, m_Radius * maxRadius);
            material.SetVector(SizeID, rectTransform.rect.size);
            material.SetFloat(Blur, m_UseBlur ? 1 : 0);
            material.SetFloat(ColorPower, m_ColorPower);
        }
    }
}