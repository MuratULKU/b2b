using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICreditCardPrefixService
    {
        List<CreditCardPrefix> GetAll();
        CreditCardPrefix Get(Guid id);
        List<CreditCardPrefix> GetBankList(int BankCode);
        CreditCardPrefix GetByPrefix(string prefix);
        CreditCardPrefix Create(CreditCardPrefix creditCardPrefix);
        CreditCardPrefix Update(CreditCardPrefix creditCardPrefix);
        CreditCardPrefix Delete(CreditCardPrefix creditCardPrefix);
    }
}
