using Core.Abstract;
using DataAccess.Concrete;
using Entity;

namespace DataAccess.Abstract
{
    public interface IPaymentRepository:IRepository<PaymentTransaction>
    {
       Task<List<PaymentTransaction>> GetPaymentTransaction(Guid companyId, DateTime startDate, DateTime endDate, int currentPage = 0, int pageSize = 10);
       Task<List<PaymentTransaction>> GetPaymentTransaction( DateTime startDate, DateTime endDate, int currentPage = 0, int pageSize = 10);
    }
}
