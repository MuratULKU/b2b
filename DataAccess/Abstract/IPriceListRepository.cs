using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPriceListRepository
    {
        void Insert(PriceList priceList);
        void Update(PriceList priceList);
        void Delete(PriceList priceList);
        List<PriceList> GetAll();
        Task<PriceList> GetByCode(string code);
        Task<PriceList> GetByProductCode(string productCode);
        Task<PriceList> GetByProductCode(string productCode,int priorty);
        
    }
}
