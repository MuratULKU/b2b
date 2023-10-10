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
    public class BasketRepository : IBasketRepository
    {
        private readonly RepositoryContext _dbContext;

        public BasketRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Basket Delete(Basket basket)
        {
            _dbContext.Baskets.Remove(basket);
            return basket;
        }

        public bool Delete(Guid UserGuid)
        {
            throw new NotImplementedException();
        }

        public List<Basket> GetAll(Guid UserGuid)
        {
            return _dbContext.Baskets.Where(x => x.UserGuid == UserGuid).ToList();
        }

        public Basket Insert(Basket basket)
        {
           _dbContext.Baskets.Add(basket);
            return basket;
        }

        public void Insert(User user, Product product, double amount, double price, double vatRate, double vatPrice, double discountRate, double discountPrize)
        {
            var basket = _dbContext.Baskets.Where(x=>x.ProductCode == product.Code)
                .FirstOrDefault();
            if(basket == null)
            {
                basket = new Basket();
                basket.Id = Guid.NewGuid();
                basket.ProductGuid = product.Id;
                basket.ProductCode = product.Code;
                basket.ProductName = product.Name;
                basket.VatRate = vatRate;
                basket.VatPrice = vatPrice;
                basket.DiscountRate = discountRate;
                basket.DiscountPrice = discountPrize;
                basket.UserGuid = user.Id;
                _dbContext.Baskets.Add(basket);
            }
            else
            {
                basket.Amount += amount;//şimdilik sadece miktar güncelledik
                _dbContext.Baskets.Update(basket);
            }
            _dbContext.SaveChanges();
        }

        public Basket Update(Basket basket)
        {
            _dbContext.Baskets.Update(basket);
            _dbContext.SaveChanges();
            return basket;
        }
    }
}
