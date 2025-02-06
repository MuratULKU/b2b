
using Core.Enum;


namespace CoreUI.Components.Base
{
    public static class EnumExtensions
    {
        public static string ToButtonClass(this ButtonColor buttonColor) =>
       buttonColor switch
       {
           ButtonColor.Primary => "btn btn-primary",
           ButtonColor.Secondary => "btn btn-secondary",
           ButtonColor.Success => "btn btn-success",
           ButtonColor.Danger => "btn btn-danger",
           ButtonColor.Warning => "btn btn-warning",
           ButtonColor.Info => "btn btn-info",
           ButtonColor.Light => "btn btn-light",
           ButtonColor.Dark => "btn btn-dark",
           ButtonColor.Link => "btn btn-link",
           _ => "btn btn-primary"
       };

        public static string ToModalSizeClass(this ModalSize modalSize) =>
        modalSize switch
        {
            ModalSize.Regular => "",
            ModalSize.Small => "modal-sm",
            ModalSize.Large => "modal-lg",
            ModalSize.ExtraLarge => "modal-xl",
            _ => ""
        };

        public static string ToModalFullscreenClass(this ModalFullscreen modalFullscreen) =>
        modalFullscreen switch
        {
            ModalFullscreen.Disabled => "",
            ModalFullscreen.Always => "modal-fullscreen",
            ModalFullscreen.SmallDown => "modal-fullscreen-sm-down",
            ModalFullscreen.MediumDown => "modal-fullscreen-md-down",
            ModalFullscreen.LargeDown => "modal-fullscreen-lg-down",
            ModalFullscreen.ExtraLargeDown => "modal-fullscreen-xl-down",
            ModalFullscreen.ExtraExtraLargeDown => "modal-fullscreen-xxl-down",
            _ => ""
        };

    }
}
