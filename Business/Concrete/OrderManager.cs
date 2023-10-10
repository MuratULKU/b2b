using Business.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        public List<Basket> Basket { get; set; } = new();

        private IBasketRepository _basketRepository;

        public OrderManager(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public void AddProduct(User user, Product product, double amount, double price)
        {
            _basketRepository.Insert(user,product,amount,price,1,0,0,0);
        }

        public List<Basket> GetAll(User user)
        {
            if (user != null)
            return _basketRepository.GetAll(user.Id).ToList();
            return null;
        }
    }
}
