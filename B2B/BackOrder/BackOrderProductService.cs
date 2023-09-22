using DataAccess.Abstract;
using Entity;


namespace B2B.BackOrder
{
    public interface IBackOrderProductService
    {
        Task<bool> isApiActiveted();
        void updateProducts(DateTime? date);

    }
    public class BackOrderProductService : IBackOrderProductService
    {
        private readonly ILogger<BackOrderProductService> _logger;

        private HttpClient _httpClient = new HttpClient();
        private readonly IServiceProvider _serviceProvider;
        private IProductRepository _productRepository;
        public BackOrderProductService(ILogger<BackOrderProductService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        public async void updateProducts(DateTime? date)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                HttpResponseMessage respone;
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri($"https://localhost:7079");
                if (date.HasValue)
                    respone = await _httpClient.GetAsync($"/api/products/{date.Value.ToString("MM/dd/yyyy HH:mm")}");
                else
                    respone = await _httpClient.GetAsync($"/api/products");
                if (respone.IsSuccessStatusCode)
                {
                    var pList = await respone.Content.ReadFromJsonAsync<List<Product>>();
                    foreach (var product in pList)
                    {
                        if (product.Code is not null && product.Name is not null)
                        {
                            Product item = await _productRepository.GetByCode(product.Code);
                            if (item is null)
                            {
                                _productRepository.Insert(product);
                            }
                            else
                            {
                                item.Name = product.Name;
                                item.Unit = product.Unit;
                                _productRepository.Update(item);
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {


            }
        }

        public async Task<bool> isApiActiveted()
        {
            try
            {
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri($"https://localhost:7079");
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
