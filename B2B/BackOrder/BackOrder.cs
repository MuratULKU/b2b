using B2B.Data;
using Business.Concrete;
using DataAccess.Abstract;
using Newtonsoft.Json.Linq;
using System.Text;

namespace B2B.BackOrder
{
    public class BackOrder : BackgroundService
    {
        private readonly ILogger<BackOrder> _logger;
        private readonly IBackOrderProductService _productService;
        private readonly IBackOrderCategoryService _categoryService;
        private readonly FirmParameterService _firmParameterService;
        private readonly IBackOrderPriceListService _priceListRepository;
        private readonly IBackOrderProductAmountService _productAmountRepository;
        private readonly IBackOrderOder _backOrderOder;


        public BackOrder(ILogger<BackOrder> logger, IBackOrderProductService productService, IBackOrderCategoryService categoryService, FirmParameterService firmParameterService, IBackOrderPriceListService priceListRepository, IBackOrderProductAmountService productAmountRepository, IBackOrderOder backOrderOder)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _firmParameterService = firmParameterService;
            _priceListRepository = priceListRepository;
            _productAmountRepository = productAmountRepository;
            _backOrderOder = backOrderOder;
        }


        public IBackOrderProductService OrderProductService { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            HttpClient _httpClient = new HttpClient();
            using StreamReader openStream = new StreamReader("appsettings.json");
            string json = openStream.ReadToEnd();
            dynamic appsetting = JObject.Parse(json);
            _httpClient.BaseAddress = appsetting.ApiService.Url;
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime? lastUpdateDate = Convert.ToDateTime(Encoding.UTF8.GetString(((byte[])_firmParameterService.Get(8))));
                DateTime updatedate = DateTime.Now;

                if (Encoding.UTF8.GetString(((byte[])_firmParameterService.Get(22))) == "True")
                {
                    await _priceListRepository.deletePrice();
                    await _productAmountRepository.deleteProducts();
                    if (_categoryService.deleteCategory().IsCompleted)
                        await _productService.deleteProducts();
                    lastUpdateDate = null;
                }
                await _categoryService.updateCategory(lastUpdateDate, _httpClient);
                await _productService.updateProducts(lastUpdateDate, _httpClient);

                await _priceListRepository.updatePrice(lastUpdateDate, _httpClient);
                await _productAmountRepository.updateProducts(lastUpdateDate, _httpClient);

                _backOrderOder.SentData(_httpClient);
                _firmParameterService.Set(8, updatedate);
                _firmParameterService.Set(22, "False");
                var time = Convert.ToInt32(Encoding.UTF8.GetString((byte[])_firmParameterService.Get(18)));

                await Task.Delay(60000 * time, stoppingToken);
            }
        }
    }
}
