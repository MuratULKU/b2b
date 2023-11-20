using B2B.Data;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Security.Policy;
using System.Text.Json.Nodes;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace B2B.BackOrder
{
    public interface IBackOrderOder
    {
        void SentData();
    }
    public class BackOrderOrder : IBackOrderOder
    {
        private HttpClient _httpClient = new HttpClient();
        private readonly IServiceProvider _serviceProvider;

        public BackOrderOrder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public async void SentData()
        {
            //cart tablosundan gönderilecek satırları seç order numarasına göre grupla
            //sipariş gönder sonarında doğruluğu sorgula 
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _basketRepository = scope.ServiceProvider.GetRequiredService<IOrderService>();
                var _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var _userManager = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                List<Guid> totaluserlist = _basketRepository.GetAllBasket().Select(x => x.UserGuid).Distinct().ToList();
                //kullanıcıya göre siparişleri grupla
                //document no ya göre grouplama yapacağız sepete yazarken document no
                //alanı kullanılacak 1 tabloda en son noyu tuttalım diğer kullanıcılar için 
                foreach (var user in totaluserlist)
                {
                    User _user = _userManager.GetUser(user).Result;
                    List<Basket> baskets = _basketRepository.GetAll(user);
                    var orderFiche = new Entity.Order()
                    {
                        LogicalRef = 0,
                        ClientCode = _user.AccountCode,
                        TrCode = 1,
                        FicheNo = "1",
                       
                        Lines = new(),
                    };
                    foreach (var basket in baskets)
                    {
                        orderFiche.Docode = basket.DocNo;
                        orderFiche.Date_ = basket.Date_;
                        orderFiche.TotalDiscounted += basket.DiscountPrice;
                        orderFiche.TotalVat += basket.VatPrice;
                        orderFiche.Total += basket.Total;
                        orderFiche.GrossTotal += basket.Total;
                        var _product = _productRepository.GetByCode(basket.ProductCode).Result;
                        orderFiche.Lines.Add(
                            new Entity.OrderLine()
                            {
                                Logicalref = 0,
                                StockRef = _product.LogicalRef,
                                OrdFicheRef = 0,
                                ClientRef =0,
                                LineType = 0,
                                LineNo = 0,
                                TrCode = 1,
                                Date_= basket.Date_,
                                Total = basket.Total,
                                Price = basket.Price,
                                Amount = basket.Amount,
                                Vat = basket.VatRate,
                                VatMatrah = basket.Total,
                                VatAmnt = basket.VatPrice,
                                Discper = 0,
                                Distdisc = 0,
                                UomRef = 0,
                                UsRef=0
                            }
                            ) ;

                        if (_httpClient.BaseAddress == null)
                            _httpClient.BaseAddress = new Uri($"https://localhost:7079");
                        _httpClient.DefaultRequestHeaders.Clear();
                        _httpClient.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
                        StringContent content = new StringContent(JsonConvert.SerializeObject(orderFiche), Encoding.UTF8, "application/json");            
                        var httpResponseMessage =
                               await _httpClient.PostAsync($"api/Order", content);
                        var response = await httpResponseMessage.Content.ReadAsStringAsync();
                        httpResponseMessage.EnsureSuccessStatusCode();
                    }

                }


                await Task.Delay(1);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
