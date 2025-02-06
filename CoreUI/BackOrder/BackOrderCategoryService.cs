using Business.Abstract;

using Entity;

namespace CoreUI.BackOrder
{
    public interface IBackOrderCategoryService
    {
        Task<bool> updateCategory(DateTime? date, HttpClient _httpClient);
        Task deleteCategory();
    }
    public class BackOrderCategoryService : IBackOrderCategoryService
    {
        
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackOrderCategoryService> _logger;
        public BackOrderCategoryService(IServiceProvider serviceProvider, ILogger<BackOrderCategoryService> logger = null)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task deleteCategory()
        {
            using var scope = _serviceProvider.CreateScope();
            var _categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryService>();
            var l = _categoryRepository.DeleteAll();
            return Task.CompletedTask;
        }
        public async Task<bool> updateCategory(DateTime? date,HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryService>();
                HttpResponseMessage respone;
                
                if (date.HasValue)
                    respone = await _httpClient.GetAsync($"/api/categories/{date.Value.ToString("MM.dd.yyyy HH:mm")}");
                else
                    respone = await _httpClient.GetAsync($"/api/categories");
                if (respone.IsSuccessStatusCode)
                {
                    var pList = await respone.Content.ReadFromJsonAsync<List<Category>>();
                    foreach (var category in pList)
                    {
                        if (category.Code is not null && category.Name is not null)
                        {
                            Category item = await _categoryRepository.GetByCode(category.Code);
                            if (item is null)
                            {
                               await _categoryRepository.Insert(category);
                            }
                            else
                            {
                                item.Name = category.Name;
                                item.Parent = category.Parent;
                               await _categoryRepository.Update(item);
                            }
                        }

                    }
                    
                }
                return true;
            }
            catch (Exception ex)
            {
               Task.FromException(ex);
                _logger.LogCritical(ex.Message);
                return false;
            }
            
        }
    }
}
