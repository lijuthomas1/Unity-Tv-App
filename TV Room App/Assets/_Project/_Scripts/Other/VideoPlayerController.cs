using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

namespace TV
{
    /// <summary>
    /// This script controls all video actions.
    /// Also sends the current video status (Playing, Paused, Ended),
    /// according to each state the receiver performs some action.
    /// </summary>
    [RequireComponent(typeof(VideoPlayer))]
    public class VideoPlayerController : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        public static event Action<VideoState> OnVideoStateChanged;
        public static event Action<float> OnVideoTimeChanged;
        public static event Action<string> OnVideoPlayingTimeChanged;
        private VideoState currentVideoState = VideoState.None;
        private bool isVideoEnded = false;
        private float invokeDelayTime = 0.15f;
        public Slider.SliderEvent SliderValueChanged { get; private set; }
        private void Start()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            videoPlayer.prepareCompleted += VideoLoaded;
            videoPlayer.started += VideoStarted;
            videoPlayer.loopPointReached += VideoEnded;
            videoPlayer.seekCompleted += VideoPlayerSeekCompleted;
            GalleryManager.OnVideoClipSelected += UpdateCurrentVideo;
            TvManager.OnTvStateChanged += GameManagerOnTvStateChanged;
            VideoInteractiveSlider.OnValueChanged += OnSliderValueChanged;
        }
        private void OnDestroy()
        {
            videoPlayer.started -= VideoStarted;
            videoPlayer.loopPointReached -= VideoEnded;
            videoPlayer.seekCompleted -= VideoPlayerSeekCompleted;
            GalleryManager.OnVideoClipSelected -= UpdateCurrentVideo;
            TvManager.OnTvStateChanged -= GameManagerOnTvStateChanged;
            VideoInteractiveSlider.OnValueChanged -= OnSliderValueChanged;
        }
        //once current video is loaded,
        private void VideoLoaded(VideoPlayer source)
        {
            CheckAndPlayVideo();
        }
        private void GameManagerOnTvStateChanged(TvState state)
        {
            switch (state)
            {
                case TvState.None:
                case TvState.MainMenu:
                case TvState.Gallery:
                    PauseVideo();
                    ResetVideoClip();
                    break;
            }
        }
        private void VideoEnded(VideoPlayer source)
        {
            isVideoEnded = true;
            //Debug.Log("VideoEnded");
            UpdateVideoState(VideoState.Ended);
        }
        private void UpdateVideoState(VideoState state)
        {
            if (currentVideoState != state)
            {
                currentVideoState = state;
                OnVideoStateChanged?.Invoke(currentVideoState);
                //Debug.Log(currentVideoState + " time " + videoPlayer.time + " length " + videoPlayer.length);
            }
        }
        private void VideoPlayerSeekCompleted(VideoPlayer source)
        {
            Invoke("UpdateVideoTime", invokeDelayTime);
        }
        private void OnSliderValueChanged(float value)
        {
            var videoTime = value * videoPlayer.length;
            SeekVideo(videoTime);
            Invoke("CheckAndPlayVideo", invokeDelayTime);
            UpdateVideoTime();
        }
        private void VideoStarted(VideoPlayer source)
        {
            //Debug.Log("VideoStarted");
            UpdateVideoTime();
        }
        private void UpdateVideoTime()
        {
            OnVideoTimeChanged?.Invoke((float)(videoPlayer.time / videoPlayer.length));
            OnVideoPlayingTimeChanged?.Invoke((videoPlayer.time / 60).ToString("F2") + "/" + (videoPlayer.length / 60).ToString("F2"));
        }
        private void Update()
        {
            if (videoPlayer.isPlaying)
            {
                if (isVideoEnded) isVideoEnded = false;
                UpdateVideoTime();
                UpdateVideoState(VideoState.Playing);
            }
            else if (videoPlayer.isPaused)
            {
                if (!isVideoEnded)
                {
                    UpdateVideoState(VideoState.Paused);
                }
                else
                {
                    UpdateVideoState(VideoState.Ended);
                }
            }
        }
        private void SeekVideo(double time)
        {
            if (videoPlayer && videoPlayer.clip) videoPlayer.time = time;
        }
        private void PauseVideo()
        {
            if (videoPlayer && videoPlayer.clip) videoPlayer.Pause();
        }
        private void PlayVideo()
        {
            if (videoPlayer && videoPlayer.clip) videoPlayer.Play();
        }
        private void CheckAndPlayVideo()
        {
            if (videoPlayer)
            {
                if (TvManager.Instance.GetState() == TvState.VideoPlayback)
                {
                    PlayVideo();
                }
                else
                {
                    PauseVideo();
                }
            }
        }
        private void ResetVideoTime()
        {
            if (videoPlayer) videoPlayer.time = 0;
        }
        private void ResetVideoClip()
        {
            if (videoPlayer) videoPlayer.clip = null;
        }
        private void UpdateCurrentVideo(VideoClip video)
        {
            if (videoPlayer)
            {
                videoPlayer.clip = video;
                videoPlayer.Prepare();
            }
        }
        private void OnClickPlayButton()
        {
            //Debug.Log("OnClickPlayButton");
            if (isVideoEnded)
            {
                ResetVideoTime();
                Invoke("CheckAndPlayVideo", invokeDelayTime);
            }
            if (videoPlayer.isPlaying)
            {
                PauseVideo();
            }
            else if (videoPlayer.isPaused && !isVideoEnded)
            {
                CheckAndPlayVideo();
            }
        }
        private void OnClickBackButton()
        {
            TvManager.Instance.SetState(TvState.Gallery);
        }
    }
}
