using UnityEngine;
using XLocalization.Events;

namespace XLocalization.Components
{
    public class LocalizeSpriteEvent : LocalizeAssetEvent<Sprite, UnityEventSprite>
    {
        protected override void UpdateObject(Sprite asset)
        {
            if(asset == null) return;
            UpdateAsset.Invoke(asset);
        }
    }
}