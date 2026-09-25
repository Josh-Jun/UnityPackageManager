using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using XLocalization.Config;
using XLocalization.Manager;

namespace XLocalization.Components
{
    public abstract class LocalizeAssetEvent<T, E> : LocalizeMonoBehaviour where T : Object where E : UnityEvent<T>
    {
        [HideInInspector]
        public LocalizeAssetConfig<T> LocalizedReference;
        [HideInInspector]
        public E UpdateAsset;

        protected abstract void UpdateObject(T asset);
        
        protected override void UpdateLanguage(SystemLanguage language)
        {
            T asset = null;
            foreach (var assetsData in from tableData in LocalizedReference.TableDatas where tableData.Key == Key from assetsData in tableData.AssetDatas where assetsData.Language == language select assetsData)
            {
                asset = assetsData.Asset;
            }
            UpdateObject(asset);
        }

        public void SetReference(LocalizeAssetConfig<T> reference)
        {
            LocalizedReference = reference;
            UpdateLanguage(LocalizationManager.Instance.Language);
        }

        public void SetKey(string key)
        {
            Key = key;
            UpdateLanguage(LocalizationManager.Instance.Language);
        }

        public void SetReferenceKey(LocalizeAssetConfig<T> reference, string key)
        {
            LocalizedReference = reference;
            Key = key;
            UpdateLanguage(LocalizationManager.Instance.Language);
        }
    }
}
