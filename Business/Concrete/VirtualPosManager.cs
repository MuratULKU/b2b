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

        public Task<List<VirtualPos>> GetVirtualPosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VirtualPos> GetVirtualPosAsync(Guid virtualPosId)
        {
            throw new NotImplementedException();
        }

        public VirtualPos UpdateVirtualPos(VirtualPos virtualPos)
        {
            throw new NotImplementedException();
        }
    }
}
