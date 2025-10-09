using Business.Abstract;
using Business.Validaton;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using System.Net.Http.Headers;

namespace SanalMagaza.Business.Concrete
{
    public class CreditCardPrefixManager : ICreditCardPrefixService
    {
        private readonly IUnitofWork _unitOfWork;

        public CreditCardPrefixManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Create(CreditCardPrefix creditCardPrefix)
        {
            var result = await _unitOfWork.CreditCardPrefixs.SingleOrDefaultAsync(x => x.Prefix == creditCardPrefix.Prefix);
            if (result == null)
            {
                var list = creditCardPrefix.Validation();
                if (list.Count > 0)
                    return new Result(ResultStatus.Error, String.Join(", ", list.ToArray()));
                await _unitOfWork.CreditCardPrefixs.AddAsync(creditCardPrefix);
                var test = await _unitOfWork.CommitAsync();
                if (test == 1)
                    return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
                return new Result(ResultStatus.Error, "Hatalı İşlem");
            }
            return new Result(ResultStatus.Error, $"{creditCardPrefix.Prefix} Kodulu Kayıt Daha Önce Eklenmiş Ara Butonu ile İlgili Kayıda Ulaşabilirsiniz.");
        }

        public async Task<IResult> Delete(CreditCardPrefix creditCardPrefix)
        {
           await _unitOfWork.CreditCardPrefixs.Delete(creditCardPrefix);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt Silme İşlemi Tamanlandı");
            return new Result(ResultStatus.Error, "Hatalı İşlem");
        }

        public async Task<CreditCardPrefix> Get(Guid id)
        {
            var result = await _unitOfWork.CreditCardPrefixs.SingleOrDefaultAsync(x => x.Id == id);
           return result;
        }

        public async Task<CreditCardPrefix> GetByPrefix(string prefix)
        {
            return await _unitOfWork.CreditCardPrefixs.SingleOrDefaultAsync(x => x.Prefix == prefix); 
        }

        public async Task<List<CreditCardPrefix>> GetByPrefixList(string prefix)
        {
            return await _unitOfWork.CreditCardPrefixs.Find(x=>x.Prefix == prefix);
        }

        public async Task<IList<CreditCardPrefix>> GetAll()
        {
            var result = await _unitOfWork.CreditCardPrefixs.GetAllAsync();
            return result;
        }

        public async Task<IResult> Update(CreditCardPrefix creditCardPrefix)
        {
            var test = _unitOfWork.CreditCardPrefixs.SingleOrDefaultAsync(x=>x.Prefix ==  creditCardPrefix.Prefix);
            if (test != null)
            {
                var list = creditCardPrefix.Validation();
                if (list.Count > 0)
                    return new Result(ResultStatus.Error, String.Join(", ", list.ToArray()));
               await _unitOfWork.CreditCardPrefixs.UpdateAsync(creditCardPrefix);
                var result = await _unitOfWork.CommitAsync();
                if (result == 1)
                    return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
                return new Result(ResultStatus.Error, "Hatalı İşlem");
            }
            return new Result(ResultStatus.Error, $"{creditCardPrefix.Prefix} Kodulu Kayıt Daha Önce Eklenmiş Ara Butonu ile İlgili Kayıda Ulaşabilirsiniz.");
        }

        public async Task<List<CreditCardPrefix>> GetBankList(Guid BankId)
        {
            var result = await _unitOfWork.CreditCardPrefixs.Find(x=> x.CreditCardId == BankId);  
            return result;
        }


    }
}
