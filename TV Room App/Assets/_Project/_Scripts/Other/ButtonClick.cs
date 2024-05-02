using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TV
{
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
