using Core.Abstract;
using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<List<Company>> GetPagedCompanies(int currentPage, int pageSize)
        {
          return await base.dbContext.Set<Company>()
                .Skip(currentPage*pageSize).Take(pageSize)
                .ToListAsync();
        }
    }
}
