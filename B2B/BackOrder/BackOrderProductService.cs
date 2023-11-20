using DataAccess.Abstract;
using Entity;
using Humanizer;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Helpers;


namespace B2B.BackOrder
{
    public interface IBackOrderProductService
    {
        Task<bool> isApiActiveted();
        Task updateProducts(DateTime? date);

    }
    public class BackOrderProductService : IBackOrderProductService
    {
        private readonly ILogger<BackOrderProductService> _logger;

        private HttpClient _httpClient = new HttpClient();
        private readonly IServiceProvider _serviceProvider;
        private IProductRepository _productRepository;
        private IFirmDocRepository _firmDocRepository;
        public BackOrderProductService(ILogger<BackOrderProductService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        public async Task updateProducts(DateTime? date)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                _firmDocRepository = scope.ServiceProvider.GetRequiredService<IFirmDocRepository>();
                HttpResponseMessage respone;
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri($"https://localhost:7079");
                if (date.HasValue)
                    respone = await _httpClient.GetAsync($"/api/products/{date.Value.ToString("MM/dd/yyyy HH:mm")}");
                else
                    respone = await _httpClient.GetAsync($"/api/products");
                if (respone.IsSuccessStatusCode)
                {
                 
                    var pList = respone.Content.ReadFromJsonAsync<List<Product>>();
                    foreach (var product in pList.Result)
                    {
                        if (product.Code is not null && product.Name is not null)
                        {
                            Product item = await _productRepository.GetByCode(product.Code);
                            if (item is null)
                            {
                                item = product;
                                item.Id = Guid.NewGuid();
                                _productRepository.Insert(item);
                            }
                            else
                            {
                                if (product.firmDocs.Count > 0)
                                {
                                    if (item.firmDocs.Count <= 1)
                                    {
                                        item.firmDocs.Add(product.firmDocs[0]);
                                    }
                                    else
                                    {
                                        item.firmDocs[0].LData = product.firmDocs[0].LData;
                                    }
                                }
                                item.PriceLists = product.PriceLists;
                                item.Name = product.Name;
                                item.Vat = product.Vat;
                                item.SellVat = product.SellVat;
                                item.StGrupCode = product.StGrupCode;
                                item.SpeCode = product.SpeCode;
                                item.SpeCode2 = product.SpeCode2;
                                item.SpeCode3 = product.SpeCode3;
                                item.SpeCode4 = product.SpeCode4;
                                item.SpeCode5 = product.SpeCode5;
                                item.ParentRef = product.ParentRef;
                                _productRepository.Update(item);
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
