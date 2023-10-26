using DataAccess.Abstract;
using Business.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BankParameterManager : IBankParameterService
    {
        private readonly IBankParameterRepository bankParameterRepository;

        public BankParameterManager(IBankParameterRepository bankParameterRepository)
        {
            this.bankParameterRepository = bankParameterRepository;
        }

        public BankParameter CreateBankParameter(BankParameter bankParameter)
        {
            return bankParameterRepository.CreateBankParameter(bankParameter);
        }

        public BankParameter DeleteBankParameter(BankParameter bankParameter)
        {
            return bankParameterRepository.DeleteBankParameter(bankParameter);
        }

        public List<BankParameter> GetAll(Guid BankId)
        {
            return bankParameterRepository.GetAll(BankId);
        }

        public BankParameter UpdateBankParameter(BankParameter bankParameter)
        {
            return bankParameterRepository.UpdateBankParameter(bankParameter);
        }
    }
}
