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
            _dbContext.SaveChanges();
            return basket;
        }

        public bool Delete(Guid UserGuid)
        {
            _dbContext.Baskets.Where(x => x.UserGuid == UserGuid).ExecuteDelete();
            return true;
        }

        public List<Basket> GetAll(Guid UserGuid)
        {
            return _dbContext.Baskets.Where(x => x.UserGuid == UserGuid).ToList();
        }

        public List<Basket> GetAll()
        {
           return _dbContext.Baskets.ToList();
        }

        public Basket Insert(Basket basket)
        {
           _dbContext.Baskets.Add(basket);
            return basket;
        }

        public void Insert(User user, Product product, double amount, double price, double vatRate, double vatPrice, double discountRate, double discountPrize,string docNo)
        {
            var basket = _dbContext.Baskets.Where(x=>x.ProductCode == product.Code && x.Send == false && x.UserGuid == user.Id)
                .FirstOrDefault();
            if(basket == null)
            {
                basket = new Basket();
                basket.Id = Guid.NewGuid();
                basket.DocNo = docNo;
                basket.ProductGuid = product.Id;
                basket.ProductCode = product.Code;
                basket.ProductName = product.Name;
                basket.Amount = amount;
                basket.Price = price;
                basket.VatRate = vatRate;
                basket.VatPrice = vatPrice;
                basket.DiscountRate = discountRate;
                basket.DiscountPrice = discountPrize;
                basket.UserGuid = user.Id;
                basket.Total = basket.Amount * basket.Price;
                basket.Date_ = DateTime.Now;
                _dbContext.Baskets.Add(basket);
            }
            else
            {
                basket.Amount += amount;
                basket.VatPrice += vatPrice;
                basket.Total += basket.Price * basket.Amount;
                basket.Date_ = DateTime.Now;
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
