
using Business.Abstract;
using Business.Concrete;
using CoreUI.Data;
using DataAccess.Abstract;
using Entity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CoreUI.BackOrder.Ulku
{
    public interface IBackOrderClientService
    {
        Task<bool> UpdateClient(DateTime? date);
        Task SentData();
        Task<List<ClientFiche>> GetExtre(int currentPage, int pageSize, string cLientCode, DateTime firstDate, DateTime lastDate);
        Task DeleteAll();

    }
    public class BackOrderClientService : IBackOrderClientService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BackOrderClientService> _logger;
        private readonly HttpClient _httpClient;
        public BackOrderClientService(IServiceProvider serviceProvider, ILogger<BackOrderClientService> logger, HttpClient httpClient = null)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task DeleteAll()
        {
            using var scope = _serviceProvider.CreateScope();
            var _clientService = scope.ServiceProvider.GetRequiredService<IClientCardService>();
            await _clientService.DeleteAll();

        }
        public async Task<bool> UpdateClient(DateTime? date)
        {
            try
            {   
                    using var scope = _serviceProvider.CreateScope();
                    var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
                    var tokenResponse = await tokenService.GetToken();
                    _httpClient.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", tokenResponse);

                
                

             
                var _clientService = scope.ServiceProvider.GetRequiredService<IClientCardService>();

                HttpResponseMessage respone;


                int currentpage = 1;
                int totalpage = 0;
                do
                {
                    string url = $"/api/v1/Client/clients?page={currentpage}&pageSize=10";
                    if (date.HasValue)
                    {
                        url += $"&updateDate={date.Value.ToString("MM.dd.yyyy HH:mm")}";
                    }
                    respone = await _httpClient.GetAsync(url);

                    if (respone.IsSuccessStatusCode)
                    {
                        var pList = await respone.Content.ReadFromJsonAsync<PageResult<Client>>();
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
                    else
                    {
                        _logger.LogCritical(respone.StatusCode.ToString());
                    }
                } while (currentpage <= totalpage);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return false;
            }
        }

        public async Task SentData()
        {
            try
            {

                using var scope = _serviceProvider.CreateScope();
                var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
                var tokenResponse = await tokenService.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", tokenResponse);


              
                var _ClFicheService = scope.ServiceProvider.GetRequiredService<IClFicheService>();


                List<ClFiche> clFiche = await _ClFicheService.GetClFicheFiche(70, 1);
                if (clFiche != null && clFiche.Count > 0)
                {
                    // _httpClient.DefaultRequestHeaders.Clear();
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

        public async Task<List<ClientFiche>> GetExtre(int currentPage, int pageSize, string cLientCode, DateTime firstDate, DateTime lastDate)
        {
            try
            { 
                var httpResponseMessage =
                  await _httpClient.GetAsync($"/api/v1/client/extre?code={cLientCode}&startDate={firstDate.ToString("MM.dd.yyyy")}&endDate={lastDate.ToString("MM.dd.yyyy")}&currentPage={currentPage}&pageSize={pageSize}");
                var response =  httpResponseMessage.Content.ReadFromJsonAsync<PageResult<ClientFiche>>().Result;
                return response.Items;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return new List<ClientFiche>();
            }

        }
    }


}
