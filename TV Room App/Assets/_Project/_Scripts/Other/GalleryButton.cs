using System;
namespace TV
{
    /// <summary>
    /// Gallery selecting button
    /// </summary>
    public class GalleryButton : BaseButtonClick
    {
        public static event Action OnButtonClick;
        protected override void ButtonClicked()
        {
            OnButtonClick?.Invoke();
        }
    }
}
