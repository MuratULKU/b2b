using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;


namespace B2B.BackOrder
{
    public interface IBackOrderProductAmountService
    {
        Task<bool> isApiActiveted(HttpClient _httpClient);
        Task updateProducts(DateTime? date, HttpClient _httpClient);
        Task deleteProducts();
    }
    public class BackOrderProductAmountService : IBackOrderProductAmountService
    {
        private readonly ILogger<BackOrderProductAmountService> _logger;

       
        private readonly IServiceProvider _serviceProvider;
        private IProductAmountRepository _productAmountRepository;
        private IProductRepository _productRepository;
        public BackOrderProductAmountService(ILogger<BackOrderProductAmountService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        public Task deleteProducts()
        {
            using var scope = _serviceProvider.CreateScope();
            _productAmountRepository = scope.ServiceProvider.GetRequiredService<IProductAmountRepository>();
            _productAmountRepository.DeleteAll();
            return Task.CompletedTask;
        }

        public async Task updateProducts(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                _productAmountRepository = scope.ServiceProvider.GetRequiredService<IProductAmountRepository>();
                _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                HttpResponseMessage respone;
                
                if (date.HasValue)
                    respone = await _httpClient.GetAsync($"/api/productsamount/{date.Value.ToString("MM/dd/yyyy")}");
                else
                    respone = await _httpClient.GetAsync($"/api/productsamount");
                if (respone.IsSuccessStatusCode)
                {
                    var pList = respone.Content.ReadFromJsonAsync<List<ProductAmount>>();
                    foreach (var productamount in pList.Result)
                    {
                        if (productamount is not null)
                        {
                            ProductAmount item = await _productAmountRepository.GetByLogicalref(productamount.StockRef);
                            Product product = await _productRepository.GetByLogicalref(productamount.StockRef);
                            if (product != null)
                            {
                                if (item is null)
                                {
                                    item = new();
                                    item.Id = Guid.NewGuid();
                                    item.StockRef = productamount.StockRef;
                                    item.ProductId = product.Id;
                                    item.OnHand = productamount.OnHand;
                                    _productAmountRepository.Insert(item);
                                }
                                else
                                {
                                    item.OnHand = productamount.OnHand;
                                    _productAmountRepository.Update(item);
                                }
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {


            }
            await Task.Delay(1);
        }

        public async Task<bool> isApiActiveted(HttpClient _httpClient)
        {
            try
            {
                var respone = await _httpClient.GetAsync("/api/getallproducts");
                if (respone.IsSuccessStatusCode)
                {

                    //iactionresult türünde data dönüşüne bakmak için

                }
            }
            catch
            {
                return false;
            }
            return false;

        }
    }

}
