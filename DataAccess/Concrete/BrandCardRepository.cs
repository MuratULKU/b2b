using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class BrandCardRepository : Repository<CardBrand>, IBrandCardRepository
    {
        public BrandCardRepository(RepositoryContext context)
           : base(context)
        { }
    }
}
