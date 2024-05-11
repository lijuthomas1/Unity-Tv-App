using UnityEngine;
using UnityEngine.UI;

namespace TV
{
    /// <summary>
    /// This script manages the images of the video player button based on the video state.
    /// It receives data from the Video Player Controller script.
    /// </summary>
    public class VideoPlayerButton : MonoBehaviour
    {
        [SerializeField] private Sprite playSprite;
        [SerializeField] private Sprite pauseSprite;
        [SerializeField] private Sprite retrySprite;
       [SerializeField] private Image thisImage;
       private void Start ()
        {
            thisImage = this.GetComponent<Image>();
        }
        private void OnEnable()
        {
            VideoPlayerController.OnVideoStateChanged += OnStateChange;
            thisImage = this.GetComponent<Image>();
        }
        private void OnDestroy()
        {
            VideoPlayerController.OnVideoStateChanged -= OnStateChange;
        }
       private void UpdateImage(Sprite newSprite)
        {
            if (thisImage != null)
            {
                thisImage.sprite = newSprite;
            }
        }
       private void OnStateChange(VideoState state)
        {
            //Debug.Log(state);
            switch (state)
            {
                case VideoState.None:
                    break;
                case VideoState.Playing:
                    UpdateImage(pauseSprite);
                    break;
                case VideoState.Paused:
                    UpdateImage(playSprite);
                    break;
                case VideoState.Ended:
                    UpdateImage(retrySprite);
                    break;
            }
        }
    }
}
