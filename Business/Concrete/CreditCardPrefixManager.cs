using Business.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanalMagaza.Business.Concrete
{
    public class CreditCardPrefixManager : ICreditCardPrefixService
    {
        private readonly ICreditCardPrefixRepository _creditCardPrefixRepository;

        public CreditCardPrefixManager(ICreditCardPrefixRepository creditCardPrefixRepository)
        {
            _creditCardPrefixRepository = creditCardPrefixRepository;
        }

        public CreditCardPrefix Create(CreditCardPrefix creditCardPrefix)
        {
            return _creditCardPrefixRepository.Create(creditCardPrefix);
        }

        public CreditCardPrefix Delete(CreditCardPrefix creditCardPrefix)
        {
            return _creditCardPrefixRepository.Delete(creditCardPrefix);
        }

        public CreditCardPrefix Get(Guid id)
        {
            return _creditCardPrefixRepository.Get(id);
        }

        public CreditCardPrefix GetByPrefix(string prefix)
        {
            return _creditCardPrefixRepository.GetByPrefix(prefix);
        }

        public List<CreditCardPrefix> GetAll()
        {
            return _creditCardPrefixRepository.GetAll();
        }

        public CreditCardPrefix Update(CreditCardPrefix creditCardPrefix)
        {
            return _creditCardPrefixRepository.Update(creditCardPrefix);
        }

        public List<CreditCardPrefix> GetBankList(int BankCode)
        {
            return _creditCardPrefixRepository.GetBankList(BankCode);
        }
    }
}
