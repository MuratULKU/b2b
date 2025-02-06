namespace B2B.Components.Preload
{
    public static class Extentions
    {
        public static string ToSpinnerColor(this SpinnerColor color) =>
       color switch
       {
           SpinnerColor.Primary => "text-primary",
           SpinnerColor.Secondary => "text-secondary",
           SpinnerColor.Success => "text-success",
           SpinnerColor.Danger => "text-danger",
           SpinnerColor.Warning => "text-warning",
           SpinnerColor.Info => "text-info",
           SpinnerColor.Light => "text-light",
           SpinnerColor.Dark => "text-dark",
           _ => ""
       };
    }
}
