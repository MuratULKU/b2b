using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VatListManager:IVatListService
    {
        private readonly IUnitofWork _unitOfWork;

        public VatListManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(VatList vatList)
        {
          await _unitOfWork.Repository<VatList>().AddAsync(vatList);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }

        public async Task<VatList> GetbyVat(byte VatNo)
        {
           return await _unitOfWork.Repository<VatList>().SingleOrDefaultAsync(x=>x.VatNo == VatNo);    
        }

        public async Task<double> GetbyVatNo(byte VatNo)
        {
           var result = await _unitOfWork.Repository<VatList>().SingleOrDefaultAsync(x => x.VatNo == VatNo);
            return result.VatPer;
        }

        public async Task<double []> GetVat()
        {
            var result = await _unitOfWork.Repository<VatList>().GetAllAsync();
            return result.Select(x=>x.VatPer).ToArray();
        }

        public async Task<IResult> Update(VatList vatList)
        {
           await _unitOfWork.Repository<VatList>().UpdateAsync(vatList);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }
    }
}
