
using B2C.Data;
using Business.Abstract;
using Business.Concrete;
using CoreUI.Data;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity;


namespace CoreUI.BackOrder
{
    public interface IBackOrderProductService
    {
        Task<bool> isApiActiveted(HttpClient _httpClient);
        Task<bool> updateProducts(DateTime? date, HttpClient _httpClient);
        Task deleteProducts();
        Task DeleteImages();
        Task<bool> updateImages(HttpClient _httpClient);
        Task<bool> DeleteProduct(DateTime? date, HttpClient _httpClient);
        Task CharSetDeleteAll();
        Task CharAsgnDeleteAll();
        Task CharCodeDeleteAll();
        Task PriceListDeleteAll();
        Task CategoriesDeleteAll();
        Task ProductAmountDeleteAll();
        Task<bool> CharSetUpdate(DateTime? date, HttpClient _httpClient);
        Task<bool> CharAsgnUpdate(DateTime? date, HttpClient _httpClient);
        Task<bool> CharCodeUpdate(DateTime? date, HttpClient _httpClient);
        Task<bool> PriceListUpdate(DateTime? date, HttpClient _httpClient);
        Task<bool> CategoryUpdate(DateTime? date, HttpClient _httpClient);
        Task<bool> ProductAmountUpdate(DateTime? date, HttpClient _httpClient);

    }
    public class BackOrderProductService : IBackOrderProductService
    {



        private readonly IServiceProvider _serviceProvider;
        private IProductRepository _productRepository;
        private IFirmDocRepository _firmDocRepository;

        private readonly ILogger<BackOrderProductService> _logger;

        public BackOrderProductService(IServiceProvider serviceProvider, ILogger<BackOrderProductService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }



        public Task deleteProducts()
        {
            using var scope = _serviceProvider.CreateScope();
            _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            DeleteImages();
            _productRepository.DeleteAll();
            return Task.CompletedTask;
        }

        public Task DeleteImages()
        {
            using var scope = _serviceProvider.CreateScope();
            _firmDocRepository = scope.ServiceProvider.GetRequiredService<IFirmDocRepository>();
            _firmDocRepository.DeleteAll();
            return Task.CompletedTask;
        }
        private async Task InsertProduct(List<Product> products)
        {
            using var scope = _serviceProvider.CreateScope();
            var _charSetRepository = scope.ServiceProvider.GetRequiredService<ICharSetService>();
            foreach (var product in products)
            {
                if (product.Code is not null && product.Name is not null)
                {
                    Product item = await _productRepository.GetByLogicalref(product.LogicalRef);
                    if (item is null)
                    {
                        item = product;
                        item.Id = Guid.NewGuid();

                        //Chaset idsini ekleyelim yukarıda ki fazlalıklar kontrol edilsin
                        if (product.CharSetCode != string.Empty)
                        {
                            CharSet charset = await _charSetRepository.GetByCode(product.CharSetCode);
                            product.CharSetId = charset.Id;
                        }
                        else
                            product.CharSetId = null;
                        if (product.firmDocs?.Count >= 1)
                        {
                            item.firmDocs?.Add(product.firmDocs[0]);
                        }
                        _productRepository.Insert(item);
                    }
                    else
                    {
                        if (product.firmDocs?.Count > 0)
                        {
                            if (item.firmDocs?.Count >= 1)
                            {
                                item.firmDocs.Add(product.firmDocs[0]);
                            }
                            else
                            {
                                if (item.firmDocs?.Count == 0)
                                    item.firmDocs.Add(product.firmDocs[0]);
                                else
                                    item.firmDocs[0].LData = product.firmDocs[0].LData;
                            }
                        }

                        item.Name = product.Name;
                        item.Vat = product.Vat;
                        item.SellVat = product.SellVat;
                        item.ProducerCode = product.ProducerCode ?? string.Empty;
                        item.StGrupCode = product.StGrupCode ?? string.Empty;
                        item.SpeCode = product.SpeCode ?? string.Empty;
                        item.SpeCode2 = product.SpeCode2 ?? string.Empty;
                        item.SpeCode3 = product.SpeCode3 ?? string.Empty;
                        item.SpeCode4 = product.SpeCode4 ?? string.Empty;
                        item.SpeCode5 = product.SpeCode5 ?? string.Empty;
                        item.ParentRef = product.ParentRef;
                        item.Active = product.Active;
                        if (product.CharSetCode != string.Empty)
                        {
                            CharSet charset = _charSetRepository.GetByCode(product.CharSetCode).Result;
                            product.CharSetId = charset.Id;
                        }
                        _productRepository.Update(item);
                    }
                }
            }
        }

        public async Task<bool> DeleteProduct(DateTime? date, HttpClient _httpClient)
        {
            try
            {

                HttpResponseMessage respone;

                if (date.HasValue)
                {
                    respone = await _httpClient.GetAsync($"/api/products/GetDeleteAllProducts/{date.Value.ToString("MM.dd.yyyy HH:mm")}");
                    if (respone.IsSuccessStatusCode)
                    {
                        var pList = respone.Content.ReadFromJsonAsync<List<string>>().Result;
                        if (pList != null && pList.Count > 0)
                        {
                            foreach (var p in pList)
                            {
                                using var scope = _serviceProvider.CreateScope();
                                _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                                Product product = await _productRepository.GetByCode(p);
                                if (product != null)
                                {
                                    //sepeti kontrol etmek lazım
                                    if (!await _productRepository.Delete(product))
                                        _logger.LogCritical($"{p} kodlu kayıt silinemedi.");
                                }

                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                await Task.FromException(ex);
                return false;
            }
        }
        public async Task<bool> updateProducts(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                _firmDocRepository = scope.ServiceProvider.GetRequiredService<IFirmDocRepository>();
                HttpResponseMessage respone;

               
                    int currentpage = 1;
                    int totalpage = 0;
                    do
                    {
                        respone = await _httpClient.GetAsync($"/api/v1/Products/items?page={currentpage}&pageSize=10");
                        if (respone.IsSuccessStatusCode)
                        {
                            var pList = respone.Content.ReadFromJsonAsync<PageResult<Product>>().Result;
                            currentpage = pList.CurrentPage + 1;
                            totalpage = pList.TotalPages;
                            if (pList.Items.Count > 0)
                                await InsertProduct(pList.Items);
                        }
                    } while (currentpage <= totalpage);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                await Task.FromException(ex);
                return false;
            }

        }

        public async Task<bool> isApiActiveted(HttpClient _httpClient)
        {
            try
            {

                var respone = await _httpClient.GetAsync("/api/getallproducts");
                if (respone.IsSuccessStatusCode)
                {

                    //iactionresult türünde data dönüşüne bakmak için

                }
            }
            catch
            {
                return false;
            }
            return false;

        }

        private async Task InsertImage(List<FirmDoc> firmDocs)
        {
            foreach (var firmdoc in firmDocs)
            {
                if (firmdoc.ProtuctId == Guid.Empty)
                {
                    var prd = await _productRepository.GetByCode(firmdoc.Code);
                    if (prd != null)
                    {
                        prd.firmDocs.Add(firmdoc);
                        _productRepository.Update(prd);
                    }
                }
            }
        }
        public async Task<bool> updateImages(HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                _firmDocRepository = scope.ServiceProvider.GetRequiredService<IFirmDocRepository>();
                HttpResponseMessage respone;
                int currentpage = 1;
                int totalpage = 0;
                do
                {
                    respone = await _httpClient.GetAsync($"/api/Products/GetItemImage?PageSize=20&CurrentPage={currentpage}");
                    if (respone.IsSuccessStatusCode)
                    {
                        var pList = respone.Content.ReadFromJsonAsync<ListPage<FirmDoc>>().Result;
                        currentpage = pList.CurrentPage;
                        totalpage = pList.PageCount;
                        if (pList.Lines.Count > 0)
                            await InsertImage(pList.Lines);
                    }
                } while (currentpage <= totalpage);

                return true;

            }

            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                Task.FromException(ex);
                return false;
            }
        }

        //charset
        public async Task CharSetDeleteAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _provider = scope.ServiceProvider.GetRequiredService<ICharSetService>();
                await _provider.DeleteAll();
            }

        }
        public async Task CharAsgnDeleteAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _provider = scope.ServiceProvider.GetRequiredService<ICharAsgnService>();
                await _provider.DeleteAll();
            }

        }

        public async Task CharCodeDeleteAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _provider = scope.ServiceProvider.GetRequiredService<ICharCodeService>();
                await _provider.DeleteAll();
            }

        }

        public async Task PriceListDeleteAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _provider = scope.ServiceProvider.GetRequiredService<IPriceListService>();
                await _provider.DeleteAll();
            }

        }

        public async Task CategoriesDeleteAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _provider = scope.ServiceProvider.GetRequiredService<ICategoryService>();
                await _provider.DeleteAll();
            }
        }

        public async Task ProductAmountDeleteAll()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _provider = scope.ServiceProvider.GetRequiredService<IProductAmountService>();
                await _provider.DeleteAll();
            }
        }

        public async Task<bool> CharSetUpdate(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var charsetRepository = scope.ServiceProvider.GetRequiredService<ICharSetService>();
                    HttpResponseMessage response;
                    //http://localhost:5023/api/v1/Products/charsets?page=1&pageSize=10&updateDate=01.01.2020
                 
                    int currentPage = 0;
                    int totalPage = 1;
                    do
                    {
                        string url = $"/api/v1/Products/charsets?page={currentPage+1}&pageSize=10";
                        if (date.HasValue)
                        {
                            url += $"&updateDate={date.Value.ToString("MM.dd.yyyy HH:mm")}";
                        }
                        response = await _httpClient.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            var pList = await response.Content.ReadFromJsonAsync<PageResult<CharSet>>();
                           
                            if (pList != null)
                            {
                                currentPage = pList.CurrentPage;
                                totalPage = pList.TotalPages;
                                foreach (var charset in pList.Items)
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
                            else { break; }
                        }
                        else
                        {
                            _logger.LogError($"HTTP Error: {response.StatusCode}");
                            return false;
                        }

                    } while (currentPage < totalPage);

                
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while fetching or processing charsets.");
                return false;
            }
        }



        public async Task<bool> CategoryUpdate(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _categoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryService>();
                    HttpResponseMessage response;
                    int currentPage = 0;
                    int totalPage = 1; 

                    do
                    {
                        string url = $"/api/v1/Products/categories?page={currentPage + 1}&pageSize=10";
                        if (date.HasValue)
                        {
                            url += $"&updateDate={date.Value.ToString("MM.dd.yyyy HH:mm")}";
                        }

                        response = await _httpClient.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var pList = await response.Content.ReadFromJsonAsync<PageResult<Category>>();
                            if (pList != null)
                            {
                                currentPage = pList.CurrentPage;
                                totalPage = pList.TotalPages;

                                foreach (var category in pList.Items)
                                {
                                    if (category.Code != null && category.Name != null)
                                    {
                                        var item = await _categoryRepository.GetByCode(category.Code);
                                        if (item == null)
                                        {
                                            await _categoryRepository.Insert(category);
                                        }
                                        else
                                        {
                                            item.Name = category.Name;
                                            item.Parent = category.Parent;
                                            item.UpdateDate = DateTime.Now; // Eğer UpdateDate özelliğiniz varsa
                                            await _categoryRepository.Update(item);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {

                            _logger.LogError($"HTTP Error: {response.StatusCode}");
                            return false; 
                        }
                    }
                    while (currentPage < totalPage);

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while updating categories.");
                return false;
            }
        }


        public async Task<bool> CharAsgnUpdate(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _charCodeManager = scope.ServiceProvider.GetRequiredService<ICharCodeService>();
                    var _charValManager = scope.ServiceProvider.GetRequiredService<ICharValService>();
                    var _charAsgnManager = scope.ServiceProvider.GetRequiredService<ICharAsgnService>();
                    var _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                    HttpResponseMessage response;
                    int currentPage = 0;
                    int totalPage = 1;
                    do
                    {
                        string url = $"/api/v1/Products/charasgn?page={currentPage+1}&pageSize=10";
                        if (date.HasValue)
                        {
                            url += $"&updateDate={date.Value.ToString("MM.dd.yyyy HH:mm")}";
                        }
                        response = await _httpClient.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            var pList = await response.Content.ReadFromJsonAsync<PageResult<CharAsgn>>();
                            if (pList != null)
                            {
                                currentPage = pList.CurrentPage;
                                totalPage = pList.TotalPages;
                                foreach (var charAsgn in pList.Items)
                                {
                                    if (charAsgn.CharCodeCode is not null && charAsgn.CharValCode is not null)
                                    {
                                        CharAsgn item = await _charAsgnManager.GetByCode(charAsgn.Code);
                                        if (item == null)
                                        {
                                            var charcode = await _charCodeManager.GetByCode(charAsgn.CharCodeCode);
                                            var charval = await _charValManager.GetByCode(charAsgn.CharValCode);
                                            var product = await _productRepository.GetByCode(charAsgn.ItemCode);
                                            if (product != null && charval != null && charcode != null)
                                            {
                                                charAsgn.CharValId = charval.Id;
                                                charAsgn.CharCodeId = charcode.Id;
                                                charAsgn.ProductId = product.Id;
                                                charAsgn.CharCodeName = charcode.Name;
                                                charAsgn.CharValName = charval.Name;
                                                await _charAsgnManager.Insert(charAsgn);
                                                
                                            }
                                        }
                                        else
                                        {
                                           await _charAsgnManager.Update(charAsgn);
                                        }
                                    }

                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                        else
                        {
                            _logger.LogError($"HTTP Error: {response.StatusCode}");
                            return false;
                        }
                    } while (currentPage < totalPage);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while fetching or processing charsets.");
                return false;
            }
        }

        public async Task<bool> CharCodeUpdate(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var charCodeManager = scope.ServiceProvider.GetRequiredService<ICharCodeService>();
                var charValManager = scope.ServiceProvider.GetRequiredService<ICharValService>();
                HttpResponseMessage response;

                int currentPage = 0;
                int totalPage = 1;

                do
                {
                    string url = $"/api/v1/Products/charcodes?page={currentPage + 1}&pageSize=10";
                    if (date.HasValue)
                    {
                        url += $"&updateDate={date.Value.ToString("MM.dd.yyyy HH:mm")}";
                    }

                    response = await _httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var pList = await response.Content.ReadFromJsonAsync<PageResult<CharCode>>();
                        if (pList != null)
                        {
                            currentPage = pList.CurrentPage; // Güncel sayfayı al
                            totalPage = pList.TotalPages; // Toplam sayfayı al

                            foreach (var charCode in pList.Items)
                            {
                                if (!string.IsNullOrEmpty(charCode.Code) && !string.IsNullOrEmpty(charCode.Name))
                                {
                                    var item = await charCodeManager.GetByCode(charCode.Code);
                                    if (item == null)
                                    {
                                       
                                        await charCodeManager.Insert(charCode);
                                    }
                                    else
                                    {
                                        item.Name = charCode.Name;
                                        await charCodeManager.Update(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            break; // pList null ise döngüyü kır
                        }
                    }
                    else
                    {
                        _logger.LogError($"HTTP Error: {response.StatusCode}");
                        return false;
                    }

                } while (currentPage < totalPage);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while fetching or processing char codes.");
                return false;
            }
        }


        public async Task<bool> PriceListUpdate(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var priceListManager = scope.ServiceProvider.GetRequiredService<IPriceListService>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                HttpResponseMessage response;

                int currentPage = 0;
                int totalPage = 1;

                do
                {
                    string url = $"/api/v1/Products/pricelist?page={currentPage + 1}&pageSize=10";
                    if (date.HasValue)
                    {
                        url += $"&updateDate={date.Value.ToString("MM.dd.yyyy HH:mm")}";
                    }

                    response = await _httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var pList = await response.Content.ReadFromJsonAsync<PageResult<PriceList>>();
                        if (pList != null)
                        {
                            currentPage = pList.CurrentPage; // Mevcut sayfayı al
                            totalPage = pList.TotalPages; // Toplam sayfayı al

                            foreach (var price in pList.Items)
                            {
                                if (!string.IsNullOrEmpty(price.Code))
                                {
                                    var product = await productRepository.GetByLogicalref(price.Cardref);
                                    if (product != null)
                                    {
                                        PriceList item = await priceListManager.GetByCode(price.Code);
                                        if (item == null)
                                        {
                                            item = new PriceList
                                            {
                                                Code = price.Code,
                                                Cardref = price.Cardref,
                                                Id = Guid.NewGuid(),
                                                ProductCode = product.Code,
                                                Name = price.Name,
                                                CurrencyId = price.CurrencyId,
                                                Priorty = price.Priorty,
                                                Price = price.Price,
                                                ProductId = product.Id,
                                                IncVat = price.IncVat
                                            };
                                            await priceListManager.Insert(item);
                                        }
                                        else
                                        {
                                            item.Price = price.Price;
                                            item.IncVat = price.IncVat;
                                            await priceListManager.Update(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            break; // pList null ise döngüyü kır
                        }
                    }
                    else
                    {
                        _logger.LogError($"HTTP Error: {response.StatusCode}");
                        return false;
                    }

                } while (currentPage < totalPage);

                return true; // Başarı durumu
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while updating the price list.");
                return false; // Hata durumu
            }
        }


        public async Task<bool> ProductAmountUpdate(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var productAmountManager = scope.ServiceProvider.GetRequiredService<IProductAmountService>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                HttpResponseMessage response;

                int currentPage = 0;
                int totalPage = 1;

                do
                {
                    string url = $"/api/v1/Products/amounts?page={currentPage + 1}&pageSize=10";
                    if (date.HasValue)
                    {
                        url += $"&updateDate={date.Value.ToString("MM.dd.yyyy")}";
                    }

                    response = await _httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var pList = await response.Content.ReadFromJsonAsync<PageResult<ProductAmount>>();
                        if (pList != null)
                        {
                            currentPage = pList.CurrentPage; // Mevcut sayfayı al
                            totalPage = pList.TotalPages; // Toplam sayfayı al

                            foreach (var productAmount in pList.Items)
                            {
                                if (productAmount != null)
                                {
                                    var item = await productAmountManager.GetByCode(productAmount.Code);
                                    var product = await productRepository.GetByCode(productAmount.Code);

                                    if (product != null)
                                    {
                                        if (item == null)
                                        {
                                            item = new ProductAmount
                                            {
                                                Id = Guid.NewGuid(),
                                                Code = productAmount.Code,
                                                StockRef = productAmount.StockRef,
                                                ProductId = product.Id,
                                                OnHand = productAmount.OnHand
                                            };
                                            await productAmountManager.Insert(item);
                                        }
                                        else
                                        {
                                            item.OnHand = productAmount.OnHand;
                                            await productAmountManager.Update(item);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            break; // pList null ise döngüyü kır
                        }
                    }
                    else
                    {
                        _logger.LogError($"HTTP Error: {response.StatusCode}");
                        return false;
                    }
                } while (currentPage < totalPage);

                return true; // Başarı durumu
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An error occurred while updating product amounts.");
                return false; // Hata durumu
            }
        }

    }
}



