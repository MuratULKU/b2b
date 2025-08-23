using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICreditCardInstallmentService
    {
        Task<List<CreditCardInstallment>> GetAll();
        Task<CreditCardInstallment> Get(Guid id);
        Task<List<CreditCardInstallment>> GeyBankId(Guid id);
        Task<List<CreditCardInstallment>> GeyCreditId(Guid id);

        Task<CreditCardInstallment> Create(CreditCardInstallment creditCardInstallment);
        Task<IResult> Update(CreditCardInstallment creditCardInstallment);
        Task<CreditCardInstallment> Delete(CreditCardInstallment creditCardInstallment);
    }
}
