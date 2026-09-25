using UnityEngine;
using XLocalization.Events;

namespace XLocalization.Components
{
    public class LocalizeAudioClipEvent : LocalizeAssetEvent<AudioClip, UnityEventAudioClip>
    {
        protected override void UpdateObject(AudioClip asset)
        {
            if(asset == null) return;
            UpdateAsset.Invoke(asset);
        }
    }
}