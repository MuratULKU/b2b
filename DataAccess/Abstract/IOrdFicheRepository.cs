using Entity;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Abstract
{
    public interface IOrdFicheRepository:IRepository<OrdFiche>
    {
        Task<OrdFiche> GetOrderFiche(int Logicalref);
        Task<List<OrdFiche>> GetOrderFiche(int TrCode, int CurrentPage, int PageSize);
        Task<List<OrdFiche>> GetOrderFiche(Guid CompanyId,int TrCode, int CurrentPage, int PageSize);

    }
}
