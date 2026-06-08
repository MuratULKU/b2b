
using CoreUI.BackOrder.Mikro;
using CoreUI.Data;
using Entity;

namespace CoreUI.BackOrder
{
    public class MikroApiStrategy : IApiStrategy
    {
        private readonly IMikroClientService _clientService;
        private readonly IMikroProductService _productService;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public MikroApiStrategy(IMikroClientService clientService, IMikroProductService productService)
        {
            _clientService = clientService;
            _productService = productService;
        }

        public async Task<List<ClientFiche>> GetExtre(int currentPage, int pageSize, string clientCode, DateTime firstDate, DateTime lastDate)
        {
           return await _clientService.GetExtre(currentPage, pageSize, clientCode,firstDate,lastDate);
        }

        public string GetHashAsync(string input)
        {
            return Md5Helper.ComputeMd5Base64("1984-01-01 12)");
        }

        public async Task RunAsync(DateTime lastUpdateCheck)
        {
            if (!await _semaphore.WaitAsync(0)) // 0 = beklemeden kontrol et
            {
             
                return;
            }

            try
            {
                await _clientService.UpdateClient(lastUpdateCheck);
                await _productService.UpdateProduct(lastUpdateCheck);
            }
            finally
            {
                _semaphore.Release(); // ✅ Hata olsa bile mutlaka serbest bırak
            }
        }

        public async Task SendAsync(DateTime lastUpdateCheck)
        {
            await _clientService.SentData ();
        }
    }
}
