using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBasketRepository
    {
        List<Basket> GetAll(Guid UserGuid);
        Basket Insert(Basket basket);
        Basket Update(Basket basket);
        Basket Delete(Basket basket);
        bool Delete(Guid UserGuid);// sadece 1 sepet tutulacak kulanıcı için 
        void Insert(User user, Product product, double amount, double price, double vatRate, double vatPrice, double discountRate, double discountPrize);

    }
}
