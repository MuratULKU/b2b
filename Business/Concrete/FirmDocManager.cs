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
            _unitOfWork.Repository<FirmDoc>().Delete(doc);
        }

        public Task<int> DeleteAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<FirmDoc>> GetAll(Guid ByProductId)
        {
           return  _unitOfWork.Repository<FirmDoc>().Find(x=>x.ProductId == ByProductId);
        }

        public async Task<IResult> Save(FirmDoc doc)
        {
            if (doc.Id == Guid.Empty)
            {
               await _unitOfWork.Repository<FirmDoc>().AddAsync(doc);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
            else
            {
                await _unitOfWork.Repository<FirmDoc>().UpdateAsync(doc);
                await _unitOfWork.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }
          
           
        }

        public void Update(FirmDoc doc)
        {
            _unitOfWork.Repository<FirmDoc>().UpdateAsync(doc);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
