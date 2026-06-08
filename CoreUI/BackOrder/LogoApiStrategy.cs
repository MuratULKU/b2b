
using CoreUI.Data;
using Entity;

namespace CoreUI.BackOrder
{
    public class LogoApiStrategy : IApiStrategy
    {
        public Task<List<ClientFiche>> GetExtre(int currentPage, int pageSize, string clientCode, DateTime firstDate, DateTime lastDate)
        {
            throw new NotImplementedException();
        }

        public string GetHashAsync(string input)
        {
            throw new NotImplementedException();
        }

        public Task RunAsync(DateTime lastUpdateCheck)
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(DateTime lastUpdateCheck)
        {
            throw new NotImplementedException();
        }
    }
}
