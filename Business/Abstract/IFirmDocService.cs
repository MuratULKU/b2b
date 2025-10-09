using Core.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFirmDocService
    {
        Task<IResult> Save(FirmDoc doc);
        void Update(FirmDoc doc);
        void Delete(FirmDoc doc);
        Task<int> DeleteAll();
        Task<List<FirmDoc>> GetAll(Guid ByProductId);   

    }
}

