using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class CreditCardPrefixRepository :Repository<CreditCardPrefix>, ICreditCardPrefixRepository
    {
        public CreditCardPrefixRepository(RepositoryContext context)
           : base(context)
        { }

       

    }
}
