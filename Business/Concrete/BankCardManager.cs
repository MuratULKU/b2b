using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BankCardManager:IBankCardService
    {
        private readonly IUnitofWork _unitOfWork;

        public BankCardManager(IUnitofWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IResult> CreateBank(BankCard bankCard)
        {
           var result = await _unitOfWork.BankCards.AddAsync(bankCard);
            if (result.Status == ResultStatus.Success)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }

        public async Task<IResult> DeleteBank(BankCard bankCard)
        {
            return await _unitOfWork.BankCards.Delete(bankCard);
        }

        public async Task<List<BankCard>> GetAllBank()
        {
            var result = await _unitOfWork.BankCards.GetAllAsync();
            return result.Data;
        }

        public async Task<IDataResult<BankCard>> GetBank(Guid id)
        {
            return await _unitOfWork.BankCards.GetByIdAsync(id);
        }

        public async Task<BankCard> GetBankbyCode(int Code)
        {
            var result = await _unitOfWork.BankCards.SingleOrDefaultAsync(x=>x.BankCode == Code);
            return result.Data;
        }

        public async Task UpdateBank(BankCard bankCard)
        {
           await _unitOfWork.BankCards.UpdateAsync(bankCard);
        }
    }
}
