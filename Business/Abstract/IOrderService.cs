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
        void AddProduct(User user, Product product, double amount, double price);

        List<Basket> GetAll(User user);
    }
}
