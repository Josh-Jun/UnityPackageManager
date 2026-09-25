using System;

namespace XLocalization.Datas
{
    [Serializable]
    public class LocalizeAssetsData<T> : LocalizeDataBase where T : UnityEngine.Object
    {
        public T Asset;
    }
}