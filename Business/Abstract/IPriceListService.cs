using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPriceListService
    {
        Task<PriceList> GetByCode(string code);
        Task<PriceList> GetByLogicalref(int Logicalref);
        Task<IResult> Insert(PriceList priceList);
        Task<IResult> Update(PriceList priceList);
        Task DeleteAll();
    }
}
