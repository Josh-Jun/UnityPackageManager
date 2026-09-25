using System;
using System.Collections.Generic;

namespace XLocalization.Datas
{
    [Serializable]
    public class LocalizeAssetTableData<T> where T : UnityEngine.Object
    {
        public string Key;
        public List<LocalizeAssetsData<T>> AssetDatas = new();
    }
}
