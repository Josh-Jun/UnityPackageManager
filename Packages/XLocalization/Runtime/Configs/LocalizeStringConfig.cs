using System.Collections.Generic;
using UnityEngine;
using XLocalization.Datas;

namespace XLocalization.Config
{
    [CreateAssetMenu(fileName = "LocalizeStringConfig", menuName = "XLocalization/LocalizeStringConfig")]
    public class LocalizeStringConfig : ScriptableObject
    {
        [HideInInspector]
        public LocalizeLanguageConfig LanguageConfig;
        [HideInInspector]
        public List<LocalizeStringTableData> TableDatas = new();
    }
}
