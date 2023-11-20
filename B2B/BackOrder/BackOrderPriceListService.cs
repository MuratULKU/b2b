using DataAccess.Abstract;
using Entity;

namespace B2B.BackOrder
{
    public interface IBackOrderPriceListService
    {
    void updatePrice(DateTime? date);

    }
    public class BackOrderPriceListService : IBackOrderPriceListService
    {
        private HttpClient _httpClient = new HttpClient();
        private readonly IServiceProvider _serviceProvider;

        public BackOrderPriceListService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async void updatePrice(DateTime? date)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _priceListRepository = scope.ServiceProvider.GetRequiredService<IPriceListRepository>();
                var _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                HttpResponseMessage respone;
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri($"https://localhost:7079");
                if (date.HasValue)
                    respone = await _httpClient.GetAsync($"/api/pricelist/{date.Value.ToString("MM/dd/yyyy HH:mm")}");
                else
                    respone = await _httpClient.GetAsync($"/api/pricelist");
                if (respone.IsSuccessStatusCode)
                {
                    var pList = await respone.Content.ReadFromJsonAsync<List<PriceList>>();
                    foreach (var price in pList)
                    {
                        if (price.Code is not null)
                        {
                            Product product = await _productRepository.GetByLogicalref(price.Cardref);
                            if (product != null)
                            {
                                PriceList item = await _priceListRepository.GetByCode(
                                    price.Code);
                                if (item is null)
                                {
                                    item = new PriceList();
                                    item.Code = price.Code;
                                    item.Cardref = price.Cardref;
                                    item.Id = Guid.NewGuid();
                                    item.ProductCode = product.Code;
                                    item.Name = price.Name;
                                    item.Currency = price.Currency;
                                    item.Priorty = price.Priorty;
                                    item.Price = price.Price;
                                    item.ProductId = product.Id;
                                    _priceListRepository.Insert(item);
                                }
                                else
                                {
                                    item.Price = price.Price;
                                    _priceListRepository.Update(item);

                                }
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {


            }
        }
    }
}
