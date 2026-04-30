using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using App.Core.Tools;

namespace App.Core.Master
{
    public partial class VideoMaster : SingletonMono<VideoMaster>
    {
        private RenderTexture movie;
        private GameObject video;
        private VideoPlayer VideoPlayer { get; set; }
        private readonly Dictionary<string, VideoPlayer> videoPlayers = new();

        private void Awake()
        {
            video = new GameObject("VideoPlayer", typeof(VideoPlayer));
            video.transform.SetParent(transform);
            VideoPlayer = video.GetOrAddComponent<VideoPlayer>();
            VideoPlayer.playOnAwake = false;
            VideoPlayer.sendFrameReadyEvents = true;
        }

        public void CreateVideoPlayer(string playerName, bool loops = false)
        {
            if (string.IsNullOrEmpty(playerName)) return;
            if (videoPlayers.ContainsKey(playerName)) return;
            var player = new GameObject(playerName, typeof(VideoPlayer));
            player.transform.SetParent(video.transform);
            videoPlayers[playerName] = player.GetOrAddComponent<VideoPlayer>();
            videoPlayers[playerName].playOnAwake = false;
            videoPlayers[playerName].sendFrameReadyEvents = true;
            videoPlayers[playerName].isLooping = loops;
        }

        public VideoPlayer GetVideoPlayer(string playerName = null)
        {
            if(string.IsNullOrEmpty(playerName))
            {
                return VideoPlayer;
            }

            if (videoPlayers.TryGetValue(playerName, out var player)) return player;
            CreateVideoPlayer(playerName);
            player = videoPlayers[playerName];
            return player;
        }

        public void RemoveVideoPlayer(string playerName)
        {
            if(!videoPlayers.TryGetValue(playerName, out var player)) return;
            Destroy(player.gameObject);
            videoPlayers.Remove(playerName);
        }

        public void SetVideoPlayerLoop(string playerName, bool loops)
        {
            GetVideoPlayer(playerName).isLooping = loops;
        }

        /// <summary>在RawImage上播放视频，URL</summary>
        public void PlayVideo(RawImage rawImage, string url, string playerName = null, Action cb = null, int width = 0, int height = 0)
        {
            var player = GetVideoPlayer(playerName);
            SetRenderTexture(player, width, height, VideoRenderMode.RenderTexture);
            player.source = VideoSource.Url;
            player.url = url;
            rawImage.texture = movie;
            player.Play();
            player.loopPointReached += _ => { cb?.Invoke(); };
        }

        /// <summary>在RawImage上播放视频，Clip</summary>
        public void PlayVideo(RawImage rawImage, VideoClip clip, string playerName = null, Action cb = null, int width = 0, int height = 0)
        {
            var player = GetVideoPlayer(playerName);
            SetRenderTexture(player, width, height, VideoRenderMode.RenderTexture);
            player.source = VideoSource.VideoClip;
            player.clip = clip;
            rawImage.texture = movie;
            VideoPlayer.Play();
            player.loopPointReached += _ => { cb?.Invoke(); };
        }
        
        /// <summary>在Renderer上播放视频，URL</summary>
        public void PlayVideo(Renderer render, string url, string playerName = null, Action cb = null, int width = 0, int height = 0)
        {
            var player = GetVideoPlayer(playerName);
            SetRenderTexture(player, width, height, VideoRenderMode.MaterialOverride);
            player.source = VideoSource.Url;
            player.url = url;
            player.targetMaterialRenderer = render;
            player.Play();
            player.loopPointReached += (VideoPlayer vp) => { cb?.Invoke(); };
        }

        /// <summary>在Renderer上播放视频，Clip</summary>
        public void PlayVideo(Renderer render, VideoClip clip, string playerName = null, Action cb = null, int width = 0, int height = 0)
        {
            var player = GetVideoPlayer(playerName);
            SetRenderTexture(player, width, height, VideoRenderMode.MaterialOverride);
            player.source = VideoSource.VideoClip;
            player.clip = clip;
            player.targetMaterialRenderer = render;
            player.Play();
            player.loopPointReached += _ => { cb?.Invoke(); };
        }
        
        /// <summary>在Camera上播放视频，URL</summary>
        public void PlayVideo(Camera targetCamera, string url, int layer, VideoAspectRatio aspectRatio, string playerName = null, Action cb = null, int width = 0, int height = 0)
        {
            var player = GetVideoPlayer(playerName);
            SetRenderTexture(player, width, height, (VideoRenderMode)layer);
            player.source = VideoSource.Url;
            player.url = url;
            player.targetCamera = targetCamera;
            player.aspectRatio = aspectRatio;
            player.Play();
            player.loopPointReached += _ => { cb?.Invoke(); };
        }

        /// <summary>在Camera上播放视频，Clip</summary>
        public void PlayVideo(Camera targetCamera, VideoClip clip, int layer, VideoAspectRatio aspectRatio, string playerName = null, Action cb = null, int width = 0, int height = 0)
        {
            var player = GetVideoPlayer(playerName);
            SetRenderTexture(player, width, height, (VideoRenderMode)layer);
            player.source = VideoSource.VideoClip;
            player.clip = clip;
            player.targetCamera = targetCamera;
            player.aspectRatio = aspectRatio;
            player.Play();
            player.loopPointReached += _ => { cb?.Invoke(); };
        }

        /// <summary>设置RenderTexture</summary>
        private void SetRenderTexture(VideoPlayer player, int width, int height, VideoRenderMode renderMode, int depth = 24)
        {
            player.renderMode = renderMode;
            if (renderMode != VideoRenderMode.RenderTexture) return;
            if (width == 0 || height == 0)
            {
                width = Screen.width;
                height = Screen.height;
            }
            movie = RenderTexture.GetTemporary(width, height, depth, RenderTextureFormat.ARGB32);
            player.targetTexture = movie;
        }

        private long frameIndex = 0;
        private Action<Texture2D> callback = null;

        /// <summary>获取视频某帧图片</summary>
        public void GetVideoFrameTexture(VideoClip clip, long frameId, Action<Texture2D> action, string playerName = null)
        {
            var player = GetVideoPlayer(playerName);
            frameIndex = frameId;
            callback = action;
            player.renderMode = VideoRenderMode.APIOnly;
            player.source = VideoSource.VideoClip;
            player.clip = clip;
            player.waitForFirstFrame = true;
            player.sendFrameReadyEvents = true;
            player.frameReady += OnFrameReadyEvent;
            player.Play();
        }
        /// <summary>获取视频某帧图片</summary>
        public void GetVideoFrameTexture(string url, long frameId, Action<Texture2D> action, string playerName = null)
        {
            var player = GetVideoPlayer(playerName);
            frameIndex = frameId;
            callback = action;
            player.renderMode = VideoRenderMode.APIOnly;
            player.source = VideoSource.Url;
            player.url = url;
            player.waitForFirstFrame = true;
            player.sendFrameReadyEvents = true;
            player.frameReady += OnFrameReadyEvent;
            player.Play();
        }

        private void OnFrameReadyEvent(VideoPlayer source, long frameIdx)
        {
            if (frameIdx != frameIndex) return;
            var renderTexture = source.texture as RenderTexture;
            if (renderTexture == null) return;
            var texture = new Texture2D(renderTexture.width, renderTexture.height);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = null;
            source.frameReady -= OnFrameReadyEvent;
            source.sendFrameReadyEvents = false;
            source.Stop();
            callback?.Invoke(texture);
        }
    }
}
