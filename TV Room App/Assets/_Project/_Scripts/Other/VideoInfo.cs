using UnityEngine;
using UnityEngine.Video;
namespace TV
{
    /// <summary>
    /// This script holds the video title, thumb image, and video clip.
    /// </summary>
    [System.Serializable]
    public struct VideoInfo
    {       
        public string title;
        public Sprite thumb;
        public VideoClip clip;
    }

}