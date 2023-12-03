using DataAccess.Abstract;
using Entity;

namespace B2B.BackOrder
{
    public interface IBackOrderCategoryService
    {
        Task updateCategory(DateTime? date, HttpClient _httpClient);
        Task deleteCategory();
    }
    public class BackOrderCategoryService : IBackOrderCategoryService
    {
        
        private readonly IServiceProvider _serviceProvider;
        public BackOrderCategoryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task deleteCategory()
        {
            using var scope = _serviceProvider.CreateScope();
            var _categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
            var l = _categoryRepository.DeleteAll();
            return Task.CompletedTask;
        }
        public async Task updateCategory(DateTime? date,HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
                HttpResponseMessage respone;
                
                if (date.HasValue)
                    respone = await _httpClient.GetAsync($"/api/categories/{date.Value.ToString("MM/dd/yyyy HH:mm")}");
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
                                _categoryRepository.Insert(category);
                            }
                            else
                            {
                                item.Name = category.Name;
                                item.Parent = category.Parent;
                                _categoryRepository.Update(item);
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
    }
}
