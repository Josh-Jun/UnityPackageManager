using System.Collections.Generic;
using UnityEngine;
using XLocalization.Datas;

namespace  XLocalization.Config
{
    public class LocalizeAssetConfig<T> : ScriptableObject where T : Object
    {
        [HideInInspector]
        public LocalizeLanguageConfig LanguageConfig;
        [HideInInspector]
        public List<LocalizeAssetTableData<T>> TableDatas = new();
    }
}
