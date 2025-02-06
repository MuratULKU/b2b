using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CharSetRepository : Repository<CharSet>, ICharSetRepository
    {
        public CharSetRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
