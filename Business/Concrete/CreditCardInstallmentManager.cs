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
    public class CreditCardInstallmentManager : ICreditCardInstallmentService
    {
        private readonly ICreditCardInstallmentRepository _creditCardInstallmentRepository;

        public CreditCardInstallmentManager(ICreditCardInstallmentRepository creditCardInstallmentRepository)
        {
            _creditCardInstallmentRepository = creditCardInstallmentRepository;
        }

        public CreditCardInstallment Create(CreditCardInstallment creditCardInstallment)
        {

            return _creditCardInstallmentRepository.Create(creditCardInstallment);
        }

        public CreditCardInstallment Delete(CreditCardInstallment creditCardInstallment)
        {
            return _creditCardInstallmentRepository.Delete(creditCardInstallment);
        }

        public CreditCardInstallment Get(Guid id)
        {
            return _creditCardInstallmentRepository.Get(id);
        }

        public List<CreditCardInstallment> GetAll()
        {
            return _creditCardInstallmentRepository.GetAll();
        }

        public CreditCardInstallment Update(CreditCardInstallment creditCardInstallment)
        {
            return _creditCardInstallmentRepository.Update(creditCardInstallment);
        }
    }
}
