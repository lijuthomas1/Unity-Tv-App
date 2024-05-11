using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TV
{
   [RequireComponent (typeof (Slider))]
    public class VideoInteractiveSlider : MonoBehaviour
    {
        public static event Action<float> OnValueChanged;
        private Slider slider;
        private void Start()
        {
           slider = GetComponent<Slider> ();
           slider.onValueChanged.AddListener(OnSliderMoved);
        }
        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(OnSliderMoved);
        }
       private void OnSliderMoved(float value)
        {
            OnValueChanged?.Invoke(slider.value);   
        }
    }
}
