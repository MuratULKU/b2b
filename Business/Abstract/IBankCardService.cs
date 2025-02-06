using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBankCardService
    {
        Task<List<BankCard>> GetAllBank();
        Task<IDataResult<BankCard>> GetBank(Guid id);
        Task<BankCard> GetBankbyCode(int Code);
        Task<IResult>CreateBank(BankCard bankCard);
        Task UpdateBank(BankCard bankCard);

        Task<IResult> DeleteBank(BankCard bankCard);
             
       
       
    }
}
