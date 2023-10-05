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
    public class PriceListRepository : IPriceListRepository
    {
        private readonly RepositoryContext _dbContext;

        public PriceListRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(PriceList priceList)
        {
            _dbContext.PriceLists.Remove(priceList);
        }

        public List<PriceList> GetAll()
        {
            return _dbContext.PriceLists.ToList();
        }

        public Task<PriceList> GetByCode(string code)
        {
            return _dbContext.PriceLists.FirstOrDefaultAsync(x=>x.Code == code);
        }

        public Task<PriceList> GetByProductCode(string productCode)
        {
            return _dbContext.PriceLists.FirstOrDefaultAsync(x => x.ProductCode == productCode);
        }

        public Task<PriceList> GetByProductCode(string productCode, int priorty)
        {
            return _dbContext.PriceLists.FirstOrDefaultAsync(x=>x.ProductCode == productCode && x.Priorty == priorty);
        }

        public void Insert(PriceList priceList)
        {
            _dbContext.PriceLists.Add(priceList);
            _dbContext.SaveChanges();
        }

        public void Update(PriceList priceList)
        {
            _dbContext.PriceLists.Update(priceList);
            _dbContext.SaveChanges();
        }
    }
}
