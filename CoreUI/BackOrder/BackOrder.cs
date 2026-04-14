
using Business.Abstract;
using Business.Concrete;
using Business.SingletonServices;
using CoreUI.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using static System.Formats.Asn1.AsnWriter;
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
        private readonly IServiceProvider _serviceProvider;

        private DateTime _lastUpdateCheck = DateTime.MinValue;
        private DateTime _lastSendCheck = DateTime.MinValue;
        private DateTime _tokenExpireTime = DateTime.MinValue;
        public BackOrder(ILogger<BackOrder> logger, HttpClient httpClient, IBackOrderProductService productService,
             IBackOrderClientService clientCardService,
            FirmParameter firmParameterService, IConfiguration configuration, IBackOrderOder backOrderOder, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _productService = productService;
            _clientService = clientCardService;
            _firmParameterService = firmParameterService;
            _configuration = configuration;
            _backOrderOder = backOrderOder;
            _httpClient = httpClient;
            _serviceProvider = serviceProvider;
        }


        public IBackOrderProductService OrderProductService { get; set; }

        private IConfiguration _configuration;

        private async Task CheckServiceUpdate()
        {
            try
            {
                var version = Assembly
                    .GetExecutingAssembly()
                    .GetName()
                    .Version
                    .ToString();

                var request = new
                {
                    ApplicationName = "MyService",
                    CurrentVersion = version
                };

                var response = await _httpClient.PostAsJsonAsync(
                    "api/update/check",
                    request);

                if (!response.IsSuccessStatusCode)
                    return;

                var result = await response.Content.ReadFromJsonAsync<UpdateCheckResponse>();

                if (result != null && result.HasUpdate)
                {
                    _logger.LogWarning($"Yeni versiyon bulundu : {result.Version}");

                    await DownloadUpdate(result.DownloadUrl);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Update kontrol hatası : " + ex.Message);
            }
        }

        private async Task DownloadUpdate(string url)
        {
            string path = @"C:\ServiceUpdate\update.zip";

            var bytes = await _httpClient.GetByteArrayAsync(url);

            Directory.CreateDirectory(@"C:\ServiceUpdate");

            await File.WriteAllBytesAsync(path, bytes);

            Process.Start(new ProcessStartInfo
            {
                FileName = "ServiceUpdater.exe",
                Arguments = path,
                UseShellExecute = true
            });

            Environment.Exit(0);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {
                _logger.LogInformation("Started Connection Api");
                _httpClient.BaseAddress = new Uri(_configuration.GetSection("ApiService").GetSection("Url").Value);
                using var scope = _serviceProvider.CreateScope();
                var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
               
              
                while (!stoppingToken.IsCancellationRequested)
                {
                    // Saatte bir update kontrolü
                    if (DateTime.Now >= _tokenExpireTime)
                    {
                        _logger.LogInformation("Token refreshing...");

                        var tokenResponse = await tokenService.GetToken(_httpClient);

                        _httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", tokenResponse);

                        _tokenExpireTime = DateTime.Now.AddMinutes(55);
                    }


                    if ((DateTime.Now - _lastUpdateCheck).TotalHours >= 1)
                    {
                        await CheckServiceUpdate();
                        _lastUpdateCheck = DateTime.Now;
                    }
                    if ((DateTime.Now - _lastSendCheck).TotalMinutes >= 1)
                    {
                        DateTime? lastUpdateDate = Convert.ToDateTime(_firmParameterService.ToString(8));

                        _backOrderOder.SentData(_httpClient);
                        _clientService.SentData(_httpClient);
                        await _backOrderOder.OrderFicheState(lastUpdateDate,_httpClient);
                        _logger.LogInformation("SendData");
                      
                        string updatedate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        if (_firmParameterService.ToString(22) == "True")
                        {
                            _firmParameterService.Set(22, "False");
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


                        if (_firmParameterService.ToString(27) == "True")
                        {
                            await _productService.DeleteImages();
                            await _productService.updateImages(lastUpdateDate,_httpClient);
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
                        _logger.LogCritical($"Completed Connection Api...{lastUpdateDate.Value.ToString()} -- {DateTime.Now}");
                        _lastSendCheck = DateTime.Now;
                    }
                  
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
            }

            
        }
    }

}
