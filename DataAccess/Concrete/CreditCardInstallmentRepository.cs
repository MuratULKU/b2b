using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CreditCardInstallmentRepository : ICreditCardInstallmentRepository
    {
        private readonly RepositoryContext _dbContext;

        public CreditCardInstallmentRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CreditCardInstallment Create(CreditCardInstallment creditCardInstallment)
        {
            _dbContext.CreditCardInstallments.Add(creditCardInstallment);
            _dbContext.SaveChanges();
            return creditCardInstallment;
        }

        public CreditCardInstallment Delete(CreditCardInstallment creditCardInstallment)
        {
            _dbContext.CreditCardInstallments.Remove(creditCardInstallment);
            _dbContext.SaveChanges();
            return creditCardInstallment;
        }

        public CreditCardInstallment Get(Guid id)
        {
            return _dbContext.CreditCardInstallments.FirstOrDefault(x => x.Id == id);
        }

        public List<CreditCardInstallment> GetAll()
        {
            return _dbContext.CreditCardInstallments.ToList();
        }

        public CreditCardInstallment Update(CreditCardInstallment creditCardInstallment)
        {
            _dbContext.CreditCardInstallments.Update(creditCardInstallment);
            _dbContext.SaveChanges();
            return creditCardInstallment;
        }
    }
}
