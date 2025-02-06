

using B2B.Components.Modal;

namespace B2B.Components.Utilities
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

       
    }
}
