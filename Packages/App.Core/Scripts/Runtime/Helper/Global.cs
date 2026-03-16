using System;
using System.Collections.Generic;
using System.Reflection;
using App.Runtime.CloudCtrl;
using App.Runtime.Helper;
using UnityEngine;


public static partial class Global
{
    public static AppConfig AppConfig;

    public static List<string> AOTMetaAssemblyNames { get; } = new() { "mscorlib.dll", "System.dll", "System.Core.dll", "UnityEngine.CoreModule.dll", };

    public static List<string> HotfixAssemblyNames { get; } = new() { "App.Core", "App.Module", };

    public static Dictionary<string, Assembly> AssemblyPairs { get; } = new();

    public static CloudCtrl CloudCtrl = new();

    public static string PlatformName
    {
        get
        {
#if UNITY_EDITOR
            return UnityEditor.EditorUserBuildSettings.activeBuildTarget switch
            {
                UnityEditor.BuildTarget.iOS or
                    UnityEditor.BuildTarget.WebGL or
                    UnityEditor.BuildTarget.Android or
                    UnityEditor.BuildTarget.VisionOS => $"{UnityEditor.EditorUserBuildSettings.activeBuildTarget}",
                UnityEditor.BuildTarget.StandaloneWindows or
                    UnityEditor.BuildTarget.StandaloneWindows64 => $"Windows",
                UnityEditor.BuildTarget.StandaloneOSX => $"MacOS",
                _ => string.Empty
            };
#else
            return Application.platform switch
            {
                RuntimePlatform.VisionOS or
                    RuntimePlatform.Android => $"{Application.platform}",
                RuntimePlatform.IPhonePlayer => $"iOS",
                RuntimePlatform.OSXPlayer => $"MacOS",
                RuntimePlatform.WindowsPlayer => $"Windows",
                RuntimePlatform.WebGLPlayer => $"WebGL",
                _ => string.Empty
            };
#endif
        }
    }

    private const string ENVIRONMENT_CONFIG_NAME = "EnvironmentConfig";
    private const string ENVIRONMENT_CONFIG_PATH = "Assets/Resources/EnvironmentConfig.asset";

    public static string HttpServer => DevelopmentEnvironments[AppConfig.DevelopmentMold].HttpServer;
    public static string SocketServer => DevelopmentEnvironments[AppConfig.DevelopmentMold].SocketServer;
    public static string CdnServer => DevelopmentEnvironments[AppConfig.DevelopmentMold].CdnServer;
    public static int SocketPort => DevelopmentEnvironments[AppConfig.DevelopmentMold].SocketPort;

    private static EnvironmentConfig EnvironmentConfig
    {
        get
        {
            var config = Resources.Load<EnvironmentConfig>(ENVIRONMENT_CONFIG_NAME);
            if (config != null) return config;
#if UNITY_EDITOR
            config = ScriptableObject.CreateInstance<EnvironmentConfig>();
            var directoryName = System.IO.Path.GetDirectoryName(ENVIRONMENT_CONFIG_PATH);
            if (string.IsNullOrEmpty(directoryName))
            {
                Debug.LogWarning("Environment config path is empty");
                return config;
            }
            if(!System.IO.Directory.Exists(directoryName))
            {
                System.IO.Directory.CreateDirectory(directoryName);
            }
            UnityEditor.AssetDatabase.CreateAsset(config, ENVIRONMENT_CONFIG_PATH);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
            Debug.Log(config.Release.HttpServer);
            return config;
        }
    }
    
    private static readonly Dictionary<DevelopmentMold, DevelopmentEnvironment> DevelopmentEnvironments = new()
    {
        {
            DevelopmentMold.Test,
            EnvironmentConfig.Test
        },
        {
            DevelopmentMold.Sandbox,
            EnvironmentConfig.Sandbox
        },
        {
            DevelopmentMold.Release,
            EnvironmentConfig.Release
        },
        {
            DevelopmentMold.Local,
            EnvironmentConfig.Local
        },
    };
}