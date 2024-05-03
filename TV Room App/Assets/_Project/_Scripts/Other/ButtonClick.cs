using System;
using UnityEngine;
using UnityEngine.UI;

namespace TV
{
    /// <summary>
    /// Whenever clicking the Unity UI Button, it sends an event.
    /// This component should be attached to the Unity UI Button.
    /// The receivers can perform some action.
    /// </summary>
    public class ButtonClick : MonoBehaviour
    {
        private Button button;
        public static event Action OnButtonClick;

        private void Start()
        {
            button = GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    ButtonClicked();
                });
            }
        }
        private void ButtonClicked()
        {
            //Debug.Log("OnMouseDown");
            OnButtonClick?.Invoke();
        }
    }
}
