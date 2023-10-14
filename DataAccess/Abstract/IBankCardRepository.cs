using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBankCardRepository
    {
        List<BankCard> GetAllBank();
        Task<BankCard> GetBank(Guid id);
        Task<BankCard> GetBankbyCode(int Code);


        Task<List<BankParameter>> GetBankParameters(int bankId);
        Task<CreditCard> GetCreditCardByPrefix(string prefix,
            bool includeInstallments = false);
        //create
        BankCard CreateBank(BankCard bankCard);
        BankCard UpdateBank(BankCard bankCard);
        void DeleteBank(BankCard bankCard);
    }
}
