namespace CoreUI.Components.LoadingService
{
    public class LoadingService
    {
        public event Action<string> OnShow;
        public event Action OnHide;
        public event Action<int> OnProgressChanged;
        public int ProgressBarMax { get; set; } = 100;
        public bool ProgressbarShow { get; set; } = true;


        public async Task ShowAync(string message = "Yükleniyor")
        {
            OnShow?.Invoke(message);
            await Task.Yield(); 
        }

      
        public void Show(string message = "Yükleniyor Lütfen Bekleyiniz")
        {
            OnShow?.Invoke(message);
        }
        public void Hide()
        {
            OnHide?.Invoke();
        }

      
        public void SetProgress(int progress)
        {
            OnProgressChanged?.Invoke(progress);
        }
    }
}
