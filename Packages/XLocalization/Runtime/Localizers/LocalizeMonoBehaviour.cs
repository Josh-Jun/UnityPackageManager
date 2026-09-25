using UnityEngine;
using XLocalization.Manager;

namespace XLocalization.Components
{
    public class LocalizeMonoBehaviour : MonoBehaviour
    {
        [HideInInspector]
        [SerializeField] 
        public string Key;

        private void Awake()
        {
            LocalizationManager.Instance.OnLanguageUpdateEvent += UpdateLanguage;
        }

        private void OnEnable()
        {
            UpdateLanguage(LocalizationManager.Instance.Language);
        }

        protected virtual void UpdateLanguage(SystemLanguage language)
        {
            
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.OnLanguageUpdateEvent -= UpdateLanguage;
        }
    }
}
