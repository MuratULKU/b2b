using Business.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VirtualPosManager : IVirtualPosService
    {
        private readonly IVirtualPosRepository _virtualPosRepository;
        public VirtualPosManager(IVirtualPosRepository virtualPosRepository)
        {
            _virtualPosRepository = virtualPosRepository;
        }

    
       
        public VirtualPos CreateVirtualPos(VirtualPos virtualPos)
        {
            virtualPos.Id = Guid.NewGuid();
            _virtualPosRepository.Create(virtualPos);
            return virtualPos;
        }

        public VirtualPos DeleteVirtualPos(VirtualPos virtualPos)
        {
            throw new NotImplementedException();
        }

        public async Task<VirtualPos> GetByBankCode(int id, bool isBusiness)
        {
            return _virtualPosRepository.GetByBankCode(id, isBusiness);
        }

      
        public async Task<List<VirtualPos>> GetByBrandCode(int brandCode)
        {
            return _virtualPosRepository.GetByBrandCode(brandCode);
        }

        public async Task<List<VirtualPos>> GetVirtualPosAsync()
        {
            return _virtualPosRepository.GetAll();
        }

        public async Task<VirtualPos> GetVirtualPosAsync(Guid virtualPosId)
        {
            return _virtualPosRepository.Get(virtualPosId);
        }

        public VirtualPos UpdateVirtualPos(VirtualPos virtualPos)
        {
            return _virtualPosRepository.Update(virtualPos);
        }

      
    }
}
