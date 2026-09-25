using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace XLocalization.Events
{
    [Serializable]
    public class UnityEventVideoClip : UnityEvent<VideoClip> {}

    [Serializable]
    public class UnityEventAudioClip : UnityEvent<AudioClip> {}
    
    [Serializable]
    public class UnityEventGameObject : UnityEvent<GameObject> {}

    [Serializable]
    public class UnityEventSprite : UnityEvent<Sprite> {}

    [Serializable]
    public class UnityEventString : UnityEvent<string> {};

    [Serializable]
    public class UnityEventTexture : UnityEvent<Texture> {}
}
