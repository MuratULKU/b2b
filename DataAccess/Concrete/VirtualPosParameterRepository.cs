using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Concrete
{
    public class VirtualPosParameterRepository : Repository<VirtualPosParameter>, IVirtualPosParameterRepository
    {
        public VirtualPosParameterRepository(DbContext context) : base(context)
        {
        }
    }
}
