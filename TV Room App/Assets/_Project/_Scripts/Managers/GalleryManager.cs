using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;
namespace TV
{
    public class GalleryManager : MonoBehaviour
    {
        [SerializeField]
        private List<VideoInfo> videoList = new List<VideoInfo>();
        [SerializeField]
        private GameObject buttonPrefab;
        [SerializeField]
        private GameObject buttonParent;
        [SerializeField]
        private VideoPlayerController videoPlayerController;
        public static event Action<VideoClip> OnVideoClipSelected;

        private void OnEnable()
        {
            for (int i = 0; i < videoList.Count; i++)
            {
                var buttonIndex = i;
                GameObject childObject = Instantiate<GameObject>(buttonPrefab, buttonParent.transform);
                childObject.GetComponent<VideoSelectButton>().UpdateVideoButtonDetails(videoList[i].title, videoList[i].thumb);
                childObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    OnClickVideoButton(buttonIndex);
                });


            }
        }

        private void UpdateVideoOnVideoPlayer(VideoClip videoClip)
        {
            if (videoPlayerController != null && videoClip != null)
            {
                OnVideoClipSelected?.Invoke(videoClip);
            }
        }

        private void OnClickVideoButton(int buttonIndex)
        {
            //Debug.Log("buttonIndex "+ buttonIndex);
            if(buttonIndex >= 0 && buttonIndex<= videoList.Count - 1)
            {
                UpdateVideoOnVideoPlayer(videoList[buttonIndex].clip);
                GameManager.Instance.SetState(GameState.VideoPlayback);
            }
        }

    }

}