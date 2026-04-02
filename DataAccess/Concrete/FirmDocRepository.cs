using Core.Abstract;
using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class FirmDocRepository : Repository<FirmDoc>, IFirmDocRepository
    {
        public FirmDocRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<int> DeleteAll()
        {
            dbContext.Set<FirmDoc>().ExecuteDelete();
            return await dbContext.SaveChangesAsync();
        }
    }
}
