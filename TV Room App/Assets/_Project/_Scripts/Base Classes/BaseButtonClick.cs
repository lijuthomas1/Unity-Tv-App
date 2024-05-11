using System;
using UnityEngine;
using UnityEngine.UI;

namespace TV
{
    /// <summary>
    /// Base Class Of All Button Click
    /// Whenever clicking the Unity UI Button, it sends an event.
    /// This component should be attached to the Unity UI Button.
    /// The receivers can perform some action.
    /// </summary>
   [RequireComponent(typeof(Button))]
    public abstract class BaseButtonClick : MonoBehaviour
    {
        private Button button;       
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
       protected abstract void ButtonClicked();
    }
}