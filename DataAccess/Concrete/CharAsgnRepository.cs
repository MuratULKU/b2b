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
    public class CharAsgnRepository : Repository<CharAsgn>, ICharAsgnRepository
    {
        public CharAsgnRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
