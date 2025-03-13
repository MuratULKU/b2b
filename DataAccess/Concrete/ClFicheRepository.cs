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
    public class ClFicheRepository : Repository<ClFiche>, IClFicheRepository
    {
        public ClFicheRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
