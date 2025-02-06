using Core.Abstract;
using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class CreditCardInstallmentRepository : Repository<CreditCardInstallment>, ICreditCardInstallmentRepository
    {
        public CreditCardInstallmentRepository(RepositoryContext dbContext) : base(dbContext)
        {


        }


    }

}
