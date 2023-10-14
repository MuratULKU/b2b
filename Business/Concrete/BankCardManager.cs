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
    public class BankCardManager:IBankCardService
    {
        private readonly IBankCardRepository bankCardRepository;
        public BankCardManager(IBankCardRepository bankCardRepository)
        {
            this.bankCardRepository = bankCardRepository;
        }

        public BankCard CreateBank(BankCard bankCard)
        {
            bankCardRepository.CreateBank(bankCard);

            return bankCard;
        }

        public void DeleteBank(BankCard bankCard)
        {
            bankCardRepository.DeleteBank(bankCard);
        }

        public List<BankCard> GetAllBank()
        {
            return bankCardRepository.GetAllBank();
        }

        public Task<BankCard> GetBank(Guid id)
        {
            return bankCardRepository.GetBank(id);
        }

        public Task<BankCard> GetBankbyCode(int Code)
        {
            return bankCardRepository.GetBankbyCode(Code);
        }
        public Task<List<BankParameter>> GetBankParameters(int bankId)
        {
            return bankCardRepository.GetBankParameters(bankId);
        }

        public Task<CreditCard> GetCreditCardByPrefix(string prefix, bool includeInstallments = false)
        {
            return bankCardRepository.GetCreditCardByPrefix(prefix, includeInstallments);
        }

      

        public BankCard UpdateBank(BankCard bankCard)
        {
            bankCardRepository.UpdateBank(bankCard);
            return bankCard;
        }

    }
}
