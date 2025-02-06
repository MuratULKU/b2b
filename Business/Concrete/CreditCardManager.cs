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
    public class CreditCardManager : ICreditCardService
    {
        private readonly IUnitofWork _unitOfWork;

        public CreditCardManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult> CreateCreditCard(CreditCard creditCard)
        {
            return _unitOfWork.CreditCards.AddAsync(creditCard);
        }

        public Task<IResult> DeleteCreditCard(CreditCard creditCard)
        {
           return _unitOfWork.CreditCards.Delete(creditCard);
        }

        public async Task<CreditCard> Get(Guid id)
        {
            var result = await  _unitOfWork.CreditCards.SingleOrDefaultAsync(x=>x.Id == id);
            return result.Data;
        }



        public Task<IDataResult<CreditCard>> Get(Guid bankId, int brandCode)
        {
            return null;
        }

        public async Task<List<CreditCard>> GetAll()
        {
            var result = await _unitOfWork.CreditCards.GetAllAsync();
            return result.Data;
        }

        public async Task<List<CreditCard>> GetBankCreditCard(Guid bankid)
        {
            var result = await _unitOfWork.CreditCards.Find(x=>x.CardBrandId == bankid);
            if (result == null)
                return null;
            return result.Data;
        }

        public async Task<CreditCard> GetCreditCardByPrefix(string prefix, bool includeInstallments = false)
        {
            var result = await _unitOfWork.CreditCardPrefixs.SingleOrDefaultAsync(x=>x.Prefix == prefix);
            var creditCard = await _unitOfWork.CreditCards.SingleOrDefaultAsync(x=>x.Id == result.Data.CreditCardId);
            if (creditCard != null)
            {
                var instalment = await _unitOfWork.CreditCardInstallment.Find(x => x.CreditCardId == creditCard.Data.Id);
                creditCard.Data.Installments =  instalment.Data;
            }
               
            return creditCard.Data;
        }

        public async Task<IResult> UpdateCreditCard(CreditCard creditCard)
        {
           return await _unitOfWork.CreditCards.UpdateAsync(creditCard);
        }
    }
}
