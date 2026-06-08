using Business.Abstract;
using Core.Abstract;
using Core.Logger;
using CoreUI.BackOrder.Ulku;

using CoreUI.Data;
using Entity;


using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace CoreUI.BackOrder.Mikro
{

    public interface IMikroClientService
    {
        Task<bool> UpdateClient(DateTime? date);
        Task SentData();
        Task<List<ClientFiche>> GetExtre(int currentPage, int pageSize, string cLientCode, DateTime firstDate, DateTime lastDate);
    }

    public class MikroClientService : IMikroClientService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerService _logger;

        private readonly HttpClient _httpClient;
        public MikroClientService(IServiceProvider serviceProvider, ILoggerService logger, HttpClient httpClient)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _httpClient = httpClient;
        }


        public async Task<List<ClientFiche>> GetExtre(int currentPage, int pageSize, string clientCode, DateTime firstDate, DateTime lastDate)
        {
            try
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
                var requestObject = new
                {
                    Mikro = new
                    {
                        ApiKey = "A9iR7xw/j3N781uF5daOyDysv3adLTrwbhKmCpc0WM7wTWHzUH0xZYbAqolveOGxq/2c/s2zXpenDMfQIZa6FetPnCuQRN8RtX7mvlWII98=",
                        CalismaYili = 2026,
                        FirmaKodu = "ULKU",
                        KullaniciKodu = "1",
                        Sifre = password,
                    },

                    SQLSorgu = $"select msg_S_0089 as Date,[dbo].[fn_EvrNoForm](msg_S_0090,msg_s_0091) as DocNo, msg_S_0094 as TrString , sum([msg_S_0101\\T]) as debit, sum([msg_S_0102\\T]) as credit, sum([#msg_S_0103\\T]) as balance,max(#msg_S_0085) as LineExp from  dbo.fn_CariFoy (N'0',0,N'{clientCode}',NULL,'{firstDate.ToString("yyyyMMdd")}','{firstDate.ToString("yyyyMMdd")}','{lastDate.ToString("yyyyMMdd")}',0,'') group by msg_S_0089, msg_S_0090,msg_s_0091, msg_S_0094 order by msg_S_0089, msg_S_0094"

                };
                var json = JsonSerializer.Serialize(requestObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response =
                  await _httpClient.PostAsync($"/Api/apiMethods/SqlVeriOkuV2", content);





                var responseJson = await response.Content.ReadAsStringAsync();


                using var doc = JsonDocument.Parse(responseJson);

                var root = doc.RootElement;

                var sqlResult = root
                    .GetProperty("result")[0]
                    .GetProperty("Data")[0]
                    .GetProperty("SQLResult1");

                List<ClientFiche> cList = new List<ClientFiche>();
                double balance = 0;
                foreach (var item in sqlResult.EnumerateArray())
                {
                    balance += item.GetProperty("balance").GetDouble();
                    cList.Add(new ClientFiche
                    {
                        Date = Convert.ToDateTime(item.GetProperty("Date").GetString()),
                        DocNo = item.GetProperty("DocNo").GetString(),
                        TrString = item.GetProperty("TrString").GetString(),
                        Debit = item.GetProperty("debit").GetDouble(),
                        Credit = item.GetProperty("credit").GetDouble(),
                        Balance = balance,
                        LineExp = item.GetProperty("LineExp").GetString()

                    });
                }
                return cList;

            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.Message);
                return new List<ClientFiche>();
            }
        }
        public async Task SentData()
        {
            using var scope = _serviceProvider.CreateScope();
            var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
            var _vatService = scope.ServiceProvider.GetRequiredService<IVatListService>();
            var ordFicheList = await orderService.GetOrderFiche(1, 1,true);
            double[] Vat = await _vatService.GetVat();


            if (ordFicheList == null || !ordFicheList.Any())
                return;

            string text = $"{DateTime.Now:yyyy-MM-dd} 1";
            using var md5 = MD5.Create();
            var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            string password = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            var requestObject = new
            {
                Mikro = new
                {
                    ApiKey = "A9iR7xw/j3N781uF5daOyDysv3adLTrwbhKmCpc0WM7wTWHzUH0xZYbAqolveOGxq/2c/s2zXpenDMfQIZa6FetPnCuQRN8RtX7mvlWII98=",
                    CalismaYili = 2026,
                    FirmaKodu = "ULKU",
                    KullaniciKodu = "1",
                    Sifre = password,

                    evraklar = ordFicheList.Select(fiche => new
                    {
                        evrak_aciklamalari = new[]
                        {
                    new { aciklama = "Test1" },
                    new { aciklama = "Test2" }
                },

                        satirlar = fiche.Lines.Select(line => new
                        {
                            seriler = "A1;B1;C1",
                            sip_b_fiyat = line.Price,
                            sip_birim_pntr = 1,
                            sip_cins = "0",
                            sip_depono = 2,
                            sip_evrakno_seri = "T",
                            sip_miktar = line.Amount,
                            sip_musteri_kod = fiche.ClientCode,
                            sip_stok_kod = line.Product?.Code,
                            sip_stok_sormerk = "",
                            sip_tarih = fiche.Date_.ToString("dd.MM.yyyy"),
                            sip_tip = "0",
                            sip_tutar = line.Total,
                            sip_vergi_pntr = 1 + Array.IndexOf(Vat, line.Vat),
                            sip_vergisiz_fl = 0,
                            sip_iskonto_1 = line.Distdisc,

                            user_tablo = new[]
                            {
                        new { aciklama = "test sipariş user tablo" }
                    },


                        })
                    })
                }
            };

            var json = JsonSerializer.Serialize(requestObject);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Api/APIMethods/SiparisKaydetV2", content);
            if (!response.IsSuccessStatusCode)
            {
                _logger.Error($"Mikro API hatası: {response.StatusCode}");
                return;
            }
            var result = await response.Content.ReadAsStringAsync();

            var doc = JsonDocument.Parse(result);
            var root = doc.RootElement;

           //var resultList = doc
           //                .RootElement
           //                .GetProperty("result")[0]
           //                .GetProperty("Data")
           //                .GetProperty("list");


            bool isError = false;

            if (root.TryGetProperty("result", out var resultArray) &&
            resultArray.GetArrayLength() > 0 &&
            resultArray[0].TryGetProperty("IsError", out var isErrorElement))
            {
                isError = isErrorElement.GetBoolean();
            }
            if (isError)
                return;

          


            foreach (var item in ordFicheList)
            {
                item.Send = 2;
                item.Lines = null;
                item.User = null;
                var test = await orderService.Save(item);
                if(test.Status == ResultStatus.Error)
                {
                    _logger.Error(test.Message);
                }
            }


        }

        public async Task<bool> UpdateClient(DateTime? date)
        {
            //md5 ile kullanıcı password oluşturma
            string text = $"{DateTime.Now.ToString("yyyy-MM-dd")} 1";
            byte[] upass = Encoding.UTF8.GetBytes(text);
            byte[] hash = MD5.Create().ComputeHash(upass);
            StringBuilder sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2")); // 32 char hex
            }
            string password = sb.ToString();
            string updatedate = date.Value.ToString("dd/MM/yyyy");
            for (int i = 0; i < 999; i++)
            {

                var json = $@"{{
                              ""Mikro"": {{
                                ""ApiKey"": ""A9iR7xw/j3N781uF5daOyDysv3adLTrwbhKmCpc0WM7wTWHzUH0xZYbAqolveOGxq/2c/s2zXpenDMfQIZa6FetPnCuQRN8RtX7mvlWII98="",
                                ""FirmaKodu"": ""ULKU"",
                                ""CalismaYili"": ""2026"",
                                ""KullaniciKodu"": ""1"",
                                ""Sifre"": ""{password}""
                              }},

                             ""WhereStr"": ""cari_lastup_date > '{updatedate}'""
                             
                              ""FieldName"": ""cari_Guid,cari_kod, cari_unvan1,cari_unvan2,cari_doviz_cinsi,cari_vdaire_adi,
                                cari_vdaire_no,cari_Portal_Enabled,cari_EMail,cari_Portal_PW"",
                              ""Sort"": ""cari_kod"",
                              ""Size"": 10,
                              ""Index"": {i}
                            }}";

                using var scope = _serviceProvider.CreateScope();
                var _clientService = scope.ServiceProvider.GetRequiredService<IClientCardService>();

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("Api/APIMethods/CariListesiV2", content);
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

                        if (isError)
                            break;
                        var root = doc.RootElement;

                        // result kontrolü
                        if (!root.TryGetProperty("result", out var resultArray) || resultArray.GetArrayLength() == 0)
                            break;

                        var first = resultArray[0];

                        // Data var mı?
                        if (!first.TryGetProperty("Data", out var dataElement) || dataElement.ValueKind == JsonValueKind.Null)
                            break;

                        // CariListesi var mı?
                        if (!dataElement.TryGetProperty("CariListesi", out var cariList))
                            break;

                        // cari list boş mu?
                        if (cariList.GetArrayLength() == 0)
                            break;

                        cariList = doc
                            .RootElement
                            .GetProperty("result")[0]
                            .GetProperty("Data")
                            .GetProperty("CariListesi");



                        foreach (var item in cariList.EnumerateArray())
                        {
                            try
                            {
                                Guid id = item.GetProperty("cari_Guid").GetGuid();
                                string name = item.GetProperty("cari_unvan1").GetString();
                                string code = item.GetProperty("cari_kod").GetString();
                                string userpw = item.GetProperty("cari_Portal_PW").GetString();
                                bool isPortal = item.GetProperty("cari_Portal_Enabled").GetBoolean();
                                string mail = item.GetProperty("cari_EMail").GetString();

                                if (isPortal && !string.IsNullOrEmpty(mail))
                                {
                                    var _companyService = scope.ServiceProvider.GetRequiredService<ICompanyService>();
                                    var _userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                                    var _roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();

                                    var companycard = _companyService.Get(id);

                                    if (companycard != null)
                                    {
                                        var userTest = await _userService.GetUserMail(mail);
                                        if (userTest != null)
                                            throw new Exception($"Mail zaten kullanılıyor: {mail}");

                                        User user = new()
                                        {
                                            Id = Guid.NewGuid(),
                                            CompanyId = id,
                                            Username = code,
                                            Password = userpw,
                                            Email = mail,
                                            CreateDate = DateTime.Now
                                        };

                                        UserRole userRole = new()
                                        {
                                            Id = Guid.NewGuid(),
                                            RoleId = _roleService.GetRole("Managment").Result.Data.Id,
                                            UserId = user.Id,
                                            CreateDate = DateTime.Now
                                        };

                                        Company company = new()
                                        {
                                            Id = id,
                                            ProgramCode = code,
                                            Name = name,
                                            CreateDate = DateTime.Now,
                                            CreateUser = user.Id,
                                            UpdateDate = DateTime.Now,
                                            UpdateUser = user.Id,
                                            City = "-",
                                            Address1 = "-",
                                            Address2 = "-",
                                            Town = "-",
                                            TelNo1 = "-",
                                            TelNo2 = "-",
                                            PeriodCode = "1"
                                        };

                                        await _companyService.Insert(company, user, userRole);
                                    }
                                }

                                var clientcard = await _clientService.GetByCode(code);

                                if (clientcard == null)
                                {
                                    await _clientService.Insert(new Client
                                    {
                                        Code = code,
                                        Name = name,
                                        VatOffice = item.GetProperty("cari_vdaire_adi").GetString(),
                                        VKN = item.GetProperty("cari_vdaire_no").GetString(),
                                        UpdateDate = DateTime.Now,
                                    });
                                }
                                else
                                {
                                    clientcard.Name = name;
                                    clientcard.VKN = item.GetProperty("cari_vdaire_no").GetString();
                                    clientcard.VatOffice = item.GetProperty("cari_vdaire_adi").GetString();
                                    clientcard.UpdateDate = DateTime.Now;

                                    await _clientService.Update(clientcard);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.Error($"Hata oluştu (ClientCode: {item.GetProperty("cari_kod").GetString()}): {ex.Message}");
                                continue; 
                            }
                        }
                    }
                    else
                        break;

                }
            }
            return true;
        }
    }
}
