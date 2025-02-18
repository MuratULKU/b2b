using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IVirtualPosService
    {
        Task<IResult> CreateVirtualPos(VirtualPos virtualPos);
        Task<IResult> DeleteVirtualPos(VirtualPos virtualPos);
        Task<IResult> UpdateVirtualPos(VirtualPos virtualPos);
        Task<VirtualPos> GetByBrandId(Guid id);
        Task<VirtualPos> GetByBankId(Guid id);
        Task<VirtualPos> GetVirtualPosAsync(Guid virtualPosId);
        Task<IDataResult<List<VirtualPosParameter>>> GetVirtualPosParameters(Guid bankId);
        Task<List<VirtualPos>> GetVirtualListsAsync();
    }
}
