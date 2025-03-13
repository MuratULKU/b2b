
using Business.Abstract;
using CoreUI.Data;
using DataAccess.Abstract;
using Entity;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CoreUI.BackOrder
{
    public interface IBackOrderClientService
    {
        Task<bool> UpdateClient(DateTime? date, HttpClient _httpClient);
        void SentData(HttpClient _httpClient);
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
                                    var clientcard = await _clientService.GetByCode(client.Code);
                                    if (clientcard == null)
                                        await _clientService.Insert(client);
                                    else
                                    {
                                        clientcard.Name = client.Name;
                                        clientcard.VKN = client.VKN;
                                        clientcard.VatOffice = client.VatOffice;
                                        clientcard.Address1 = client.Address1;
                                        clientcard.Address2 = client.Address2;
                                        clientcard.Town = client.Town;
                                        clientcard.City = client.City;
                                        clientcard.Country = client.Country;
                                        clientcard.UpdateDate = DateTime.Now;
                                        clientcard.Phone2 = client.Phone2;
                                        clientcard.FirmExecutiveName = client.FirmExecutiveName;
                                        clientcard.FirmExecutiveSurName = client.FirmExecutiveSurName;
                                        clientcard.MailAdress2 = client.MailAdress2;
                                        await _clientService.Update(clientcard);
                                    }
                                        
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

        public async void SentData(HttpClient _httpClient)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var _ClFicheService = scope.ServiceProvider.GetRequiredService<IClFicheService>();


                List<ClFiche> clFiche = await _ClFicheService.GetClFicheFiche(70, 1);
                if (clFiche != null && clFiche.Count > 0)
                {
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(JsonSerializer.Serialize(clFiche), Encoding.UTF8, "application/json");

                    var httpResponseMessage =
                           await _httpClient.PostAsync($"/api/v1/client/fiche", content);
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();
                    var responseResults = JsonSerializer.Deserialize<List<ResponseModel>>(response);
                    httpResponseMessage.EnsureSuccessStatusCode();

                    foreach (var result in responseResults)
                    {
                        if (result.status == 1)
                        {
                            var sended = clFiche.FirstOrDefault(x => x.Id == result.id);
                            //sended.LogicalRef = result.logicalref; tabloya eklenecek
                            sended.Send = 2;

                            await _ClFicheService.Update(sended);
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


}
