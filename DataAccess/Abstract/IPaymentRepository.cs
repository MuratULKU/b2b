using Core.Abstract;
using DataAccess.Concrete;
using Entity;

namespace DataAccess.Abstract
{
    public interface IPaymentRepository:IRepository<PaymentTransaction>
    {
       
    }
}
