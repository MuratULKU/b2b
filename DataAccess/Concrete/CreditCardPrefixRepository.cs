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
    public class CreditCardPrefixRepository : ICreditCardPrefixRepository
    {
        private readonly RepositoryContext _dbContext;

        public CreditCardPrefixRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CreditCardPrefix Create(CreditCardPrefix creditCardPrefix)
        {
            if (creditCardPrefix != null)
            {
                _dbContext.CreditCardPrefixes.Add(creditCardPrefix);
                _dbContext.SaveChanges();
                return creditCardPrefix;
            }
            return null;
        }

        public CreditCardPrefix Delete(CreditCardPrefix creditCardPrefix)
        {
            _dbContext.CreditCardPrefixes.Remove(creditCardPrefix);
            _dbContext.SaveChanges();
            return creditCardPrefix;
        }

        public CreditCardPrefix Get(Guid id)
        {
            return _dbContext.CreditCardPrefixes.FirstOrDefault(x => x.Id == id);
        }

        public CreditCardPrefix GetByPrefix(string prefix)
        {
                return _dbContext.CreditCardPrefixes.
                FirstOrDefault(x => x.Prefix == prefix);
        }

        public List<CreditCardPrefix> GetAll()
        {
            return _dbContext.CreditCardPrefixes.ToList();
        }

        public List<CreditCardPrefix> GetBankList(int BankCode)
        {
            return _dbContext.CreditCardPrefixes.Where(x=>x.BankCode == BankCode).OrderBy(x=>x.Prefix).ToList();
        }

        public CreditCardPrefix Update(CreditCardPrefix creditCardPrefix)
        {
            if (creditCardPrefix != null)
            {
                _dbContext.CreditCardPrefixes.Update(creditCardPrefix);
                _dbContext.SaveChanges();
                return creditCardPrefix;
            }
            return null;
        }
    }
}
