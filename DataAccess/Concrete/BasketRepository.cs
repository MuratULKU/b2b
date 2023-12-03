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

        public List<Basket> GetAll(string DocNo)
        {
            return _dbContext.Baskets.Where(x => x.DocNo == DocNo).ToList();
        }
        public List<Basket> GetAllFiche(bool send)
        {
            return _dbContext.Baskets.Where(x=>x.Send == send).GroupBy(x => x.DocNo).
                Select(grp => new Basket{
                    DocNo = grp.Max(x=>x.DocNo),
                    Amount = grp.Sum(x=>x.Amount),
                    UserGuid = grp.Max(x=>x.UserGuid),
                    Date_ = grp.Max(x=>x.Date_)
                }).ToList();
        }

        public Basket Insert(Basket basket)
        {
           _dbContext.Baskets.Add(basket);
            _dbContext.SaveChanges();
            return basket;
        }

        public void Insert(User user, Product product, double amount, double price, double vatRate, double vatPrice, double discountRate, double discountPrize,string docNo)
        {
            List<Basket>  baskets = _dbContext.Baskets.Where(x=>x.Send == false && x.UserGuid == user.Id)
                .ToList();
            Basket basket = baskets.Find(x => x.ProductCode == product.Code);
            if ( basket == null)
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
                basket.LineNUmber = baskets.Count+1;
                Insert(basket);
            }
            else
            {
                basket.Amount += amount;
                basket.VatPrice += vatPrice;
                basket.Total += basket.Price * basket.Amount;
                basket.Date_ = DateTime.Now;
                Update(basket);
            }
        
        }

        public Basket Update(Basket basket)
        {
            _dbContext.Baskets.Update(basket);
            _dbContext.SaveChanges();
            return basket;
        }
    }
}
