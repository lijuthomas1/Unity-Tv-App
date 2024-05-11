using System;
namespace TV
{
    /// <summary>
    /// Derived from BaseButtonClick for handling buttonclick sound
    /// </summary>
    public class ButtonClick : BaseButtonClick
    {
        public static event Action OnButtonClick;
        protected override void ButtonClicked()
        {
            OnButtonClick?.Invoke();
        }
    }
}
