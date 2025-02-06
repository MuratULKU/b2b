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
        Task<IDataResult<List<PaymentTransaction>>> GetFull(DateTime startDate, DateTime endDate);
        Task<IDataResult<List<PaymentTransaction>>> GetUserId(Guid userId, DateTime startDate, DateTime endDate);
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
