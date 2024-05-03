using UnityEngine;

namespace TV
{
    /// <summary>
    /// The UI manager handles all the main UI selections of the game.
    /// Based on the GameState this script does some action.
    /// </summary>
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject tapOnTvPanel;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject galleryPanel;
        [SerializeField] private GameObject videoPlayerPanel;
        private void Awake()
        {
            GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        }
        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        }


        private void GameManagerOnGameStateChanged(GameState state)
        {            
            ShowCurrentPanel();
        }

        

        private void ShowOrHideTvOnPanel(bool canShow = false)
        {
            tapOnTvPanel.SetActive(canShow);
        }

        private void ShowOrHideMainMenuPanel(bool canShow = false)
        {
            mainMenuPanel.SetActive(canShow);
        }
        private void ShowOrHideGalleryPanel(bool canShow = false)
        {
            galleryPanel.SetActive(canShow);
        }
        private void ShowOrHideVideoPanel(bool canShow = false)
        {
            videoPlayerPanel.SetActive(canShow);
        }

        private void ShowCurrentPanel()
        {
            //Debug.Log(GameManager.Instance.GetState());
            ShowOrHideTvOnPanel(GameManager.Instance.GetState() == GameState.None);
            ShowOrHideMainMenuPanel(GameManager.Instance.GetState() == GameState.MainMenu);
            ShowOrHideGalleryPanel(GameManager.Instance.GetState() == GameState.Gallery);
            ShowOrHideVideoPanel(GameManager.Instance.GetState() == GameState.VideoPlayback);
        }

       
        public void OnClickGalleryButton()
        {
            GameManager.Instance.SetState(GameState.Gallery);
            ShowCurrentPanel();
        }

        public void OnClickTvOnPanel()
        {
            GameManager.Instance.SetState(GameState.MainMenu);
            ShowCurrentPanel();
        }
    }
}
