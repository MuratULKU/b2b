using DataAccess.Abstract;
using Entity;

namespace B2B.BackOrder
{
    public interface IBackOrderCategoryService
    {
        void updateCategory(DateTime? date);
    }
    public class BackOrderCategoryService : IBackOrderCategoryService
    {
        private HttpClient _httpClient = new HttpClient();
        private readonly IServiceProvider _serviceProvider;
        public BackOrderCategoryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public async void updateCategory(DateTime? date)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
                HttpResponseMessage respone;
                if (_httpClient.BaseAddress == null)
                    _httpClient.BaseAddress = new Uri($"https://localhost:7079");
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
        }
    }
}
