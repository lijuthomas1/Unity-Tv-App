using UnityEngine;
using UnityEngine.UI;

namespace TV
{
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


        public void OnStateChange(VideoState state)
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
