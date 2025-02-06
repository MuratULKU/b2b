using Business.Abstract;
using Business.Validaton;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;

namespace SanalMagaza.Business.Concrete
{
    public class CreditCardPrefixManager : ICreditCardPrefixService
    {
        private readonly IUnitofWork _unitOfWork;

        public CreditCardPrefixManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult> Create(CreditCardPrefix creditCardPrefix)
        {
            return _unitOfWork.CreditCardPrefixs.AddAsync(creditCardPrefix);
        }

        public Task<IResult> Delete(CreditCardPrefix creditCardPrefix)
        {
            return _unitOfWork.CreditCardPrefixs.Delete(creditCardPrefix);
        }

        public async Task<CreditCardPrefix> Get(Guid id)
        {
            var result = await _unitOfWork.CreditCardPrefixs.SingleOrDefaultAsync(x => x.Id == id);
           return result.Data;
        }

        public async Task<CreditCardPrefix> GetByPrefix(string prefix)
        {
            var result = await _unitOfWork.CreditCardPrefixs.SingleOrDefaultAsync(x => x.Prefix == prefix); 
            return result.Data;
        }

        public async Task<IList<CreditCardPrefix>> GetAll()
        {
            var result = await _unitOfWork.CreditCardPrefixs.GetAllAsync();
            return result.Data;
        }

        public async Task<IResult> Update(CreditCardPrefix creditCardPrefix)
        {
            var result = _unitOfWork.CreditCardPrefixs.SingleOrDefaultAsync(x=>x.Prefix ==  creditCardPrefix.Prefix);
            if (result != null)
            {
                var list = creditCardPrefix.Validation();
                if (list.Count > 0)
                    return new Result(ResultStatus.Error, String.Join(", ", list.ToArray()));
                return await _unitOfWork.CreditCardPrefixs.UpdateAsync(creditCardPrefix);
            }
            return new Result(ResultStatus.Error, $"{creditCardPrefix.Prefix} Kodulu Kayıt Daha Önce Eklenmiş Ara Butonu ile İlgili Kayıda Ulaşabilirsiniz.");
        }

        public async Task<List<CreditCardPrefix>> GetBankList(Guid BankId)
        {
            var result = await _unitOfWork.CreditCardPrefixs.Find(x=> x.CreditCardId == BankId);  
            return result.Data;
        }


    }
}
