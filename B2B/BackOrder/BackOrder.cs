using B2B.Data;

namespace B2B.BackOrder
{
    public class BackOrder : BackgroundService
    {
        private readonly ILogger<BackOrder> _logger;
        private readonly IBackOrderProductService _productService;
        private readonly IBackOrderCategoryService _categoryService;
        private readonly FirmParameterService _firmParameterService;

        public BackOrder(ILogger<BackOrder> logger, IBackOrderProductService productService, IBackOrderCategoryService categoryService, FirmParameterService firmParameterService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _firmParameterService = firmParameterService;
        }


        public IBackOrderProductService OrderProductService { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime? lastUpdateDate = _firmParameterService.FirmParam.LastSync;
                DateTime updatedate = DateTime.Now;
                _categoryService.updateCategory(lastUpdateDate);
                _productService.updateProducts(lastUpdateDate);
                _firmParameterService.SetLastUpdate(updatedate);
                await Task.Delay(300000, stoppingToken);
            }
        }
    }
}
