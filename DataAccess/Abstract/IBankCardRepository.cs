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
        Task<List<BankParameter>> GetBankParameters(Guid bankId);
        Task<CreditCard> GetCreditCardByPrefix(string prefix,
            bool includeInstallments = false);
        //create
        Task<List<CardBrand>> GetCardBrandById(int brandCode);
        Task<List<CardBrand>> GetCardBrand();
        BankCard CreateBank(BankCard bankCard);
        BankCard UpdateBank(BankCard bankCard);
        void DeleteBank(BankCard bankCard);
    }
}
