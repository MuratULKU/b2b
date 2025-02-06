using Business.Abstract;
using Entity;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;
using Core.Concrete;
using B2C.Data;
using DataAccess.Abstract;
using CoreUI.Data;



namespace CoreUI.BackOrder
{
    public interface IBackOrderOder
    {
        void SentData(HttpClient _httpClient);
        Task<bool> OrderFicheState(HttpClient _httpClient);
    }
    public class BackOrderOrder : IBackOrderOder
    {

        private readonly IServiceProvider _serviceProvider;
        private ILogger<BackOrderOrder> _logger;

        public BackOrderOrder(IServiceProvider serviceProvider, ILogger<BackOrderOrder> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<bool> OrderFicheState(HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                HttpResponseMessage respone;
                int currentpage = 1;
                int totalpage = 0;
                do
                {
                    respone = await _httpClient.GetAsync($"/api/v1/Orders/ordersstate?PageSize=20&Page={currentpage}");
                    if (respone.IsSuccessStatusCode)
                    {
                        var pList = respone.Content.ReadFromJsonAsync<PageResult<ResposeStateModel>>().Result;
                        currentpage = pList.CurrentPage;
                        totalpage = pList.TotalCount;
                        foreach (var item in pList.Items)
                        {
                            var _ordFiche = await _orderService.GetOrderFiche(item.logicalref);
                            if(_ordFiche != null && (item.status == 4 || item.status == 2))
                            {
                                if (item.status == 4)
                                    _ordFiche.Send = 4;
                                else
                                    _ordFiche.Send = 3;
                                await _orderService.Save(_ordFiche);
                            }
                        }
                        currentpage++;
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
        public async void SentData(HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();


                List<OrdFiche> ordFiche = await _orderService.GetOrderFiche(1, 1);
                if (ordFiche != null && ordFiche.Count > 0)
                {
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(JsonSerializer.Serialize(ordFiche), Encoding.UTF8, "application/json");

                    var httpResponseMessage =
                           await _httpClient.PostAsync($"/api/v1/Orders/order", content);
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();
                    var responseResults = JsonSerializer.Deserialize<List<ResponseModel>>(response);
                    httpResponseMessage.EnsureSuccessStatusCode();

                    foreach (var result in responseResults)
                    {
                        if (result.status == 1)
                        {
                            var sended = ordFiche.FirstOrDefault(x => x.Id == result.id);
                            sended.LogicalRef = result.logicalref;
                            sended.Send = 2;

                            await _orderService.Save(sended);
                        }
                    }




                }

                await Task.Delay(1);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
            }

        }
    }
    public class ResponseModel
    {
        public Guid id { get; set; }
        public int status { get; set; }
        public int logicalref { get; set; }
        public string message { get; set; }
        public Exception exception { get; set; }
    }

    public class ResposeStateModel
    {
        public int logicalref { get; set; }
        public Int16 status { get; set; }
    }
}
