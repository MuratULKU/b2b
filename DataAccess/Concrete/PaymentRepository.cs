using Microsoft.EntityFrameworkCore;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.EFCore;

namespace DataAccess.Concrete
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly RepositoryContext _dataContext;

        public PaymentRepository(RepositoryContext dataContext)
        {
            _dataContext = dataContext;
        }

        public List<PaymentTransaction> GetFull()
        {
            return _dataContext.PaymentTransactions.ToList();
        }
        public PaymentTransaction GetByCode(string code) {
            return _dataContext.PaymentTransactions.FirstOrDefault(x => x.ClientCode == code);
        }

        public PaymentTransaction GetById(Guid id, bool includeBank = false)
        {
            return _dataContext.PaymentTransactions.FirstOrDefault(x => x.BankCardId == id);
        }

        public PaymentTransaction GetByOrderNumber(Guid orderNumber, bool includeBank = false)
        {
            return _dataContext.PaymentTransactions
                .Include(x=>x.BankCard)
                .FirstOrDefault(x => x.OrderNumber == orderNumber);
        }

        public PaymentTransaction Insert(PaymentTransaction paymentTransaction)
        {
            _dataContext.PaymentTransactions.Add(paymentTransaction);
            _dataContext.SaveChanges();
            return paymentTransaction;
        }

        public PaymentTransaction Update(PaymentTransaction paymentTransaction)
        {
            _dataContext.PaymentTransactions.Update(paymentTransaction);
            _dataContext.SaveChanges();
            return paymentTransaction;
        }
    }
}
