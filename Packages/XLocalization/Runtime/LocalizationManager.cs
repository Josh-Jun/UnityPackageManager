using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace XLocalization.Manager
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance { get; set; } = null;

        private const string LOCALIZATION_LANGUAGE = "LOCALIZATION_LANGUAGE";

        private SystemLanguage _language;

        public SystemLanguage Language
        {
            get => _language;
            set
            {
                _language = value;
                PlayerPrefs.SetInt(LOCALIZATION_LANGUAGE, (int)_language);
                OnLanguageUpdateEvent?.Invoke(_language);
            }
        }

        public UnityAction<SystemLanguage> OnLanguageUpdateEvent;

        private void Awake()
        {
            if (Instance == null)
            {
                //单例初始化
                Instance = this;
                //切换场景不销毁此对象
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            if (PlayerPrefs.HasKey(LOCALIZATION_LANGUAGE))
            {
                Language = (SystemLanguage)PlayerPrefs.GetInt(LOCALIZATION_LANGUAGE);
            }
            else
            {
                Language = Application.systemLanguage;
            }
#if UNITY_EDITOR
            _languageConfig = FindScriptableObject<LocalizeLanguageConfig>();
            if (_languageConfig == null) return;
            var array = _languageConfig.Languages.Select(language => language.ToString()).ToArray();
            _dropdown = new OnGUIDropdown(
                array,
                defaultIndex: 0,
                onChanged: index =>
                {
                    if (!Enum.TryParse<SystemLanguage>(array[index], out var language)) return;
                    Language = language;
                }
            );
#endif
        }

        // Update is called once per frame
        private void Update()
        {
        }

#if UNITY_EDITOR
        private LocalizeLanguageConfig _languageConfig;
        private OnGUIDropdown _dropdown;

        private void OnGUI()
        {
            if(_dropdown == null) return;
            var rect = new Rect(0, 0, 120, 20);
            _dropdown.Draw(rect);
        }

        public static T FindScriptableObject<T>() where T : ScriptableObject
        {
            var guids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            if (guids.Length <= 0) return null;
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
            var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            return asset;
        }
#endif
    }
#if UNITY_EDITOR
    /// <summary>
    /// 运行时 OnGUI 用的下拉菜单，可复用。
    /// </summary>
    public class OnGUIDropdown
    {
        public string[] Options { get; set; }
        public int SelectedIndex { get; private set; }
        public event Action<int> OnValueChanged;

        private bool _isOpen;
        private Rect _buttonRect;
        private readonly int _controlId;

        private static int _nextId = 0;

        private const float ItemHeight = 22f;

        public OnGUIDropdown(string[] options, int defaultIndex = 0, Action<int> onChanged = null)
        {
            Options = options;
            SelectedIndex = Mathf.Clamp(defaultIndex, 0, options.Length - 1);
            OnValueChanged = onChanged;
            _controlId = _nextId++; // 每个实例唯一 ID，避免多个下拉互相干扰
        }

        /// <summary>
        /// 在 OnGUI 里调用，返回当前选中的索引。
        /// </summary>
        public int Draw(Rect buttonRect)
        {
            _buttonRect = buttonRect;
            var label = Options.Length > 0 ? Options[SelectedIndex] : "None";

            // 按钮
            if (GUI.Button(buttonRect, label))
            {
                _isOpen = !_isOpen;
            }

            // 展开列表
            if (!_isOpen) return SelectedIndex;
            var listHeight = ItemHeight * Options.Length;
            var listRect = new Rect(
                buttonRect.x,
                buttonRect.y + buttonRect.height,
                buttonRect.width,
                listHeight
            );

            GUI.Box(listRect, "");

            for (var i = 0; i < Options.Length; i++)
            {
                var itemRect = new Rect(
                    listRect.x + 1,
                    listRect.y + i * ItemHeight,
                    listRect.width - 2,
                    ItemHeight
                );

                // 选中项高亮
                if (i == SelectedIndex)
                {
                    EditorLikeHighlight(itemRect);
                }

                if (!GUI.Button(itemRect, Options[i])) continue;
                if (i != SelectedIndex)
                {
                    SelectedIndex = i;
                    OnValueChanged?.Invoke(i);
                }

                _isOpen = false;
            }

            return SelectedIndex;
        }

        private void EditorLikeHighlight(Rect rect)
        {
            var old = GUI.color;
            GUI.color = new Color(0.24f, 0.48f, 0.9f, 0.6f);
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
            GUI.color = old;
        }
    }
#endif
}