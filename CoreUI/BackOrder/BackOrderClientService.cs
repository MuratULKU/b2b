
using Business.Abstract;
using CoreUI.Data;
using DataAccess.Abstract;
using Entity;

namespace CoreUI.BackOrder
{
    public interface IBackOrderClientService
    {
        Task<bool> UpdateClient(DateTime? date, HttpClient _httpClient);
    }
    public class BackOrderClientService : IBackOrderClientService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackOrderClientService> _logger;
        public BackOrderClientService(IServiceProvider serviceProvider, ILogger<BackOrderClientService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<bool> UpdateClient(DateTime? date, HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _clientService = scope.ServiceProvider.GetRequiredService<IClientCardService>();

                HttpResponseMessage respone;


                int currentpage = 1;
                int totalpage = 0;
                do
                {
                    respone = await _httpClient.GetAsync($"/api/v1/Client/clients?page={currentpage}&pageSize=10");
                    if (respone.IsSuccessStatusCode)
                    {
                        var pList = respone.Content.ReadFromJsonAsync<PageResult<Client>>().Result;
                        if (pList != null)
                        {
                            currentpage = pList.CurrentPage + 1;
                            totalpage = pList.TotalPages;
                            if (pList?.Items.Count > 0)
                            {
                                foreach (var client in pList.Items)
                                {
                                    client.VKN = "111";
                                    var clientcard = await _clientService.GetByCode(client.Code);
                                    if (clientcard == null)
                                        await _clientService.Insert(client);
                                    else
                                        await _clientService.Update(clientcard);
                                }
                            }
                        }
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
    }
}
