using B2B.Components.Confirm;

namespace B2B.Components.Utilities
{
    public class BootstrapClassProvider
    {
        public string Modal() => "modal";
        public string ModalFade() => Fade();
        public string PageLoadingModal() => "modal-page-loading";
        public string Show() => "show";
        public string Fade() => "fade";
        public string ConfirmationModal() => "modal-confirmation";
        public string ToDialogSize(DialogSize size) =>
       size switch
       {
           DialogSize.Regular => null,
           DialogSize.Small => "modal-sm",
           DialogSize.Large => "modal-lg",
           DialogSize.ExtraLarge => "modal-xl",
           _ => null
       };

    }
}
