namespace B2B.Components.Preload
{
    public class PreloadService
    {
        internal event Action OnHide;
        internal event Action<SpinnerColor, string?> OnShow;

        public void Hide() => OnHide?.Invoke();

        public void Show(SpinnerColor spinnerColor = SpinnerColor.Light, string? loadingText = null) => OnShow?.Invoke(spinnerColor, loadingText);

        
    }
}
