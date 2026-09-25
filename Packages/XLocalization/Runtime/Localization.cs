using UnityEngine;

namespace XLocalization.Manager
{
    public static class Localization
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void OnRuntimeMethodLoad()
        {
            var go = new GameObject("LocalizationManager", typeof(LocalizationManager));
        }
    }
}
