using Business.Abstract;
using Core.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FirmParamManager : IFirmParamService
    {
        private readonly IUnitofWork _unitOfWork;

        public FirmParamManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IResult> Create(FirmParam firmParam)
        {
            return _unitOfWork.FirmParam.AddAsync(firmParam);
        }

        public Task<IResult> Delete(FirmParam firmParam)
        {
            return _unitOfWork.FirmParam.Delete(firmParam);
        }

        public async  Task<FirmParam> Get(int no)
        {
           var result = await _unitOfWork.FirmParam.SingleOrDefaultAsync(x=>x.No == no);
            return result.Data;
        }

        public async Task<List<FirmParam>> GetAll()
        {
            var result = await _unitOfWork.FirmParam.GetAllAsync();
            return result.Data;
        }

        public async Task<IResult> Update(FirmParam firmParam)
        {
            var result = await  _unitOfWork.FirmParam.UpdateAsync(firmParam);
            var r = await _unitOfWork.CommitAsync();
            if (r == 1)
                return result;
            else
                return result;
        }


        public string ToString(int no)
        {
            var result = Encoding.UTF8.GetString((byte[])(Get(no)).Result.Value);
            return result;
        }
        public bool ToBoolean(int no)
        {
            var firmParam = Get(no);
            if (firmParam != null)
            {
                if(ToString(no) == "1")
                    return true;
                return false;
           
            }
            throw new InvalidOperationException("Value cannot be converted to boolean.");
        }

        public int ToInteger(int no)
        {
            return Convert.ToInt32(ToString(no));
        }

    }
}
