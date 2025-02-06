using Core.Abstract;
using Entity;


namespace Business.Abstract
{
    public interface ICreditCardPrefixService
    {
        Task<IList<CreditCardPrefix>> GetAll();
        Task<CreditCardPrefix> Get(Guid id);
        Task<List<CreditCardPrefix>> GetBankList(Guid BankId);
        Task<CreditCardPrefix> GetByPrefix(string prefix);
        Task<IResult> Create(CreditCardPrefix creditCardPrefix);
        Task<IResult> Update(CreditCardPrefix creditCardPrefix);
        Task<IResult> Delete(CreditCardPrefix creditCardPrefix);
    }
}
