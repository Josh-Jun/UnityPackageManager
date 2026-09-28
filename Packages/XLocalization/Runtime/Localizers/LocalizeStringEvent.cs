using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
        [HideInInspector]
        public bool UseArguments = false;
        [HideInInspector]
        public List<string> Arguments = new();

        protected override void UpdateLanguage(SystemLanguage language)
        {
            var text = "";
            var str = "";
            foreach (var stringData in from tableData in LocalizedReference.TableDatas where tableData.Key == Key from stringData in tableData.StringDatas where stringData.Language == language select stringData)
            {
                str = stringData.Value;
            }
            if (Arguments.Count > 0 && UseArguments)
            {
                try
                {
                    text = string.Format(str, Arguments.ToArray() as object[]);
                }
                catch (FormatException fe)
                {
                    // Supplement with a better error message as its likely that the string was a Smart String.
                    throw new FormatException($"Input string was not in the correct format for String.Format. Ensure that the string is marked as Smart if you intended to use Smart Format.\n`{str}`\n{fe}", fe);
                }
            }
            else
            {
                text = str;
            }
            UpdateString.Invoke(text);
        }

        public void SetReference(LocalizeStringConfig reference)
        {
            LocalizedReference = reference;
#if UNITY_EDITOR
            UpdateLanguage(Application.systemLanguage);
#else
            UpdateLanguage(XLocalization.Manager.LocalizationManager.Instance.Language);
#endif
        }

        public void SetKey(string key)
        {
            Key = key;
#if UNITY_EDITOR
            var language = EditorApplication.isPlaying
                ? LocalizationManager.Instance.Language
                : Application.systemLanguage;
#else
            var language = LocalizationManager.Instance.Language;
#endif
            UpdateLanguage(language);
        }

        public void SetReferenceKey(LocalizeStringConfig reference, string key)
        {
            LocalizedReference = reference;
            Key = key;
#if UNITY_EDITOR
            var language = EditorApplication.isPlaying
                ? LocalizationManager.Instance.Language
                : Application.systemLanguage;
#else
            var language = LocalizationManager.Instance.Language;
#endif
            UpdateLanguage(language);
        }

        public void SetArguments(List<string> arguments)
        {
            Arguments = arguments;
#if UNITY_EDITOR
            var language = EditorApplication.isPlaying
                ? LocalizationManager.Instance.Language
                : Application.systemLanguage;
#else
            var language = LocalizationManager.Instance.Language;
#endif
            UpdateLanguage(language);
        }

        public void SetArguments(params string[] args)
        {
            Arguments.Clear();
            Arguments.AddRange(args);
#if UNITY_EDITOR
            var language = EditorApplication.isPlaying
                ? LocalizationManager.Instance.Language
                : Application.systemLanguage;
#else
            var language = LocalizationManager.Instance.Language;
#endif
            UpdateLanguage(language);
        }
    }
}
