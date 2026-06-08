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
    public class BankCardManager : IBankCardService
    {
        private readonly IUnitofWork _unitOfWork;

        public BankCardManager(IUnitofWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IResult> CreateBank(BankCard bankCard)
        {
            await _unitOfWork.Repository<BankCard>().AddAsync(bankCard);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }

        public async Task<IResult> DeleteBank(BankCard bankCard)
        {
            await _unitOfWork.Repository<BankCard>().Delete(bankCard);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt Silindi");
            return new Result(ResultStatus.Error, "Kayıt Silinemedi");
        }

        public async Task<List<BankCard>> GetAllBank()
        {
            return await _unitOfWork.Repository<BankCard>().GetAllAsync();
            
        }

        public async Task<IDataResult<BankCard>> GetBank(Guid id)
        {
            var result = await _unitOfWork.Repository<BankCard>().SingleOrDefaultAsync(x => x.Id == id);
            if(result != null)
                return new DataResult<BankCard>(ResultStatus.Success, result);
            return new DataResult<BankCard>(ResultStatus.Error,result!);
        }

        public async Task<BankCard> GetBankbyCode(int Code)
        {
           return await _unitOfWork.Repository<BankCard>().SingleOrDefaultAsync(x => x.BankCode == Code);
            
        }

        public async Task UpdateBank(BankCard bankCard)
        {
            await _unitOfWork.Repository<BankCard>().UpdateAsync(bankCard);
        }
    }
}
