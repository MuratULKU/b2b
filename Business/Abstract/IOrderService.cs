using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        List<Basket> Basket { get; set; }
        void AddProduct(User user, Product product, double amount, double price, string docNo);
        void DeleteProduct(Basket basket);
        void DeleteBasket(Guid guid);
        void UpdateBasket(Basket basket);
        List<Basket> GetAll(User user);
        List<Basket> GetAll(Guid userId);
        List<Basket> GetAll(string DocNo);
        List<Basket> GetAllBasket();
        List<Basket> GetAllFiche(bool send);
    }
}
