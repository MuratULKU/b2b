using Microsoft.EntityFrameworkCore;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EFCore;

namespace DataAccess.Concrete
{
    public class BankCardRepository : IBankCardRepository
    {
        private readonly RepositoryContext _dataContext;
        public BankCardRepository(RepositoryContext dataContext)
        {
            _dataContext = dataContext;
        }
        public BankCard CreateBank(BankCard bankCard)
        {
            _dataContext.BankCards.Add(bankCard);
            _dataContext.SaveChanges();
            return bankCard;
        }

        public void DeleteBank(BankCard bankCard)
        {
            _dataContext.BankCards.Remove(bankCard);
            _dataContext.SaveChanges();
        }

        public List<BankCard> GetAllBank()
        {
            return _dataContext.BankCards.ToList();
        }




        public BankCard UpdateBank(BankCard bankCard)
        {
            _dataContext.Update(bankCard);
            _dataContext.SaveChanges();
            return bankCard;
        }

        public Task<CreditCard> GetCreditCardByPrefix(string prefix,
            bool includeInstallments = false)
        {
            if (string.IsNullOrEmpty(prefix))
                return Task.FromResult<CreditCard>(null);

            prefix = prefix.Trim();

            var query = _dataContext.CreditCards
                .Where(x => x.Prefixes.Any(cp => cp.Prefix.Equals(prefix)));

            if (includeInstallments)
                query = query.Include(x => x.Installments);

            var c = query.FirstOrDefaultAsync();
            return c;
        }


        public Task<List<BankParameter>> GetBankParameters(Guid bankId)
        {
            return _dataContext.BankParameters
              .Where(bp => bp.BankCardId.Equals(bankId))
              .ToListAsync();
        }

        public Task<BankCard> GetBank(Guid id)
        {
            return _dataContext.BankCards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<BankCard> GetBankbyCode(int Code)
        {
            return _dataContext.BankCards
                .Include(x => x.CreditCards)
                .FirstOrDefaultAsync(x => x.BankCode == Code);
        }

        public Task<BankCard> GetBank(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CardBrand>> GetCardBrandById(int brandCode)
        {
            return _dataContext.CardBrands.Where(x => x.Code == brandCode)
                .ToListAsync();
        }

        public Task<List<CardBrand>> GetCardBrand()
        {
            return _dataContext.CardBrands
               .ToListAsync();
        }
    }
}
