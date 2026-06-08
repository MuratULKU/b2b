using CoreUI.Data;
using Entity;

namespace CoreUI.BackOrder
{
    public interface IApiStrategy
    {
        Task RunAsync(DateTime lastUpdateCheck);
        Task SendAsync(DateTime lastUpdateCheck);
        Task<List<ClientFiche>> GetExtre(int currentPage, int pageSize, string clientCode, DateTime firstDate, DateTime lastDate);
        string GetHashAsync(string input);
    }
}
