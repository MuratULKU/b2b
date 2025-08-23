namespace CoreUI.Components.Confirm
{
    public class ConfirmDialogService
    {
        /// <summary>
        /// Bu servis bir event üzerinden bileşene mesaj gönderir
        /// bileşen kendini bu evetne abone eder
        /// </summary>
        public event Func<string, string, TaskCompletionSource<bool>,Task>? OnShow;
        public Task<bool> Show(string title, string message)
        {
            var tcs = new TaskCompletionSource<bool>();
            OnShow?.Invoke(title, message, tcs);
            return tcs.Task;
        }
    }
}
