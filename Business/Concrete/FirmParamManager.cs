using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
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

        public async Task<IResult> Create(FirmParam firmParam)
        {
            await _unitOfWork.FirmParam.AddAsync(firmParam);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }

        public async Task<IResult> Delete(FirmParam firmParam)
        {
            _unitOfWork.FirmParam.Delete(firmParam);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt Silindi");
            return new Result(ResultStatus.Error, "Kayıt Silinemedi");
        }

        public async  Task<FirmParam> Get(int no)
        {
           var result = await _unitOfWork.FirmParam.SingleOrDefaultAsync(x=>x.No == no);
            return result;
        }

        public async Task<List<FirmParam>> GetAll()
        {
            var result = await _unitOfWork.FirmParam.GetAllAsync();
            return result;
        }

        public async Task<IResult> Update(FirmParam firmParam)
        {
            await  _unitOfWork.FirmParam.UpdateAsync(firmParam);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt Güncellendi");
            return new Result(ResultStatus.Error, "Kayıt Güncellenmedi");
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
