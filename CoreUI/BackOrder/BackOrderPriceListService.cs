using Business.Abstract;
using CoreUI.Data;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity;

namespace CoreUI.BackOrder
{
    public interface IBackOrderPriceListService
    {
        Task updatePrice(DateTime? date, HttpClient _httpClient);
        Task deletePrice();

    }
    public class BackOrderPriceListService : IBackOrderPriceListService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackOrderPriceListService> _logger;

        public BackOrderPriceListService(IServiceProvider serviceProvider, ILogger<BackOrderPriceListService> logger = null)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        public Task deletePrice()
        {
            using var scope = _serviceProvider.CreateScope();
            var _priceListRepository = scope.ServiceProvider.GetRequiredService<IPriceListRepository>();
          //  _priceListRepository.DeleteAll();
            return Task.CompletedTask;
        }
        public async Task updatePrice(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
               var _productRepository = scope.ServiceProvider.GetRequiredService<IProductServices>();
               var _priceRepository = scope.ServiceProvider.GetRequiredService<IPriceListService>();
                HttpResponseMessage respone;


                int currentpage = 1;
                int totalpage = 0;
                do
                {
                    respone = await _httpClient.GetAsync($"/api/v1/Products/pricelist?page={currentpage}&pageSize=10");
                    if (respone.IsSuccessStatusCode)
                    {
                        var pList = respone.Content.ReadFromJsonAsync<PageResult<PriceList>>().Result;
                        currentpage = pList.CurrentPage + 1;
                        totalpage = pList.TotalPages;
                        if (pList.Items.Count > 0)
                            foreach (var item in pList.Items)
                            {
                                var product = _productRepository.GetByLogicalref(item.Cardref);
                                if(product != null)
                                {
                                    var pricelist = _priceRepository.GetByLogicalref(item.Logicalref);
                                }
                            }
                    }
                } while (currentpage <= totalpage);

               
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                await Task.FromException(ex);
              
            }
        }
    }
}
