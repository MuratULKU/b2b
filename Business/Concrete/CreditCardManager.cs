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
     
        public CreditCardManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> CreateCreditCard(CreditCard creditCard)
        {
            try
            {
                await _unitOfWork.Repository<CreditCard>().AddAsync(creditCard);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result == 1)
                    return new Result(ResultStatus.Success, "Kayıt İşlemi Tamamlandı");

                return new Result(ResultStatus.Error, "Hatalı İşlem");
            }
            catch (Exception ex)
            {
              
                return new Result(ResultStatus.Error, $"Beklenmedik bir hata oluştu: {ex.Message}");
            }
        }


        public async Task<IResult> DeleteCreditCard(CreditCard creditCard)
        {
            try
            {

                await _unitOfWork.Repository<CreditCard>().Delete(creditCard);

                var result = await _unitOfWork.SaveChangesAsync();
                if (result == 1)
                    return new Result(ResultStatus.Success, "Kayıt Silme İşlemi Tamanlandı");
                return new Result(ResultStatus.Error, "Hatalı İşlem");
            }
            catch (Exception ex)
            {
               
                return new Result(ResultStatus.Error, "Hatalı İşlem");
            }

        }

        public async Task<CreditCard> Get(Guid id)
        {
            var result = await _unitOfWork.Repository<CreditCard>().SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }



        public async Task<CreditCard> Get(Guid bankId, Guid brandId)
        {
            return await _unitOfWork.Repository<CreditCard>().FirstOrDefaultAsync(x => x.BankCardId == bankId && x.CardBrandId == brandId);
        }

        public async Task<List<CreditCard>> GetAll()
        {
            var result = await _unitOfWork.Repository<CreditCard>().GetAllAsync(x => x.Include(x => x.CardBrand).AsNoTracking());
            return result;
        }

        public async Task<List<CreditCard>> GetBankCreditCard(Guid bankid)
        {
            var result = await _unitOfWork.Repository<CreditCard>().Find(x => x.CardBrandId == bankid);
            if (result == null)
                return null;
            return result;
        }

        public async Task<CreditCard> GetCreditCardByPrefix(string prefix, bool includeInstallments = false)
        {
            var result = await _unitOfWork.Repository<CreditCardPrefix>().SingleOrDefaultAsync(x => x.Prefix == prefix);
            var creditCard = await _unitOfWork.Repository<CreditCard>().SingleOrDefaultAsync(x => x.Id == result.CreditCardId,x=>x.Include(x=>x.Bank));
            
            if (creditCard != null)
            {
                var instalment = await _unitOfWork.Repository<CreditCardInstallment>().Find(x => x.CreditCardId == creditCard.Id );
                creditCard.Installments = instalment;
            }

            return creditCard;
        }

        public Task<List<CreditCard>> GetFiltered(string filter)
        {
            return _unitOfWork.Repository<CreditCard>().GetFilteredAsync(x => x.Name.ToLower().Contains(filter), x => x.Include(x => x.CardBrand));
        }

        public async Task<List<CreditCardInstallment>> GetPosIdCreditCard(Guid posid)
        {
            return await _unitOfWork.Repository<CreditCardInstallment>().Find(x => x.VirtualPosId == posid,x=>x.Include(x=>x.CreditCard));
        }

        public async Task<IResult> UpdateCreditCard(CreditCard creditCard)
        {
            await _unitOfWork.Repository<CreditCard>().UpdateAsync(creditCard);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
            return new Result(ResultStatus.Error, "Hatalı İşlem");
        }
    }
}
