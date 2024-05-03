using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
using System;

namespace TV
{
    /// <summary>
    /// This script controls all video actions.
    /// Also sends the current video status (Playing, Paused, Ended),
    /// according to each state the receiver performs some action.
    /// </summary>
    public class VideoPlayerController : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private Slider videoVisualingSlider;
        [SerializeField] private Slider videoInteractingSlider;
        [SerializeField] private TMP_Text videoTimeText;
        public static event Action<VideoState> OnVideoStateChanged;
        private VideoState currentVideoState = VideoState.None;
        private bool isVideoEnded = false;
        private float invokeDelayTime = 0.15f;

        public Slider.SliderEvent SliderValueChanged { get; private set; }

        private void Start()
        {
            videoInteractingSlider.onValueChanged.AddListener((v) =>
            {
                OnSliderValueChanged();
            });
            videoPlayer.prepareCompleted += VideoLoaded;
            videoPlayer.started += VideoStarted;
            videoPlayer.loopPointReached += VideoEnded;
            videoPlayer.seekCompleted += VideoPlayerSeekCompleted;
            GalleryManager.OnVideoClipSelected += UpdateCurrentVideo;
            GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        }
        private void OnDestroy()
        {
            videoPlayer.started -= VideoStarted;
            videoPlayer.loopPointReached -= VideoEnded;
            videoPlayer.seekCompleted -= VideoPlayerSeekCompleted;
            GalleryManager.OnVideoClipSelected -= UpdateCurrentVideo;
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        }


        //once current video is loaded,
        private void VideoLoaded(VideoPlayer source)
        {
            CheckAndPlayVideo();
        }


        private void GameManagerOnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.None:
                case GameState.MainMenu:
                case GameState.Gallery:
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





        private void OnSliderValueChanged()
        {
            var videoTime = videoInteractingSlider.value * videoPlayer.length;
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

            videoTimeText.text = (videoPlayer.time / 60).ToString("F2") + "/" + (videoPlayer.length / 60).ToString("F2");
            videoVisualingSlider.value = (float)(videoPlayer.time / videoPlayer.length);
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
                //Debug.Log(" ---" + GameManager.Instance.GetState());

                if (GameManager.Instance.GetState() == GameState.VideoPlayback)
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

        public void UpdateCurrentVideo(VideoClip video)
        {
            if (videoPlayer)
            {
                videoPlayer.clip = video;
                videoPlayer.Prepare();
            }
        }


        public void OnClickPlayButton()
        {
            //Debug.Log("OnClickPlayButton");
            if (isVideoEnded)
            {
                ResetVideoTime();
                Invoke("CheckAndPlayVideo", 0.2f);
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

        public void OnClickBackButton()
        {
            GameManager.Instance.SetState(GameState.Gallery);
        }
    }
}
