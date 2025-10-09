using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using Core.Logger;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILoggerService _logger;
        public CreditCardManager(IUnitofWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> CreateCreditCard(CreditCard creditCard)
        {
            try
            {
                await _unitOfWork.CreditCards.AddAsync(creditCard);
                var result = await _unitOfWork.CommitAsync();

                if (result == 1)
                    return new Result(ResultStatus.Success, "Kayıt İşlemi Tamamlandı");

                return new Result(ResultStatus.Error, "Hatalı İşlem");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return new Result(ResultStatus.Error, $"Beklenmedik bir hata oluştu: {ex.Message}");
            }
        }


        public async Task<IResult> DeleteCreditCard(CreditCard creditCard)
        {
            try
            {

                await _unitOfWork.CreditCards.Delete(creditCard);

                var result = await _unitOfWork.CommitAsync();
                if (result == 1)
                    return new Result(ResultStatus.Success, "Kayıt Silme İşlemi Tamanlandı");
                return new Result(ResultStatus.Error, "Hatalı İşlem");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new Result(ResultStatus.Error, "Hatalı İşlem");
            }

        }

        public async Task<CreditCard> Get(Guid id)
        {
            var result = await _unitOfWork.CreditCards.SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }



        public async Task<CreditCard> Get(Guid bankId, Guid brandId)
        {
            return await _unitOfWork.CreditCards.FirstOrDefaultAsync(x => x.BankCardId == bankId && x.CardBrandId == brandId);
        }

        public async Task<List<CreditCard>> GetAll()
        {
            var result = await _unitOfWork.CreditCards.GetAllAsync(x => x.Include(x => x.CardBrand).AsNoTracking());
            return result;
        }

        public async Task<List<CreditCard>> GetBankCreditCard(Guid bankid)
        {
            var result = await _unitOfWork.CreditCards.Find(x => x.CardBrandId == bankid);
            if (result == null)
                return null;
            return result;
        }

        public async Task<CreditCard> GetCreditCardByPrefix(string prefix, bool includeInstallments = false)
        {
            var result = await _unitOfWork.CreditCardPrefixs.SingleOrDefaultAsync(x => x.Prefix == prefix);
            var creditCard = await _unitOfWork.CreditCards.SingleOrDefaultAsync(x => x.Id == result.CreditCardId);
            if (creditCard != null)
            {
                var instalment = await _unitOfWork.CreditCardInstallment.Find(x => x.CreditCardId == creditCard.Id);
                creditCard.Installments = instalment;
            }

            return creditCard;
        }

        public Task<List<CreditCard>> GetFiltered(string filter)
        {
            return _unitOfWork.CreditCards.GetFilteredAsync(x => x.Name.ToLower().Contains(filter), x => x.Include(x => x.CardBrand));
        }

        public async Task<IResult> UpdateCreditCard(CreditCard creditCard)
        {
            await _unitOfWork.CreditCards.UpdateAsync(creditCard);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
            return new Result(ResultStatus.Error, "Hatalı İşlem");
        }
    }
}
