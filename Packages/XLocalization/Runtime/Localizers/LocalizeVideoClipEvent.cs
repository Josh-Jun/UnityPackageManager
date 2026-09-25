using UnityEngine.Video;
using XLocalization.Events;

namespace XLocalization.Components
{
    public class LocalizeVideoClipEvent : LocalizeAssetEvent<VideoClip, UnityEventVideoClip>
    {
        protected override void UpdateObject(VideoClip asset)
        {
            if(asset == null) return;
            UpdateAsset.Invoke(asset);
        }
    }
}