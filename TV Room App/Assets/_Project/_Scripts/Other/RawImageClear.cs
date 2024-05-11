using UnityEngine;
using UnityEngine.UI;
namespace TV
{
    /// <summary>
    /// This script clears the Raw Image texture whenever going back to the gallery mode.
    /// This should be attached to the video player's raw image.
    /// </summary>
    public class RawImageClear : MonoBehaviour
    {
        private RawImage rawImage;
       private void Start()
        {
            rawImage = GetComponent<RawImage>();
            TvManager.OnTvStateChanged += GameManagerOnTvStateChanged;
        }
        private void OnDestroy()
        {
            TvManager.OnTvStateChanged -= GameManagerOnTvStateChanged;
        }
       private void GameManagerOnTvStateChanged(TvState state)
        {
            switch (state)
            {
                case TvState.Gallery:
                    ClearRowimage();
                    break;
            }
       }
       private void ClearRowimage()
        {
            if( rawImage != null) rawImage.texture = null;
        }
    }
}
