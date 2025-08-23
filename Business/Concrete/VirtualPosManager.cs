using Business.Abstract;
using Business.Validaton;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VirtualPosManager : IVirtualPosService
    {
        private readonly IUnitofWork _unitOfWork;

        public VirtualPosManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> CreateVirtualPos(VirtualPos virtualPos)
        {

            var valid = virtualPos.Validation();
            if (valid.Count == 0)
            {
                try
                {
                    await _unitOfWork.VirtualPoses.AddAsync(virtualPos);
                    var result = await _unitOfWork.CommitAsync();
                    if (result == 1)
                        return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
                    return new Result(ResultStatus.Error, "Hatalı İşlem");
                }
                catch (Exception ex)
                {
                    return new Result(ResultStatus.Error, ex.Message);
                }
            }
            else
            {
                string msg = "";
                foreach (var item in valid)
                {
                    msg += item.ToString();
                }

                return new Result(ResultStatus.Error, msg);

            }

        }

        public async Task<IResult> DeleteVirtualPos(VirtualPos virtualPos)
        {
            await _unitOfWork.VirtualPoses.Delete(virtualPos);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Tamamlandı");
            return new Result(ResultStatus.Error, "Hatalı İşlem");

        }

        public async Task<VirtualPos> GetByBrandId(Guid id)
        {
          return await _unitOfWork.VirtualPoses.FirstOrDefaultAsync(x => x.CardBrandId == id && x.Active == false, x => x.Include(x=>x.BankCard));
           
        }
        public async Task<VirtualPos> GetByBankId(Guid id)
        {
            return await _unitOfWork.VirtualPoses.SingleOrDefaultAsync(x => x.BankCardId == id && x.Active == false, x => x.Include(x => x.BankCard));
        }

        public async Task<List<VirtualPos>> GetVirtualListsAsync()
        {
           var result = await _unitOfWork.VirtualPoses.GetAllAsync();
            if (result == null)
                return null;
            return result;
        }

        public async Task<VirtualPos> GetVirtualPosAsync(Guid virtualPosId)
        {
            var result = await _unitOfWork.VirtualPoses.SingleOrDefaultAsync(x => x.Id == virtualPosId, x=>x.Include(x=>x.BankCard).Include(y=>y.VirtualPosParameters));
            if (result == null)
                return null;
            return result;

        }

        public async Task<IDataResult<List<VirtualPosParameter>>> GetVirtualPosParameters(Guid bankId)
        {
            var result = await _unitOfWork.VirtualPosParameter.Find(x => x.VirtualPosId == bankId);
            if (result == null)
                return null;
            return new DataResult<List<VirtualPosParameter>>(ResultStatus.Success, result);  
        }

        public async Task<IResult> UpdateVirtualPos(VirtualPos virtualPos)
        {
            await _unitOfWork.VirtualPoses.UpdateAsync(virtualPos);
            var result =await  _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt Tamamlandı");
            return new Result(ResultStatus.Error, "Hatalı Kayıt");
        }
    }
}
