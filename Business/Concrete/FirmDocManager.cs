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
    public class FirmDocManager : IFirmDocService
    {
        private readonly IUnitofWork _unitOfWork;

        public FirmDocManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(FirmDoc doc)
        {
            _unitOfWork.FirmDoc.Delete(doc);
        }

        public Task<int> DeleteAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<FirmDoc>> GetAll(Guid ByProductId)
        {
           return  _unitOfWork.FirmDoc.Find(x=>x.ProtuctId == ByProductId);
        }

        public async Task<IResult> Save(FirmDoc doc)
        {
            if (doc.Id == Guid.Empty)
            {
               await _unitOfWork.FirmDoc.AddAsync(doc);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
            else
            {
                await _unitOfWork.FirmDoc.UpdateAsync(doc);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
          
           
        }

        public void Update(FirmDoc doc)
        {
            _unitOfWork.FirmDoc.UpdateAsync(doc);
            _unitOfWork.CommitAsync();
        }
    }
}
