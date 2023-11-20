using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPaymentRepository
    {
        List<PaymentTransaction> GetFull();
        PaymentTransaction GetByCode(string code);
        PaymentTransaction GetById(Guid id,
           bool includeBank = false);
        PaymentTransaction GetByOrderNumber(Guid orderNumber,
            bool includeBank = false);
        PaymentTransaction Insert(PaymentTransaction paymentTransaction);
        PaymentTransaction Update(PaymentTransaction paymentTransaction);
    }
}
