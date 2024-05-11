using UnityEngine;
using UnityEngine.UI;

namespace TV
{
    /// <summary>
    /// This script helps to visualize the video timer.
    /// Video time shows with the help of a slider.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class VideoPlaySliderVisualizer : MonoBehaviour
    {
        private Slider slider;
        private void Start()
        {
            slider = GetComponent<Slider>();
            VideoPlayerController.OnVideoTimeChanged += UpdateSliderValue;
        }
        private void UpdateSliderValue(float value)
        {
            slider.value = value;
        }
    }
}
