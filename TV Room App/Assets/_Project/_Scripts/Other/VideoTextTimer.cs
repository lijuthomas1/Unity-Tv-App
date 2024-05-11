using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace TV
{
    /// <summary>
    /// This 
    /// </summary>
    [RequireComponent (typeof (TMP_Text))]
    public class VideoTextTimerComponent : MonoBehaviour
    {
        private TMP_Text videoTimeText;
        private void Start ()
        {
            videoTimeText = GetComponent<TMP_Text> ();
            VideoPlayerController.OnVideoPlayingTimeChanged += UpdateTimerText;
        }
       private void UpdateTimerText(string text)
        {
            if (videoTimeText != null) videoTimeText.text = text;            
        }
    }
}
