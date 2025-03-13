using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        Task<List<CreditCard>> GetAll();
        Task<List<CreditCard>> GetBankCreditCard(Guid bankid);
        Task<CreditCard> Get(Guid id);
        Task<IDataResult<CreditCard>> Get(Guid bankId, int brandCode);
        Task<IResult> CreateCreditCard(CreditCard creditCard);
        Task<IResult> UpdateCreditCard(CreditCard creditCard);
        Task<IResult> DeleteCreditCard(CreditCard creditCard);
        Task<CreditCard> GetCreditCardByPrefix(string prefix,
           bool includeInstallments = false);
        Task<List<CreditCard>> GetFiltered(string filter);
    }
}
