using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBankCardService
    {
        List<BankCard> GetAllBank();
        Task<BankCard> GetBank(Guid id);
        Task<BankCard> GetBankbyCode(int Code);
        BankCard CreateBank(BankCard bankCard);
        BankCard UpdateBank(BankCard bankCard);

        void DeleteBank(BankCard bankCard);

      
        Task<CreditCard> GetCreditCardByPrefix(string prefix,
            bool includeInstallments = false);
        Task<List<BankParameter>> GetBankParameters(int bankId);
    }
}
