using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICreditCardInstallmentRepository
    {
        List<CreditCardInstallment> GetAll();
        CreditCardInstallment Get(Guid id);
        CreditCardInstallment Create(CreditCardInstallment creditCardInstallment);
        CreditCardInstallment Update(CreditCardInstallment creditCardInstallment);
        CreditCardInstallment Delete(CreditCardInstallment creditCardInstallment);
    }
}
