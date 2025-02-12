using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class OrdFicheRepository : Repository<OrdFiche>, IOrdFicheRepository
    {
        public OrdFicheRepository(RepositoryContext context) : base(context)
        {

        }
        public async Task<OrdFiche> GetOrderFiche(int Logicalref)
        {
            return await base.dbContext.Set<OrdFiche>()
                   .FirstOrDefaultAsync(x => x.LogicalRef == Logicalref);

        }

        public async Task<List<OrdFiche>> GetOrderFiche(int TrCode, int CurrentPage, int PageSize)
        {
            return await base.dbContext.Set<OrdFiche>()
                .Include(x => x.User)
                  .Include(x => x.Lines)
                  .ThenInclude(x => x.Product)
          .Where(x => x.TrCode == TrCode)
          .OrderByDescending(x => x.Date_)
          .Skip(CurrentPage * PageSize)
          .Take(PageSize)
          .ToListAsync();
        }

        public async Task<List<OrdFiche>> GetOrderFiche(Guid CompanyId, int TrCode, int CurrentPage, int PageSize)
        {
            return await base.dbContext.Set<OrdFiche>()
                  .Include(x => x.Lines)
                  .ThenInclude(x => x.Product)
          .Where(x => x.TrCode == TrCode && x.CompanyId == CompanyId)
          .OrderByDescending(x => x.Date_)
          .Skip(CurrentPage * PageSize)
          .Take(PageSize)
          .ToListAsync();
        }
    }
}
