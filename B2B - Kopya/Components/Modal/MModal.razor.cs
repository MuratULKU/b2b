using B2B.Components.Utilities;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace B2B.Components.Modal
{
    public partial class MModal
    {
        private bool isVisible;
        private Type? childComponent = default!;
        private Dictionary<string, object> parameters = default!;
        private bool showFooterButton = false;
        private string footerButtonCSSClass = string.Empty;
        private ButtonColor footerButtonColor = ButtonColor.Secondary;
        private string footerButtonCancelText = string.Empty;
        private string footerButtonSaveText = string.Empty;
        private string modalSize => Size.ToModalSizeClass();
        private string scrollable => IsScrollable ? "modal-dialog-scrollable" : "";
        private string verticallyCentered => IsVerticallyCentered ? "modal-dialog-centered" : "";
        private bool showBackdrop;

        [Inject] private ModalService ModalService { get; set; } = default!;

        [Parameter]
        public bool IsServiceModal { get; set; }

        [Parameter]
        public string Title { get; set; } = default!;

        [Parameter]
        public string BodyCssClass { get; set; } = default!;

        [Parameter]
        public RenderFragment HeaderTemplate { get; set; } = default!;

        [Parameter]
        public bool ShowCloseButton { get; set; } = true;
        [Parameter]
        public string HeaderCssClass { get; set; } = default!;
       
        [Parameter]
        public string Message { get; set; } = default!;

        [Parameter]
        public RenderFragment BodyTemplate { get; set; } = default!;

        [Parameter]
        public RenderFragment FooterTemplate { get; set; } = default!;

        [Parameter]
        public string FooterCssClass { get; set; } = default!;

        [Parameter]
        public bool IsScrollable { get; set; }

        [Parameter]
        public bool IsVerticallyCentered { get; set; }

        [Parameter]
        public ModalSize Size { get; set; } = ModalSize.Regular;

        [Parameter]
        public string DialogCssClass { get; set; } = default!;

        [Parameter]
        public ModalType ModalType { get; set; } = ModalType.Light;

        [Parameter]
        public bool UseStaticBackdrop { get; set; }
        [Parameter]
        public bool CloseOnEscape { get; set; } = true;


        private Task OnShowAsync(ModalOption modalOption)
        {
            if (modalOption == null)
                throw new ArgumentNullException(nameof(modalOption));
            ModalType = modalOption.Type;
            Size = modalOption.Size;
            IsVerticallyCentered = modalOption.IsVerticallyCentered;
            showFooterButton = modalOption.ShowFooterButton;
            if (showFooterButton)
            {
                footerButtonColor = modalOption.FooterButtonColor;
                footerButtonCSSClass = modalOption.FooterButtonCSSClass;
                footerButtonCancelText = modalOption.FooterButtonCancelText;
                footerButtonSaveText = modalOption.FooterButtonSaveText;
                FooterCssClass = "border-top-0";
            }

            return ShowAsync(modalOption.Title, modalOption.Message, null, null);
        }
        public async Task ShowAsync<T>(string title, string? message = null, Dictionary<string, object>? parameters = null) => await ShowAsync(title, message, typeof(T), parameters);


        private async Task ShowAsync(string? title, string? message, Type? type, Dictionary<string, object>? parameters)
        {
            isVisible = true;
            if (!string.IsNullOrWhiteSpace(title))
                Title = title;
            if (!string.IsNullOrWhiteSpace(message))
                Message = message;
            childComponent = type;
            this.parameters = parameters!;
            showBackdrop = true;
            DirtyClasses();
            DirtyStyles();
            await InvokeAsync(StateHasChanged);
           
        }


        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override Task OnInitializedAsync()
        {
            if (ModalService is not null && IsServiceModal)
            {
                ModalService.OnShow += OnShowAsync;
            }
            return base.OnInitializedAsync();
        }

        protected override ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                if (ModalService is not null && IsServiceModal)
                    ModalService.OnShow -= OnShowAsync;
            }
            return base.DisposeAsync(disposing);
        }

        protected override void BuildClasses(CssClassBuilder builder)
        {
            builder.Append(BootstrapClassProvider.Modal());
           // builder.Append(BootstrapClassProvider.ConfirmationModal());
           // builder.Append(BootstrapClassProvider.ModalFade());
            base.BuildClasses(builder);
        }

        protected override void BuildStyles(CssStyleBuilder builder)
        {
            builder.Append("display:block", showBackdrop);
            builder.Append("display:none", !showBackdrop);
            base.BuildStyles(builder);
        }

        protected override bool ShouldAutoGenerateId => true;
        public async Task HideAsync()
        {
            isVisible = false;
            showBackdrop = false;
            DirtyClasses();
            DirtyStyles();
            await InvokeAsync(StateHasChanged);
        }

    }
}
