using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICreditCardPrefixRepository
    {
        List<CreditCardPrefix> GetAll();
        CreditCardPrefix Get(Guid id);
        CreditCardPrefix GetByPrefix(string prefix);
        List<CreditCardPrefix> GetBankList(int BankCode); //bunu gerek yoksa silelim
        CreditCardPrefix Create(CreditCardPrefix creditCardPrefix);
        CreditCardPrefix Update(CreditCardPrefix creditCardPrefix);
        CreditCardPrefix Delete(CreditCardPrefix creditCardPrefix);
    }
}
