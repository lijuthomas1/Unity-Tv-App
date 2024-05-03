using UnityEngine;
namespace TV
{
    /// <summary>
    /// This script receives the button click event from the ButtonClick script 
    /// and plays button click audio. 
    /// </summary>
    public class ButtonClickPlayer : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            ButtonClick.OnButtonClick += PlayButtonClick;
        }
        private void OnDestroy()
        {
            ButtonClick.OnButtonClick -= PlayButtonClick;
        }

        private void PlayButtonClick()
        {
            if(audioSource != null) audioSource.Play();
        }
    }
}
