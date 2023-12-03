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
        void SentData(HttpClient _httpClient);
    }
    public class BackOrderOrder : IBackOrderOder
    {
        
        private readonly IServiceProvider _serviceProvider;

        public BackOrderOrder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public async void SentData(HttpClient _httpClient)
        {
            //cart tablosundan gönderilecek satırları seç order numarasına göre grupla
            //sipariş gönder sonarında doğruluğu sorgula 
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _basketRepository = scope.ServiceProvider.GetRequiredService<IOrderService>();
                var _productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                var _userManager = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var _basketFiche = _basketRepository.GetAllFiche(true);
                foreach (var basket in _basketFiche)
                {
                    var orderFiche = new Entity.Order()
                    {
                        LogicalRef = 0,
                        ClientCode = _userManager.GetUser(basket.UserGuid).Result.AccountCode,
                        TrCode = 1,
                        FicheNo = "1",
                        Docode = basket.DocNo,
                        Lines = new(),
                    };
                    var basketLines = _basketRepository.GetAll(orderFiche.Docode);
                    foreach (var ordline in basketLines)
                    {
                        orderFiche.Docode = basket.DocNo;
                        orderFiche.Date_ = basket.Date_;
                        orderFiche.TotalDiscounted += ordline.DiscountPrice;
                        orderFiche.TotalVat += ordline.VatPrice;
                        orderFiche.Total += ordline.Total;
                        orderFiche.GrossTotal += ordline.Total;
                        var _product = _productRepository.GetByCode(ordline.ProductCode).Result;
                        orderFiche.Lines.Add(
                            new Entity.OrderLine()
                            {
                                Logicalref = 0,
                                StockRef = _product.LogicalRef,
                                OrdFicheRef = 0,
                                ClientRef = 0,
                                LineType = 0,
                                LineNo = 0,
                                TrCode = 1,
                                Date_ = ordline.Date_,
                                Total = ordline.Total,
                                Price = ordline.Price,
                                Amount = ordline.Amount,
                                Vat = ordline.VatRate,
                                VatMatrah = ordline.Total,
                                VatAmnt = ordline.VatPrice,
                                Discper = 0,
                                Distdisc = 0,
                                UomRef = 0,
                                UsRef = 0,
                                Unit = _product.Unit,
                                Code = _product.Code,
                                Name = _product.Name
                                
                            }
                            );
                    }


                   
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(orderFiche), Encoding.UTF8, "application/json");
                    var httpResponseMessage =
                           await _httpClient.PostAsync($"api/Order", content);
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var sendedBasket = _basketRepository.GetAll(orderFiche.Docode);
                    foreach (Basket sended in sendedBasket)
                    {
                        _basketRepository.DeleteProduct(sended);
                    }
                }


                await Task.Delay(1);
            }
            catch (Exception)
            {

               //
            }

        }
    }
}
