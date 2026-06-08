
using CoreUI.BackOrder.Ulku;
using CoreUI.Data;

namespace CoreUI.BackOrder
{
    public class UlkuApiStrategy : IApiStrategy
    {
        private readonly IBackOrderClientService _clientService;
        private readonly IBackOrderProductService _productService;
        private readonly IBackOrderOder _orderService;

        public UlkuApiStrategy(IBackOrderClientService clientService, IBackOrderProductService productService, IBackOrderOder orderService = null)
        {
            _clientService = clientService;
            _productService = productService;
            _orderService = orderService;
        }

        public Uri BaseUrl => throw new NotImplementedException();

        public async Task<List<ClientFiche>> GetExtre(int currentPage, int pageSize, string clientCode, DateTime firstDate, DateTime lastDate)
        {
            return await _clientService.GetExtre(currentPage, pageSize, clientCode, firstDate, lastDate);
        }

        public string GetHashAsync(string input)
        {
            return input;
        }

        public async Task RunAsync(DateTime lastUpdateCheck)
        {
           await _productService.CharAsgnUpdate(lastUpdateCheck);
            await _productService.CharCodeUpdate(lastUpdateCheck);
            await _productService.CharSetUpdate(lastUpdateCheck);
            await _clientService.UpdateClient(lastUpdateCheck);
            await _productService.updateProducts(lastUpdateCheck);
            await _productService.PriceListUpdate(lastUpdateCheck);
            await _productService.ProductAmountUpdate(lastUpdateCheck);
            await _productService.CategoryUpdate(lastUpdateCheck);
            

        }

        public async Task SendAsync(DateTime lastUpdateCheck)
        {
            await _clientService.SentData();
            await _orderService.SentData();
        }
    }
}
