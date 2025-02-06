using Microsoft.EntityFrameworkCore;
using Entity;

using DataAccess.Abstract;
using DataAccess.EFCore;
using Core.Abstract;
using Core.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public class CreditCardRepository :Repository<CreditCard>, ICreditCardRepository
    {
        public CreditCardRepository(RepositoryContext context)
           : base(context)
        { }

        public async Task<CreditCard> GetCreditCardAsync(Guid id, Expression<Func<CreditCard, object>>[] includes)
        {
            IQueryable<CreditCard> query = this.dbContext.Set<CreditCard>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(x=>x.Id == id);
        }
    }
}
