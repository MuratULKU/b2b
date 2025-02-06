using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Task Save(OrdFiche ordFiche);

        Task DeleteOrderFiche(OrdFiche ordFiche);
        Task DeleteLine(OrdLine ordLine);
        Task<int> GetOrderFicheCount(int trCode);
        Task<int> GetOrderFicheCount(Guid firmId,int trCode);
        Task<List<OrdFiche>> GetOrderFiche(int trCode, int CurrentPage, int PageSize);
        Task<List<OrdFiche>> GetOrderFiche(Guid FirmId,int trCode, int CurrentPage, int PageSize);
        Task<List<OrdFiche>> GetOrderFiche(int trCode, byte send);
        Task<OrdFiche> GetOrderFiche(int send,Guid userId);
        Task<OrdFiche> GetOrderFiche(Guid id);
        Task<OrdFiche> GetOrderFiche(int id);

    }
}
