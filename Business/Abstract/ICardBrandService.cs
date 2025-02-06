using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICardBrandService
    {
        Task<IDataResult<List<CardBrand>>> GetCardBrandById(int brandCode);
        Task<List<CardBrand>> GetCardBrand();
    }
}
