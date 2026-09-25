using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace XLocalization.Editor
{
    [CustomEditor(typeof(LocalizeLanguageConfig))]
    public class LocalizeLanguageConfigEditor : UnityEditor.Editor
    {
        private LocalizeLanguageConfig config;
        private SerializedObject _target;
        
        private static readonly Random _random = new Random();
        private static readonly object _lock = new object();
        
        // 每列固定宽度（表头、数据行共用这一份，保证对齐）
        private const float RowHeight = 18f;
        private const float RowWidth = 130f;
        private Vector2 _scroll;
        private void OnEnable()
        {
            config = (LocalizeLanguageConfig)target;
            _target = new SerializedObject(target);
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Undo.RecordObject(config, "object change");
            EditorGUILayout.LabelField("语言列表", EditorStyles.boldLabel);
            
            // 外层滚动（行数多时有用）
            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(300));
            
            for (var i = 0; i < config.Languages.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                var rect = EditorGUILayout.GetControlRect(false, RowHeight);
                var rectPopup = new Rect(rect.x, rect.y, RowWidth, RowHeight);
                config.Languages[i] = (SystemLanguage)EditorGUI.EnumPopup(rectPopup, config.Languages[i]);
                var rectButton = new Rect(rect.x + rectPopup.width, rect.y, RowHeight, RowHeight);
                if (GUI.Button(rectButton, "-"))
                {
                    config.Languages.RemoveAt(i);
                }
                
                EditorGUILayout.EndHorizontal();
            }
            
            var _rect = EditorGUILayout.GetControlRect(false, RowHeight);
            var _rectButton = new Rect(_rect.x, _rect.y, RowWidth, RowHeight);
            if (GUI.Button(_rectButton, "+"))
            {
                if (TryRandomExcluding(config.Languages, out var language))
                {
                    config.Languages.Add(language);
                }
            }
            
            EditorGUILayout.EndScrollView();
            
            EditorUtility.SetDirty(config);
            _target.ApplyModifiedProperties();
        }

        private bool TryRandomExcluding<T>(IEnumerable<T> excludeList, out T result) where T : struct, Enum
        {
            var excluded = excludeList == null
                ? new HashSet<T>()
                : new HashSet<T>(excludeList);

            var candidates = Enum.GetValues(typeof(T))
                .Cast<T>()
                .Where(e => !excluded.Contains(e))
                .ToList();

            if (candidates.Count == 0)
            {
                result = default;
                return false;
            }

            lock (_lock)
            {
                result = candidates[_random.Next(candidates.Count)];
            }
            return true;
        }
    }
}
