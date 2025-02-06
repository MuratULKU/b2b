using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public  interface IProductAmountService
    {
        Task<ProductAmount> GetByCode(string code);
        Task<IResult> Insert(ProductAmount charAsgn);
        Task<IResult> Update(ProductAmount charAsgn);
        Task DeleteAll();
    }
}
