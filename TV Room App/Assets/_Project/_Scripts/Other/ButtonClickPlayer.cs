using UnityEngine;
namespace TV
{
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
