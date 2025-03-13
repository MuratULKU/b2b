
using Business.SingletonServices;
using CoreUI.Data;
using System.Text;
namespace CoreUI.BackOrder
{
    public class BackOrder : BackgroundService
    {
        private readonly ILogger<BackOrder> _logger;
        private readonly IBackOrderProductService _productService;
        private readonly FirmParameter _firmParameterService;
        private readonly IBackOrderClientService _clientService;
        private readonly IBackOrderOder _backOrderOder;
        private readonly HttpClient _httpClient;

        public BackOrder(ILogger<BackOrder> logger,HttpClient httpClient ,IBackOrderProductService productService,
             IBackOrderClientService clientCardService,
            FirmParameter firmParameterService, IConfiguration configuration, IBackOrderOder backOrderOder)
        {
            _logger = logger;
            _productService = productService;
            _clientService = clientCardService;
            _firmParameterService = firmParameterService;
            _configuration = configuration;
            _backOrderOder = backOrderOder;
            _httpClient = httpClient;
        }


        public IBackOrderProductService OrderProductService { get; set; }

        private IConfiguration _configuration;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {
                _logger.LogInformation("Started Connection Api");

                _httpClient.BaseAddress = new Uri(_configuration.GetSection("ApiService").GetSection("Url").Value);
                while (!stoppingToken.IsCancellationRequested)
                {
                    _backOrderOder.SentData(_httpClient); 
                    _clientService.SentData(_httpClient);
                    await _backOrderOder.OrderFicheState(_httpClient);
                    _logger.LogInformation("SendData");
                    DateTime? lastUpdateDate = Convert.ToDateTime(_firmParameterService.ToString(8));
                    string updatedate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    if (_firmParameterService.ToString(27) == "True")
                    {
                        _firmParameterService.Set(27, "False");
                        await _productService.CharAsgnDeleteAll();
                        await _productService.CharCodeDeleteAll();
                        await _productService.CharSetDeleteAll();
                        await _productService.PriceListDeleteAll();
                        await _productService.ProductAmountDeleteAll();
                        await _productService.CategoriesDeleteAll();
                        await _productService.deleteProducts();
                        lastUpdateDate = null;
                        _logger.LogInformation("Tables deleted");
                    }
                    //   var deleteproduct = _productService.DeleteProduct(lastUpdateDate, _httpClient);
                    await _productService.CharSetUpdate(lastUpdateDate, _httpClient); //_charSetRepository.updateCharSets(lastUpdateDate, _httpClient);
                    var category = await _productService.CategoryUpdate(lastUpdateDate, _httpClient);
                    if (category)
                    {
                        var product = await _productService.updateProducts(lastUpdateDate, _httpClient);
                        if (product)
                        {
                            await _productService.PriceListUpdate(lastUpdateDate, _httpClient);
                            await _productService.ProductAmountUpdate(lastUpdateDate, _httpClient);
                        }
                    }
                    _logger.LogInformation("Products tables updated");
                    //ürünlerle eşeleştirilme yapıldığı için ürünlerden sonra yüklenecek



                    var charCode = await _productService.CharCodeUpdate(lastUpdateDate, _httpClient);
                    var charAsgn = await _productService.CharAsgnUpdate(lastUpdateDate, _httpClient);

                    //silinmiş ürünleri veri tabanından sil


                    if (_firmParameterService.Get(27) == "True")
                    {
                        await _productService.DeleteImages();
                        await _productService.updateImages(_httpClient);
                        _firmParameterService.Set(27, "False");
                    }
                    _logger.LogInformation("Image table updated");

                    if (category)
                    {
                        _firmParameterService.Set(8, updatedate);
                        _firmParameterService.Set(22, "False");
                    }
                    _logger.LogInformation("All Tables Updated.");

                    await _clientService.UpdateClient(lastUpdateDate, _httpClient);

                    var time = Convert.ToInt32(_firmParameterService.ToString(18));
                    _logger.LogCritical($"Completed Connection Api...{DateTime.Now}");
                    await Task.Delay(60000 * time, stoppingToken);

                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

}
