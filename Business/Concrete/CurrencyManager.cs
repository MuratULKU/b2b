using Business.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CurrencyManager : ICurrencyService
    {
        private readonly IUnitofWork _unitOfWork;

        public CurrencyManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Currency> GetCurrency(int id)
        {
           return _unitOfWork.Currencies.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
