using System;
namespace TV
{
    /// <summary>
    /// Tap on tv button
    /// </summary>
    public class TapOnTvButton : BaseButtonClick
    {
        public static event Action OnButtonClick;
        protected override void ButtonClicked()
        {
            OnButtonClick?.Invoke();
        }
    }
}
