using App.Runtime.Helper;
using App.Runtime.Hotfix;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace App.Runtime
{
    public partial class Launcher : MonoBehaviour
    {
        private const string APP_CONFIG_PATH = "AppConfig";
        public string EVENT_CONFIG_PATH = "Launcher/LauncherEventArgsConfig";
        private bool CanMoveNext = true;
        private CrossAssemblyEventArgsConfig LauncherEventArgsConfig;

        private void Awake()
        {
            // 加载应用配置文件
            Global.AppConfig = Resources.Load<AppConfig>(APP_CONFIG_PATH);
            LauncherEventArgsConfig = Resources.Load<CrossAssemblyEventArgsConfig>(EVENT_CONFIG_PATH);
            // 加载配置文件之后，热更之前事件
            LauncherEventArgsConfig.Execute(new CrossAssemblyEventArgsData<string>("LoadAppConfigCompletedEvent"));
            // 弹出隐私UI界面，启动脚本热更
            HotfixView.Instance.Startup(() =>
            {
                // YooAssets初始化
                YooAssets.Initialize();
                UniTask.Void(async () =>
                {
                    LauncherEventArgsConfig.Execute(new CrossAssemblyEventArgsData<string>("HotfixBeforeEvent"));
                    // 热更之前事件
                    await UniTask.WaitUntil(() => CanMoveNext);
                    // 创建默认包
                    await Assets.UpdatePackage(AssetPackage.BuiltinPackage, HotfixView.Instance.SetDownloadProgress, true);
                    // 热更之后事件
                    LauncherEventArgsConfig.Execute(new CrossAssemblyEventArgsData<string>("HotfixAfterEvent"));
                    // 加载AppScene
                    await Assets.LoadSceneAsync(AssetPath.AppScene);
                });
            });
        }
    }
}