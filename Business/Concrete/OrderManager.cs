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

      

        public void AddProduct(User user, Product product, double amount, double price,string docNo)
        {
            _basketRepository.Insert(user, product, amount, price, product.SellVat ?? 0, (price * product.SellVat ?? 0) / 100, 0, 0, docNo);
        }

        public List<Basket> GetAll(User user)
        {
            if (user != null)
            Basket= _basketRepository.GetAll(user.Id).ToList();
            return Basket;
            
        }

        public List<Basket> GetAll(Guid userId)
        {
            return _basketRepository.GetAll(userId).Where(x=>x.Send == true).ToList();
        }

        public List<Basket> GetAllFiche(bool send)
        {
            return _basketRepository.GetAllFiche(send);
        }

        public void DeleteProduct(Basket basket)
        {
            _basketRepository.Delete(basket);
            
        }

        public void DeleteBasket(Guid guid)
        {
            _basketRepository.Delete(guid);
        }

        public void UpdateBasket(Basket basket) {
            _basketRepository.Update(basket);
        }

        public List<Basket> GetAllBasket()
        {
           return _basketRepository.GetAll().Where(x=>x.Send == true).ToList();
        }

        public List<Basket> GetAll(string DocNo)
        {
            return _basketRepository.GetAll(DocNo);
        }
    }
}
