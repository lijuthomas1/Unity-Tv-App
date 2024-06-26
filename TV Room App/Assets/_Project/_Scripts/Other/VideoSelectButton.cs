using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TV
{
    /// <summary>
    /// This script should be attached to the video button,
    /// it updates the title and thumb of the video button.
    /// </summary>
    public class VideoSelectButton : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text titleText;
        [SerializeField]
        private Image thumbImage;
        private void UpdateTitle(string title)
        {
            if (titleText != null)titleText.text = title;
        }
        private void UpdateThumbImage(Sprite sprite)
        {
            if (thumbImage != null) thumbImage.sprite = sprite;
        }
       public void UpdateVideoButtonDetails(string title, Sprite thumbImage)
        {
            UpdateTitle(title);
            UpdateThumbImage(thumbImage);
        }
    }
}
