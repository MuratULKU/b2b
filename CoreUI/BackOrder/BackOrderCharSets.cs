using Business.Abstract;
using DataAccess.Abstract;
using Entity;

namespace CoreUI.BackOrder
{
    public interface IBackOrderCharSets
    { 
        Task<bool> updateCharSets(DateTime? date, HttpClient _httpClient);
        Task deleteCharSets();
        Task<bool> updateCharCodes(DateTime? date, HttpClient _httpClient);
        Task deleteCharCodes();
        Task<bool> updateCharAsgns(DateTime? date, HttpClient _httpClient);
        Task deleteCharAsgns();

    }
    public class BackOrderCharSets : IBackOrderCharSets
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackOrderCharSets> _logger;
        public BackOrderCharSets(IServiceProvider serviceProvider, ILogger<BackOrderCharSets> logger = null)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task deleteCharSets()
        {
            using var scope = _serviceProvider.CreateScope();
            var _charSetRepository = scope.ServiceProvider.GetRequiredService<ICharSetService>();
            var l = _charSetRepository.DeleteAll();
            return Task.CompletedTask;
        }

        public async Task<bool> updateCharSets(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var charsetRepository = scope.ServiceProvider.GetRequiredService<ICharSetService>();
                HttpResponseMessage response;

                var requestUri = date.HasValue
                    ? $"/api/charset/GelAllCharSets/{date.Value:MM.dd.yyyy HH:mm}"
                    : "/api/charset/GetAllCharSets";

                response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var pList = await response.Content.ReadFromJsonAsync<List<CharSet>>();

                    foreach (var charset in pList)
                    {
                        if (!string.IsNullOrEmpty(charset.Code) && !string.IsNullOrEmpty(charset.Name))
                        {
                            var item = await charsetRepository.GetByCode(charset.Code);
                            if (item is null)
                            {
                                await charsetRepository.Insert(charset);
                            }
                            else
                            {
                                item.Name = charset.Name;
                                await charsetRepository.Update(item);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while fetching or processing charsets.");
                return false;
            }

        }

        public Task deleteCharCodes()
        {
            using var scope = _serviceProvider.CreateScope();
            var _charCodeRepository = scope.ServiceProvider.GetRequiredService<ICharCodeService>();
            var l = _charCodeRepository.DeleteAll();
            return Task.CompletedTask;
        }

        public async Task<bool> updateCharCodes(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _charCodeRepository = scope.ServiceProvider.GetRequiredService<ICharCodeService>();
                var _charValRepository = scope.ServiceProvider.GetRequiredService<ICharValRepository>();
                HttpResponseMessage respone;

                if (date.HasValue)
                    respone = await _httpClient.GetAsync($"/api/CharCode/GetAllCharCodes/{date.Value.ToString("MM.dd.yyyy HH:mm")}");
                else
                    respone = await _httpClient.GetAsync($"/api/CharCode/GetAllCharCodes");
                if (respone.IsSuccessStatusCode)
                {
                    var pList = await respone.Content.ReadFromJsonAsync<List<CharCode>>();
                    foreach (var charCode in pList)
                    {
                        if (charCode.Code is not null && charCode.Name is not null)
                        {
                            CharCode item = await _charCodeRepository.GetByCode(charCode.Code);
                            if (item is null)
                            {
                                _charCodeRepository.Insert(charCode);
                            }
                            else
                            {
                                item.Name = charCode.Name;
                                _charCodeRepository.Update(item);
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

        public async Task<bool> updateCharAsgns(DateTime? date, HttpClient _httpClient)
        {
            try
            {   using var scope = _serviceProvider.CreateScope();
                var _charCodeRepository = scope.ServiceProvider.GetRequiredService<ICharCodeService>();
                var _charValRepository =  scope.ServiceProvider.GetRequiredService<ICharValService>();
                var _charAsgnRepository = scope.ServiceProvider.GetRequiredService<ICharAsgnService>();
                var _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                HttpResponseMessage respone;

                if (date.HasValue)
                    respone = await _httpClient.GetAsync($"/api/CharAsgn/GetAllCharSets/{date.Value.ToString("MM.dd.yyyy HH:mm")}");
                else
                    respone = await _httpClient.GetAsync($"/api/CharAsgn/GetAllCharSets");
                if (respone.IsSuccessStatusCode)
                {
                    var pList = await respone.Content.ReadFromJsonAsync<List<CharAsgn>>();
                    foreach (var charAsgn in pList)
                    {
                        if (charAsgn.CharCodeCode is not null && charAsgn.CharValCode is not null)
                        {
                            CharAsgn item = await _charAsgnRepository.GetByCode(charAsgn.Code);
                            if (item == null)
                            {
                                var charcode = await _charCodeRepository.GetByCode(charAsgn.CharCodeCode);
                                var charval = await _charValRepository.GetByCode(charAsgn.CharValCode);
                                var product = await _productRepository.GetByCode(charAsgn.ItemCode);
                                if (product != null && charval != null && charcode != null)
                                {
                                    charAsgn.CharValId = charval.Id;
                                    charAsgn.CharCodeId = charcode.Id;
                                    charAsgn.ProductId = product.Id;
                                    charAsgn.CharCodeName = charcode.Name;
                                    charAsgn.CharValName = charval.Name;
                                    _charAsgnRepository.Insert(charAsgn);
                                }
                            }
                            else
                            {
                               // _charAsgnRepository.Update(charAsgn);
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

        public Task deleteCharAsgns()
        {
            using var scope = _serviceProvider.CreateScope();
            var _charAsgnRepository = scope.ServiceProvider.GetRequiredService<ICharAsgnService>();
            var l = _charAsgnRepository.DeleteAll();
            return Task.CompletedTask;
        }
    }
}
