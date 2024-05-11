using UnityEngine;

namespace TV
{
    /// <summary>
    /// The TvMenuManager handles all the TV UI selections of the game.
    /// Based on the TvState this script does some action.
    /// </summary>
    public class TvMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject tapOnTvPanel;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject galleryPanel;
        [SerializeField] private GameObject videoPlayerPanel;
        private void Awake()
        {
            TvManager.OnTvStateChanged += GameManagerOnTvStateChanged;
            GalleryButton.OnButtonClick += OnClickGalleryButton;
            TapOnTvButton.OnButtonClick += OnClickTvOnPanel;
        }
        private void OnDestroy()
        {
            TvManager.OnTvStateChanged -= GameManagerOnTvStateChanged;
            GalleryButton.OnButtonClick -= OnClickGalleryButton;
            TapOnTvButton.OnButtonClick -= OnClickTvOnPanel;
        }
       private void GameManagerOnTvStateChanged(TvState state)
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
            //Debug.Log(TvManager.Instance.GetState());
            ShowOrHideTvOnPanel(TvManager.Instance.GetState() == TvState.None);
            ShowOrHideMainMenuPanel(TvManager.Instance.GetState() == TvState.MainMenu);
            ShowOrHideGalleryPanel(TvManager.Instance.GetState() == TvState.Gallery);
            ShowOrHideVideoPanel(TvManager.Instance.GetState() == TvState.VideoPlayback);
        }
       private void OnClickGalleryButton()
        {
            TvManager.Instance.SetState(TvState.Gallery);
            ShowCurrentPanel();
        }
       private void OnClickTvOnPanel()
        {
            TvManager.Instance.SetState(TvState.MainMenu);
            ShowCurrentPanel();
        }
    }
}
