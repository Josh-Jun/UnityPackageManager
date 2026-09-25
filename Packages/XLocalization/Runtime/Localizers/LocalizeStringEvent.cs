using System.Linq;
using UnityEngine;
using XLocalization.Config;
using XLocalization.Events;
using XLocalization.Manager;

namespace XLocalization.Components
{
    public class LocalizeStringEvent : LocalizeMonoBehaviour
    {
        [HideInInspector]
        public LocalizeStringConfig LocalizedReference;
        [HideInInspector]
        public UnityEventString UpdateString;

        protected override void UpdateLanguage(SystemLanguage language)
        {
            var text = "";
            foreach (var stringData in from tableData in LocalizedReference.TableDatas where tableData.Key == Key from stringData in tableData.StringDatas where stringData.Language == language select stringData)
            {
                text = stringData.Value;
            }
            UpdateString.Invoke(text);
        }

        public void SetReference(LocalizeStringConfig reference)
        {
            LocalizedReference = reference;
            UpdateLanguage(LocalizationManager.Instance.Language);
        }

        public void SetKey(string key)
        {
            Key = key;
            UpdateLanguage(LocalizationManager.Instance.Language);
        }

        public void SetReferenceKey(LocalizeStringConfig reference, string key)
        {
            LocalizedReference = reference;
            Key = key;
            UpdateLanguage(LocalizationManager.Instance.Language);
        }
    }
}
