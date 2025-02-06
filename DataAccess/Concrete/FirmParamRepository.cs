using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DataAccess.Concrete
{
    public class FirmParamRepository : Repository<FirmParam>, IFirmParamRepository
    {
        public FirmParamRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
