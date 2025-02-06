using Microsoft.EntityFrameworkCore;
using DataAccess.Abstract;
using Entity;

using DataAccess.EFCore;
using Core.Abstract;
using Core.Concrete;

namespace DataAccess.Concrete
{
    public class PaymentRepository : Repository<PaymentTransaction>, IPaymentRepository
    {
        public PaymentRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
