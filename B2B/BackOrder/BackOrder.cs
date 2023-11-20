using B2B.Data;
using Business.Concrete;
using DataAccess.Abstract;
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


            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime? lastUpdateDate = Convert.ToDateTime( Encoding.UTF8.GetString(((byte[])_firmParameterService.Get(8))));
                DateTime updatedate = DateTime.Now;
                await _categoryService.updateCategory(lastUpdateDate);
                await _productService.updateProducts(lastUpdateDate);
                _priceListRepository.updatePrice(lastUpdateDate);
                _productAmountRepository.updateProducts(lastUpdateDate);
                _backOrderOder.SentData();
                _firmParameterService.Set(8, updatedate);
                var time = Convert.ToInt32(Encoding.UTF8.GetString((byte[])_firmParameterService.Get(18)));

                await Task.Delay(60000*time, stoppingToken); 
            }
        }
    }
}
