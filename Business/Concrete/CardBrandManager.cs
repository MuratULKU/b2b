using Business.Abstract;
using Core.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CardBrandManager : ICardBrandService
    {
        private readonly IUnitofWork _unitOfWork;

        public CardBrandManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CardBrand>> GetCardBrand()
        {
            return await _unitOfWork.BrandCards.GetAllAsync();
        }

        public async Task<CardBrand> GetCardBrandById(Guid brandId)
        {
            return await _unitOfWork.BrandCards.FirstOrDefaultAsync(x => x.Id == brandId);
        }
    }
}
