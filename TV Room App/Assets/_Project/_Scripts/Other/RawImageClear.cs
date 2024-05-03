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
            GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        }
        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        }


        private void GameManagerOnGameStateChanged(GameState state)
        {
            switch (state)
            {
                case GameState.Gallery:
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
