/* *
 * ===============================================
 * author      : Josh@book
 * e-mail      : shijun_z@163.com
 * create time : 2025年1月9 10:47
 * function    :
 * ===============================================
 * */

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using YooAsset;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace App.Runtime.Hotfix
{
    public partial class HotfixView : MonoBehaviour
    {
        public static HotfixView Instance;
        
        public bool ShowAgreePanel = true;
        private const string Agreement = "ARGEEMENT";
        
        private Slider _slider;
        private TextMeshProUGUI _text;
        private TextMeshProUGUI _progressText;

        private void Awake()
        {
            Instance = this;
            
            _slider = transform.Find("Slider").GetComponent<Slider>();
            _text = transform.Find("Slider/Text").GetComponent<TextMeshProUGUI>();
            _progressText = transform.Find("Slider/Fill Area/Fill/Progress").GetComponent<TextMeshProUGUI>();
            if (_slider.gameObject.activeSelf)
                _slider.gameObject.SetActive(false);
        }

        public void Startup(Action callback)
        {
            if (PlayerPrefs.HasKey(Agreement) || !ShowAgreePanel)
            {
                callback?.Invoke();
                return;
            }
            SendMessage("ShowAgreePanel", callback);
        }

        public void SetDownloadProgress(DownloadUpdateData data)
        {
            if (!_slider.gameObject.activeSelf)
            {
                _slider.gameObject.SetActive(true);
            }
            var progress = data.CurrentDownloadBytes / (float)data.TotalDownloadBytes;
            _slider.value = Mathf.Clamp(progress, 0, 1);
            _progressText.text = $"{(progress * 100):00}%";
            _text.text = $"{data.CurrentDownloadBytes / 1048576f:F2}M/{data.TotalDownloadBytes / 1048576f:F2}M\n{data.CurrentDownloadCount}/{data.TotalDownloadCount}";
        }
    }
}