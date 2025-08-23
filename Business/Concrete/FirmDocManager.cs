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

        public void Insert(FirmDoc doc)
        {
           _unitOfWork.FirmDoc.AddAsync(doc);
            _unitOfWork.CommitAsync();
        }

        public void Update(FirmDoc doc)
        {
            _unitOfWork.FirmDoc.UpdateAsync(doc);
            _unitOfWork.CommitAsync();
        }
    }
}
