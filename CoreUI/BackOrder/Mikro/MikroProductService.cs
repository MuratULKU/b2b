using Business.Abstract;
using Entity;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text;
using CoreUI.BackOrder.Ulku;
using Core.Logger;

namespace CoreUI.BackOrder.Mikro
{
    public interface IMikroProductService
    {
        Task<bool> UpdateProduct(DateTime? date);
        Task SentData();
    }
    public class MikroProductService : IMikroProductService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerService _logger;
        private readonly HttpClient _httpClient;

        public MikroProductService(ILoggerService logger, IServiceProvider serviceProvider, HttpClient httpClient = null)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _httpClient = httpClient;
        }

        public Task SentData()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProduct(DateTime? date)
        {
            try
            {
                await VatList(date);

                string text = $"{DateTime.Now.ToString("yyyy-MM-dd")} 1";
                byte[] upass = Encoding.UTF8.GetBytes(text);
                byte[] hash = MD5.Create().ComputeHash(upass);
                StringBuilder sb = new StringBuilder();
                foreach (var b in hash)
                    sb.Append(b.ToString("x2"));
                string password = sb.ToString();

                using var scope = _serviceProvider.CreateScope();
                var _productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                var _vatService = scope.ServiceProvider.GetRequiredService<IVatListService>();
                double[] Vat = await _vatService.GetVat();

                int index = 0;

                while (true)
                {
                    var json = $@"{{
        ""Mikro"": {{
          ""ApiKey"": ""A9iR7xw/j3N781uF5daOyDysv3adLTrwbhKmCpc0WM7wTWHzUH0xZYbAqolveOGxq/2c/s2zXpenDMfQIZa6FetPnCuQRN8RtX7mvlWII98="",
          ""FirmaKodu"": ""ULKU"",
          ""CalismaYili"": ""2026"",
          ""KullaniciKodu"": ""1"",
          ""Sifre"": ""{password}""
        }},
        ""TarihTipi"": 2,
        ""IlkTarih"": ""{date.Value.ToString("yyyy-MM-dd")}"",
        ""SonTarih"": ""{DateTime.Now.ToString("yyyy-MM-dd")}"",
        ""Sort"": ""-sto_kod"",
        ""Size"": 10,
        ""Index"": {index}
    }}";

                    try
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await _httpClient.PostAsync("Api/APIMethods/StokListesiV2", content);

                        if (!response.IsSuccessStatusCode)
                        {
                            _logger.Error($"HTTP hata: {response.StatusCode}");
                            break;
                        }

                        var responseresult = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrEmpty(responseresult))
                        {
                            _logger.Warn("Boş yanıt alındı, döngü sonlandırılıyor.");
                            break;
                        }

                        using var doc = JsonDocument.Parse(responseresult);
                        var root = doc.RootElement;

                        if (!root.TryGetProperty("result", out var resultArray) || resultArray.GetArrayLength() == 0)
                            break;

                        var first = resultArray[0];

                        if (first.GetProperty("IsError").GetBoolean())
                        {
                            _logger.Error("API IsError=true döndürdü, döngü sonlandırılıyor.");
                            break;
                        }

                        if (!first.TryGetProperty("Data", out var dataElement) || dataElement.ValueKind == JsonValueKind.Null)
                            break;

                        if (!dataElement.TryGetProperty("StokListesi", out var productList) || productList.GetArrayLength() == 0)
                            break;

                        foreach (var item in productList.EnumerateArray())
                        {
                            try
                            {
                                var productcard = await _productService.GetByCode(item.GetProperty("sto_kod").GetString());

                                if (productcard == null)
                                {
                                    await _productService.Insert(new Product
                                    {
                                        Code = item.GetProperty("sto_kod").GetString(),
                                        Name = item.GetProperty("sto_isim").GetString(),
                                        Vat = Vat[(byte)item.GetProperty("sto_toptan_vergi").GetInt16() - 1],
                                        SellVat = Vat[(byte)item.GetProperty("sto_perakende_vergi").GetInt16() - 1],
                                        Unit = item.GetProperty("sto_birim1_ad").GetString(),
                                        Unit1 = item.GetProperty("sto_birim2_ad").GetString(),
                                        Unit3 = item.GetProperty("sto_birim3_ad").GetString(),
                                        Unit1rate = item.GetProperty("sto_birim2_katsayi").GetDecimal(),
                                        Unit2rate = item.GetProperty("sto_birim3_katsayi").GetDecimal(),
                                        Unit3rate = item.GetProperty("sto_birim4_katsayi").GetDecimal(),
                                    });
                                }
                                else
                                {
                                    productcard.Name = item.GetProperty("sto_isim").GetString();
                                    productcard.Vat = Vat[(byte)item.GetProperty("sto_toptan_vergi").GetInt16() - 1];
                                    productcard.SellVat = Vat[(byte)item.GetProperty("sto_perakende_vergi").GetInt16() - 1];
                                    productcard.Unit = item.GetProperty("sto_birim1_ad").GetString();
                                    productcard.Unit1 = item.GetProperty("sto_birim2_ad").GetString();
                                    productcard.Unit2 = item.GetProperty("sto_birim3_ad").GetString();
                                    productcard.Unit3 = item.GetProperty("sto_birim4_ad").GetString();
                                    productcard.Unit1rate = item.GetProperty("sto_birim2_katsayi").GetDecimal();
                                    productcard.Unit2rate = item.GetProperty("sto_birim3_katsayi").GetDecimal();
                                    productcard.Unit3rate = item.GetProperty("sto_birim4_katsayi").GetDecimal();
                                    await _productService.Save(productcard);
                                }
                            }
                            catch (Exception ex)
                            {
                                // Tek ürün hata verse bile diğerleri işlenmeye devam eder
                                _logger.Error($"Ürün işlenirken hata [{item.GetProperty("sto_kod").GetString()}]: {ex.Message}");
                            }
                        }

                        index++;
                    }
                    catch (HttpRequestException ex)
                    {
                        _logger.Error($"HTTP istek hatası (index: {index}): {ex.Message}");
                        break;
                    }
                    catch (JsonException ex)
                    {
                        _logger.Error($"JSON parse hatası (index: {index}): {ex.Message}");
                        break;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateProduct genel hata: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> VatList(DateTime? date)
        {
            string text = $"{DateTime.Now.ToString("yyyy-MM-dd")} 1";
            byte[] upass = Encoding.UTF8.GetBytes(text);
            byte[] hash = MD5.Create().ComputeHash(upass);
            StringBuilder sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2")); // 32 char hex
            }
            string password = sb.ToString();

         
                var json = $@"{{
                              ""Mikro"": {{
                                ""ApiKey"": ""A9iR7xw/j3N781uF5daOyDysv3adLTrwbhKmCpc0WM7wTWHzUH0xZYbAqolveOGxq/2c/s2zXpenDMfQIZa6FetPnCuQRN8RtX7mvlWII98="",
                                ""FirmaKodu"": ""ULKU"",
                                ""CalismaYili"": ""2026"",
                                ""KullaniciKodu"": ""1"",
                                ""Sifre"": ""{password}""
                              }}

                             
                            }}";

                using var scope = _serviceProvider.CreateScope();
                var _vatService = scope.ServiceProvider.GetRequiredService<IVatListService>();

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("Api/APIMethods/VergiListesiV2", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseresult = await response.Content.ReadAsStringAsync();
                    if (responseresult != null)
                    {
                        using var doc = JsonDocument.Parse(responseresult);
                        var isError = doc
                                     .RootElement
                                     .GetProperty("result")[0]
                                     .GetProperty("IsError")
                                     .GetBoolean();

                  
                  


                       var productList = doc
                            .RootElement
                            .GetProperty("result")[0]
                            .GetProperty("Data")
                            .GetProperty("list");



                        foreach (var item in productList.EnumerateArray())
                        {

                            var vatcard = await _vatService.GetbyVat(item.GetProperty("vergiSiraNo").GetByte());

                            if (vatcard == null)
                                await _vatService.Add(new VatList
                                {
                                    VatNo = item.GetProperty("vergiSiraNo").GetByte(),
                                    VatPer = item.GetProperty("vergiOrani").GetDouble(),
                                    VatName = item.GetProperty("vergiAdi").GetString()

                                });
                            else
                            {

                                vatcard.VatPer = item.GetProperty("vergiOrani").GetDouble();
                                vatcard.VatName = item.GetProperty("vergiAdi").GetString();
                                await _vatService.Update(vatcard);
                            }

                        }
                    }
                   

                }
         return true;
        }


    }
}
