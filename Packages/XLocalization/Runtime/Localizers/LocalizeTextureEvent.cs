using UnityEngine;
using XLocalization.Events;

namespace XLocalization.Components
{
    public class LocalizeTextureEvent : LocalizeAssetEvent<Texture, UnityEventTexture>
    {
        protected override void UpdateObject(Texture asset)
        {
            if(asset == null) return;
            UpdateAsset.Invoke(asset);
        }
    }
}