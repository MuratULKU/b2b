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
        List<CreditCardInstallment> GetAll();
        CreditCardInstallment Get(Guid id);
        List<CreditCardInstallment> GeyBankId(Guid id);
        List<CreditCardInstallment> GeyCreditId(Guid id);

        CreditCardInstallment Create(CreditCardInstallment creditCardInstallment);
        IResult Update(CreditCardInstallment creditCardInstallment);
        CreditCardInstallment Delete(CreditCardInstallment creditCardInstallment);
    }
}
