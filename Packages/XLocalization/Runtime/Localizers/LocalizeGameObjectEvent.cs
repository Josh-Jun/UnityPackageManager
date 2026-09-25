using UnityEngine;
using XLocalization.Events;

namespace XLocalization.Components
{
    public class LocalizeGameObjectEvent : LocalizeAssetEvent<GameObject, UnityEventGameObject>
    {
        private GameObject m_Current;
        protected override void UpdateObject(GameObject asset)
        {
            if (m_Current != null)
            {
                Destroy(m_Current);
                m_Current = null;
            }

            if (asset != null)
            {
                m_Current = Instantiate(asset, transform);
                m_Current.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
            }
            UpdateAsset.Invoke(asset);
        }
    }
}