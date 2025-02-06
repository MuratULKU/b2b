using Core.Abstract;
using Entity;
using System.Linq.Expressions;


namespace DataAccess.Abstract
{
    public interface ICreditCardRepository:IRepository<CreditCard>
    {
        public Task<CreditCard> GetCreditCardAsync(Guid id, Expression<Func<CreditCard, object>>[] includes);
    }
}
