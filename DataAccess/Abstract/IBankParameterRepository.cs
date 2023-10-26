using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBankParameterRepository
    {
        List<BankParameter> GetAll(Guid BankId);
        BankParameter CreateBankParameter(BankParameter bankParameter);
        BankParameter UpdateBankParameter(BankParameter bankParameter);
        BankParameter DeleteBankParameter(BankParameter bankParameter);
    }
}
