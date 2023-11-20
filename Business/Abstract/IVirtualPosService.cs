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
        VirtualPos CreateVirtualPos(VirtualPos virtualPos);
        VirtualPos DeleteVirtualPos(VirtualPos virtualPos);
        VirtualPos UpdateVirtualPos(VirtualPos virtualPos);
        Task<List<VirtualPos>> GetVirtualPosAsync();
        Task<VirtualPos> GetByBankCode(int id, bool isBusiness);
        Task<VirtualPos> GetVirtualPosAsync(Guid virtualPosId);
       Task<List<VirtualPos>> GetByBrandCode(int brandCode);
    }
}
