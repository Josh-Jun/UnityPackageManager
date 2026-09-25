using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;
using XLocalization.Config;
using XLocalization.Datas;
using Object = UnityEngine.Object;

namespace XLocalization.Editor
{
    public static class EditorHelper
    {
        public static string FindPath<T>() where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            if (guids.Length <= 0) return "";
            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return path;
        }
        
        public static T FindScriptableObject<T>() where T : ScriptableObject
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            if (guids.Length > 0)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                var asset = AssetDatabase.LoadAssetAtPath<T>(path);
                return asset;
            }
            else
            {
                var asset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, $"Assets/Resources/{typeof(T).Name}.asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                return null;
            }
        }

        public static List<LocalizeStringTableData> ToLocalizeStringTableData(this List<string[]> datas)
        {
            var tableDatas = new List<LocalizeStringTableData>();
            for (var j = 1; j < datas.Count; j++)
            {
                var tableData = new LocalizeStringTableData()
                {
                    Key = datas[j][0]
                };
                for (var k = 1; k < datas[j].Length; k++)
                {
                    var stringData = new LocalizeStringData()
                    {
                        Language = Enum.Parse<SystemLanguage>(datas[0][k]),
                        Value = datas[j][k]
                    };
                    tableData.StringDatas.Add(stringData);
                }
                tableDatas.Add(tableData);
            }
            return tableDatas;
        }

        public static List<string[]> ToCSVData(this LocalizeStringConfig Config)
        {
            var titles = new string[Config.LanguageConfig.Languages.Count + 1];
            titles[0] = "Key";
            var index = 1;
            foreach (var language in Config.LanguageConfig.Languages)
            {
                titles[index] = language.ToString();
                index++;
            }
            
            var datas = new List<string[]>()
            {
                titles,
            };

            foreach (var tableData in Config.TableDatas)
            {
                var rows = new string[Config.LanguageConfig.Languages.Count + 1];
                rows[0] = tableData.Key;
                var i = 1;
                foreach (var stringData in tableData.StringDatas)
                {
                    rows[i] = stringData.Value;
                    i++;
                }
                datas.Add(rows);
            }
            
            return datas;
        }
        public static List<string[]> ToCSVData<T>(this LocalizeAssetConfig<T> Config) where T : Object
        {
            var titles = new string[Config.LanguageConfig.Languages.Count + 1];
            titles[0] = "Key";
            var index = 1;
            foreach (var language in Config.LanguageConfig.Languages)
            {
                titles[index] = language.ToString();
                index++;
            }
            
            var datas = new List<string[]>()
            {
                titles,
            };
            
            foreach (var tableData in Config.TableDatas)
            {
                var rows = new string[Config.LanguageConfig.Languages.Count + 1];
                rows[0] = tableData.Key;
                var i = 1;
                foreach (var stringData in tableData.AssetDatas)
                {
                    rows[i] = AssetDatabase.GetAssetPath(stringData.Asset);
                    i++;
                }
                datas.Add(rows);
            }
            
            return datas;
        }

        public static List<LocalizeAssetTableData<T>> ToLocalizeStringTableData<T>(this List<string[]> datas) where T : Object
        {
            var tableDatas = new List<LocalizeAssetTableData<T>>();
            for (var j = 1; j < datas.Count; j++)
            {
                var tableData = new LocalizeAssetTableData<T>()
                {
                    Key = datas[j][0]
                };
                for (var k = 1; k < datas[j].Length; k++)
                {
                    var assetsData = new LocalizeAssetsData<T>()
                    {
                        Language = Enum.Parse<SystemLanguage>(datas[0][k]),
                        Asset = AssetDatabase.LoadAssetAtPath<T>(datas[j][k])
                    };
                    tableData.AssetDatas.Add(assetsData);
                }
                tableDatas.Add(tableData);
            }
            return tableDatas;
        }
        
        public static void ExportCSV(List<string[]> datas, string fileName = "data")
        {
            // 1. 弹出保存对话框
            var path = EditorUtility.SaveFilePanel(
                "导出 CSV",
                "",
                fileName,
                "csv"
            );

            if (string.IsNullOrEmpty(path)) return; // 用户取消

            // 2. 写入 CSV（关键：UTF-8 with BOM）
            var utf8WithBom = new UTF8Encoding(true);
            using (var writer = new StreamWriter(path, false, utf8WithBom))
            {
                foreach (var row in datas)
                {
                    writer.WriteLine(string.Join(",", row));
                }
            }

            AssetDatabase.Refresh();
            Debug.Log($"CSV 已导出到: {path}");
        }

        public static void ImportCSV(Action<List<string[]>> callback)
        {
            var path = EditorUtility.OpenFilePanel("选择 CSV", "", "csv");
            if (string.IsNullOrEmpty(path)) return;

            var lines = File.ReadAllLines(path);
            // 逐行解析...
            var datas = lines.Select(str => str.Split(',')).ToList();
            callback?.Invoke(datas);
        }
    }
}
