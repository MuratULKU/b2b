using B2B.Data;
using DataAccess.Abstract;

namespace B2B.BackOrder
{
    public class BackOrder : BackgroundService
    {
        private readonly ILogger<BackOrder> _logger;
        private readonly IBackOrderProductService _productService;
        private readonly IBackOrderCategoryService _categoryService;
        private readonly FirmParameterService _firmParameterService;
        private readonly IBackOrderPriceListService _priceListRepository;

        public BackOrder(ILogger<BackOrder> logger, IBackOrderProductService productService, IBackOrderCategoryService categoryService, FirmParameterService firmParameterService, IBackOrderPriceListService priceListRepository)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _firmParameterService = firmParameterService;
            _priceListRepository = priceListRepository;
        }


        public IBackOrderProductService OrderProductService { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime? lastUpdateDate = _firmParameterService.FirmParam.LastSync;
                DateTime updatedate = DateTime.Now;
                _categoryService.updateCategory(lastUpdateDate);
                await _productService.updateProducts(lastUpdateDate);
                _priceListRepository.updatePrice(lastUpdateDate);
                _firmParameterService.SetLastUpdate(updatedate);
                 await Task.Delay(300000, stoppingToken);
            }
        }
    }
}
