using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class BankParameterRepository : IBankParameterRepository
    {
        private readonly RepositoryContext _dataContext;

        public BankParameterRepository(RepositoryContext dataContext)
        {
            _dataContext = dataContext;
        }

        public BankParameter CreateBankParameter(BankParameter bankParameter)
        {
            _dataContext.BankParameters.Add(bankParameter);
            _dataContext.SaveChanges();
            return bankParameter;
        }

        public BankParameter DeleteBankParameter(BankParameter bankParameter)
        {
            _dataContext.BankParameters.Remove(bankParameter);
            _dataContext.SaveChanges();
            return bankParameter;
        }

        public List<BankParameter> GetAll(Guid BankId)
        {
            return _dataContext.BankParameters.Where(x => x.BankCardId == BankId).ToList();
        }

        public BankParameter UpdateBankParameter(BankParameter bankParameter)
        {
            _dataContext.BankParameters.Update(bankParameter);
            _dataContext.SaveChanges();
            return bankParameter;
        }
    }
}
