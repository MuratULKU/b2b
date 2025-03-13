using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        Task<List<PaymentTransaction>> GetFull(DateTime startDate, DateTime endDate,int currentPage, int pageSize);
        Task<IDataResult<List<PaymentTransaction>>> GetUserId(Guid userId, DateTime startDate, DateTime endDate);
        Task<int> GetTotalCount(DateTime startDate, DateTime endDate);
        Task<int> GetCompanyTotalCount(Guid companyId, DateTime startDate, DateTime endDate);
        Task<List<PaymentTransaction>> GetPaymentTransaction(Guid userId, DateTime startDate, DateTime endDate, int currentPage = 0, int pageSize = 10);
        Task<IDataResult<PaymentTransaction>> GetById(Guid id,
           bool includeBank = false);
        Task<IDataResult<PaymentTransaction>> GetByOrderNumber(Guid orderNumber,
            bool includeBank = false);
        Task<IResult> Insert(PaymentTransaction paymentTransaction);
        Task<IResult> Update(PaymentTransaction paymentTransaction);
        decimal GetTotalAmount();
        decimal GetTotalAmount(DateTime startDate);
    }
}
