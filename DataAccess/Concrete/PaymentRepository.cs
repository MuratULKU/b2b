using Microsoft.EntityFrameworkCore;
using DataAccess.Abstract;
using Entity;

using DataAccess.EFCore;
using Core.Abstract;
using Core.Concrete;
using System.ComponentModel.Design;

namespace DataAccess.Concrete
{
    public class PaymentRepository : Repository<PaymentTransaction>, IPaymentRepository
    {
        public PaymentRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<List<PaymentTransaction>> GetPaymentTransaction(Guid companyId, DateTime startDate, DateTime endDate, int currentPage = 0, int pageSize = 10)
        {
            return await base.dbContext.Set<PaymentTransaction>()
                 .Include(x => x.User)
                 .Include(x => x.Company)
                 .Include(x=>x.VirtualPos)
                 .Where(x => x.CompanyId == companyId && x.CreateDate >= startDate && x.CreateDate <= endDate)
                 .OrderByDescending(x => x.CreateDate)
                 .Skip(currentPage * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
        }

        public async Task<List<PaymentTransaction>> GetPaymentTransaction(DateTime startDate, DateTime endDate, int currentPage = 0, int pageSize = 10)
        {
            return await base.dbContext.Set<PaymentTransaction>()
                .Include(x => x.User)
                .Include(x => x.Company)
                .Include (x => x.VirtualPos)
                .Where(x=> x.CreateDate >= startDate && x.CreateDate <= endDate)
                .OrderByDescending(x => x.CreateDate)
                .Skip(currentPage * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
