using Microsoft.EntityFrameworkCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataAccess.EFCore;

namespace DataAccess.Concrete
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly RepositoryContext _dataContext;

        public CreditCardRepository(RepositoryContext dataContext)
        {
            _dataContext = dataContext;
        }

        public CreditCard CreateCreditCard(CreditCard creditCard)
        {
            _dataContext.CreditCards.Add(creditCard);
            _dataContext.SaveChanges();
            return creditCard;
        }

        public CreditCard DeleteCreditCard(CreditCard creditCard)
        {
            _dataContext.CreditCards.Remove(creditCard);
            _dataContext.SaveChanges();
            return creditCard;
        }

        public List<CreditCard> GetAll()
        {
            return _dataContext.CreditCards
                .Include(x=>x.Bank)
                .Include(x=>x.Installments)
                .ToList();
        }

        public CreditCard Get(Guid id)
        {
            return _dataContext.CreditCards.FirstOrDefault(x => x.Id == id);
        }

        
        public CreditCard UpdateCreditCard(CreditCard creditCard)
        {
            _dataContext.CreditCards.Update(creditCard);
            _dataContext.SaveChanges();
            return creditCard;
        }

        public List<CreditCard> GetBankCreditCard(Guid bankid)
        {
            return _dataContext.CreditCards.Where(x => x.BankCardId == bankid).ToList();
        }

        public Task<CreditCard> Get(Guid bankId, int brandCode)
        {
            return _dataContext.CreditCards.FirstOrDefaultAsync(x=>x.BankCardId== bankId && x.BrandCode == brandCode);
        }
    }
}
