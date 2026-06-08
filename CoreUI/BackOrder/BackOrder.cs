
using Business.Abstract;
using Business.Concrete;
using Business.SingletonServices;
using Core.Logger;
using CoreUI.BackOrder.Ulku;
using CoreUI.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using static System.Formats.Asn1.AsnWriter;
namespace CoreUI.BackOrder
{
    public class BackOrder : BackgroundService
    {
        private readonly ILoggerService _logger;
        private readonly IBackOrderProductService _productService;
        private readonly FirmParameter _firmParameterService;
        private readonly IBackOrderClientService _clientService;
        private readonly IBackOrderOder _backOrderOder;
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;

        private DateTime _lastUpdateCheck = DateTime.MinValue;
        private DateTime _lastSendCheck = DateTime.MinValue;


        private readonly ApiStrategyFactory _strategyFactory;

        public BackOrder(ILoggerService logger, HttpClient httpClient, IBackOrderProductService productService,
             IBackOrderClientService clientCardService,
            FirmParameter firmParameterService, IConfiguration configuration, IBackOrderOder backOrderOder, IServiceProvider serviceProvider, ApiStrategyFactory strategyFactory = null)
        {
            _logger = logger;
            _productService = productService;
            _clientService = clientCardService;
            _firmParameterService = firmParameterService;
            _configuration = configuration;
            _backOrderOder = backOrderOder;
            _httpClient = httpClient;
            _serviceProvider = serviceProvider;
            _strategyFactory = strategyFactory;
            
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
                    _logger.Warn($"Yeni versiyon bulundu : {result.Version}");

                    await DownloadUpdate(result.DownloadUrl);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Update kontrol hatası : " + ex.Message);
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

            _logger.Info("Started Connection Api");
           

            var strategy = _strategyFactory.GetStrategy();
            _lastUpdateCheck = Convert.ToDateTime(_firmParameterService.ToString(8));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var updateDate = DateTime.Now;

                    if (_firmParameterService.ToString(22) == "True")
                    {
                        // await _clientService.DeleteAll();
                    }

                    if ((DateTime.Now - _lastSendCheck).TotalMinutes > 1)
                    {
                        try
                        {
                            await strategy.SendAsync(_lastSendCheck);
                            _lastSendCheck = updateDate; 
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("SendAsync hata: " + ex.Message);
                        }
                    }

                    if ((DateTime.Now - _lastUpdateCheck).TotalMinutes > 10)
                    {
                        try
                        {
                            await strategy.RunAsync(_lastUpdateCheck);

                            _lastUpdateCheck = updateDate;
                            _firmParameterService.Set(8, updateDate);

                            _logger.Info($"Completed Connection Api...{DateTime.Now}");
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("RunAsync hata: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                  
                    _logger.Error("Loop genel hata: " + ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }

}
