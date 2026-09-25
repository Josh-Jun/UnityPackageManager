using System;
using System.Collections.Generic;

namespace XLocalization.Datas
{
    [Serializable]
    public class LocalizeStringTableData
    {
        public string Key;
        public List<LocalizeStringData> StringDatas = new();
    }
}
