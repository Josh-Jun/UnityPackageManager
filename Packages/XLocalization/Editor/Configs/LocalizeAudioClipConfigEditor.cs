using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XLocalization.Config;
using XLocalization.Datas;
using Random = System.Random;

namespace XLocalization.Editor
{
    [CustomEditor(typeof(LocalizeAudioClipConfig))]
    public class LocalizeAudioClipConfigEditor : UnityEditor.Editor
    {
        private LocalizeAudioClipConfig config;
        private SerializedObject _target;
        
        private static readonly Random _random = new Random();
        private static readonly object _lock = new object();
        
        // 每列固定宽度（表头、数据行共用这一份，保证对齐）
        private const float RowHeight = 18f;
        private const float CellWidth = 100f;
        private const float CellPadding = 2f;
        private Vector2 _scroll;
        private void OnEnable()
        {
            config = (LocalizeAudioClipConfig)target;
            _target = new SerializedObject(target);
            
            config.LanguageConfig = EditorHelper.FindScriptableObject<LocalizeLanguageConfig>();
            
            InitDatas();
            RefreshLanguage();
        }
        private void InitDatas()
        {
            if(config.LanguageConfig == null) return;
            if(config.TableDatas.Count != 0) return;
            AddData();
        }

        private void AddData()
        {
            var assetDatas = config.LanguageConfig.Languages.Select(language => new LocalizeAssetsData<AudioClip>() { Language = language }).ToList();
            var data = new LocalizeAssetTableData<AudioClip>()
            {
                Key = "",
                AssetDatas = assetDatas,
            };
            config.TableDatas.Add(data);
        }
        
        private void RefreshLanguage()
        {
            if(config.LanguageConfig == null) return;
            // 移除语言表不存在的语言数据
            foreach (var td in config.TableDatas)
            {
                foreach (var ad in td.AssetDatas.ToList().Where(ad => !config.LanguageConfig.Languages.Contains(ad.Language)))
                {
                    td.AssetDatas.Remove(ad);
                }
            }

            // 添加语言表数据
            var languages = config.LanguageConfig.Languages.ToList();
            foreach (var ad in config.TableDatas.SelectMany(ad => ad.AssetDatas.ToList().Where(a => languages.Contains(a.Language))))
            {
                languages.Remove(ad.Language);
            }

            foreach (var language in languages)
            {
                foreach (var td in config.TableDatas)
                {
                    var data = new LocalizeAssetsData<AudioClip>()
                    {
                        Language = language
                    };
                    td.AssetDatas.Add(data);
                }
            }
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Undo.RecordObject(config, "object change");
            
            if(config.LanguageConfig == null)
            {
                config.LanguageConfig = EditorHelper.FindScriptableObject<LocalizeLanguageConfig>();
            }

            var width = (config.LanguageConfig.Languages.Count + 1) * CellWidth + CellPadding;
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("导出CSV", GUILayout.Width(width/2)))
            {
                EditorHelper.ExportCSV(config.ToCSVData());
            }
            if (GUILayout.Button("导入CSV", GUILayout.Width(width/2)))
            {
                EditorHelper.ImportCSV(datas =>
                {
                    var tableDatas = datas.ToLocalizeStringTableData<AudioClip>();
                    config.TableDatas = tableDatas;
                });
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("本地化数据(AudioClip)", EditorStyles.boldLabel);
            
            // 外层滚动（行数多时有用）
            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(300));
            
            // ---------- 表头 ----------
            var TableTitle = new List<string> { "Key" };
            var listRange = config.LanguageConfig.Languages.Select(language => language.ToString()).ToList();
            TableTitle.AddRange(listRange);
            
            DrawTitle(TableTitle);
            
            if (config.TableDatas.Count > 0)
            {
                DrawTable(config.TableDatas);
            }
            else
            {
                DrawLine((config.LanguageConfig.Languages.Count + 1) * CellWidth + 1,1, Color.gray);
            }

            if (GUILayout.Button("+", GUILayout.Width(width)))
            {
                AddData();
            }
            EditorGUILayout.EndScrollView();
            EditorUtility.SetDirty(config);
            _target.ApplyModifiedProperties();
        }

        private void DrawTable(List<LocalizeAssetTableData<AudioClip>> data)
        {
            // ---------- 数据行 ----------
            for (var i = 0; i < data.Count; i++)
            {
                DrawLine((config.LanguageConfig.Languages.Count + 1) * CellWidth + 1,1, Color.gray);
                var cells = data[i].AssetDatas.Select(t => t.Asset).ToList();
                DrawRow($"{data[i].Key}", cells, i);
            }
            
            DrawLine((config.LanguageConfig.Languages.Count + 1) * CellWidth + 1,1, Color.gray);
        }

        private void DrawRow(string Key, List<AudioClip> cs, int index)
        {
            EditorGUILayout.BeginHorizontal();
            
            // 整行占用的矩形
            var rowRect = EditorGUILayout.GetControlRect(false, RowHeight);
            rowRect.width = cs.Count * CellWidth;
        
            var x = rowRect.x;
        
            var first = new Rect(x, rowRect.y - CellPadding, 1f, rowRect.height + CellPadding * 2);
            DrawLine(first, Color.gray);

            var cellRect = new Rect(x, rowRect.y, CellWidth, rowRect.height);
            var contentRect = new Rect(
                cellRect.x + CellPadding,
                cellRect.y,
                cellRect.width - CellPadding * 2,
                RowHeight
            );
            config.TableDatas[index].Key = GUI.TextField(contentRect, Key);
            x += CellWidth;
            
            var next = new Rect(x, rowRect.y - CellPadding, 1f, rowRect.height + CellPadding * 2);
            DrawLine(next, Color.gray);

            for (var i = 0; i < cs.Count; i++)
            {
                // 每列精确矩形（宽度固定，位置累加）
                cellRect = new Rect(x, rowRect.y, CellWidth, rowRect.height);
                // 内边距，避免文字贴边
                contentRect = new Rect(
                    cellRect.x + CellPadding,
                    cellRect.y,
                    cellRect.width - CellPadding * 2,
                    RowHeight
                );
                config.TableDatas[index].AssetDatas[i].Asset = (AudioClip)EditorGUI.ObjectField(contentRect, cs[i], typeof(AudioClip), false);
                x += CellWidth;
                var rect = new Rect(x, rowRect.y - CellPadding, 1f, rowRect.height+CellPadding*2);
                DrawLine(rect, Color.gray);
            }
        
            var rectButton = new Rect(x + 8, rowRect.y, RowHeight, RowHeight);
            if (GUI.Button(rectButton, "-"))
            {
                config.TableDatas.RemoveAt(index);
            }
            
            EditorGUILayout.EndHorizontal();
        }

        private void DrawTitle(List<string> cs)
        {
            var rowRect = EditorGUILayout.GetControlRect(false, RowHeight);
            var titleRect = new Rect(rowRect.x, rowRect.y, rowRect.width, 1)
            {
                width = cs.Count * 100
            };
            EditorGUI.DrawRect(titleRect, Color.gray);
            
            EditorGUILayout.BeginHorizontal();
            // 整行占用的矩形
            rowRect.width = cs.Count * CellWidth;
            rowRect.height += CellPadding;
            EditorGUI.DrawRect(rowRect, new Color(0.5f, 0.5f, 0.5f, 0.1f));

            var x = rowRect.x;

            var first = new Rect(x, rowRect.y, 1f, rowRect.height + CellPadding);
            DrawLine(first, Color.gray);

            foreach (var t in cs)
            {
                // 每列精确矩形（宽度固定，位置累加）
                var cellRect = new Rect(x, rowRect.y, CellWidth, rowRect.height);
                // 内边距，避免文字贴边
                var contentRect = new Rect(
                    cellRect.x + CellPadding,
                    cellRect.y + CellPadding,
                    cellRect.width - CellPadding * 2,
                    RowHeight
                );
                GUI.Label(contentRect, t, EditorStyles.boldLabel);
                x += CellWidth;
                var rect = new Rect(x, rowRect.y, 1f, rowRect.height+CellPadding);
                DrawLine(rect, Color.gray);
            }

            EditorGUILayout.EndHorizontal();
        }
        
        private void DrawLine(Rect rect, Color color)
        {
            EditorGUI.DrawRect(rect, color);
        }
        private void DrawLine(float width, float height, Color color)
        {
            var rect = EditorGUILayout.GetControlRect(false, height);
            rect.width = width;
            EditorGUI.DrawRect(rect, color);
        }
    }
}
