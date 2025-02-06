using DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstract;
using Entity;
using System.Linq;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Concrete;
using System.Linq.Expressions;

namespace SanalMagaza.DataAccess.Concrete
{
    public class VirtualPosRepository : Repository<VirtualPos>, IVirtualPosRepository
    {
        public VirtualPosRepository(RepositoryContext context)
           : base(context)
        {
        }

        public async Task<VirtualPos> GetVirtualPosAsync(Guid id, Expression<Func<VirtualPos, object>>[] includes)
        {
            IQueryable<VirtualPos> query = this.dbContext.Set<VirtualPos>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
