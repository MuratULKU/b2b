using DataAccess.Abstract;
using Business.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstract;
using Core.Concrete;

namespace Business.Concrete
{
    public class VirtualPosParameterManager : IVirtualPosParameterService
    {
        private readonly IUnitofWork _unitOfWork;

        public VirtualPosParameterManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> CreateBankParameter(VirtualPosParameter bankParameter)
        {
            var result = await _unitOfWork.VirtualPosParameter.AddAsync(bankParameter);
            await _unitOfWork.CommitAsync();
           return result;
        }

        public async Task<IResult> DeleteBankParameter(VirtualPosParameter bankParameter)
        {
            var result = await _unitOfWork.VirtualPosParameter.Delete(bankParameter);
            await _unitOfWork.CommitAsync();
            return result;
        }

        public async Task<List<VirtualPosParameter>> GetAll(Guid BankId)
        {
            var result = await _unitOfWork.VirtualPosParameter.GetAllAsync();
            return result.Data;
        }

        public async Task<IResult> UpdateBankParameter(VirtualPosParameter bankParameter)
        {
            var result = await _unitOfWork.VirtualPosParameter.UpdateAsync(bankParameter);
            await _unitOfWork.CommitAsync();
          return result;
        }
    }
}
