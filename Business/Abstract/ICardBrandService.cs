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
        Task<CardBrand> GetCardBrandById(Guid brandId);
        Task<List<CardBrand>> GetCardBrand();
    }
}
