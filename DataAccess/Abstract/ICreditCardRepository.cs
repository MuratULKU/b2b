using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICreditCardRepository
    {
        List<CreditCard> GetAll();
        List<CreditCard> GetBankCreditCard(Guid bankid);
        CreditCard Get(Guid id);
        Task<CreditCard> Get(Guid bankId, int brandCode);
        CreditCard CreateCreditCard(CreditCard creditCard);
        CreditCard UpdateCreditCard(CreditCard creditCard);
        CreditCard DeleteCreditCard(CreditCard creditCard);
    }
}
