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
    public class CategoryManager:ICategoryService
    {
        private readonly IUnitofWork _unitOfWork;

        public CategoryManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAll()
        {
           var list = await _unitOfWork.Category.GetAllAsync();
           List<Category> allcategory = list.Data;
            foreach (var category in allcategory)
            {
               await _unitOfWork.Category.Delete(category); 
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<Category>> Get(int parentref)
        {
           var result = await _unitOfWork.Category.Find(x => x.Parent == parentref);
            return result.Data;
        }

        public async Task<Category> GetByCode(string code)
        {
          var result = await _unitOfWork.Category.SingleOrDefaultAsync(x => x.Code == code);
            return result.Data;
        }

        public async Task<IResult> Insert(Category category)
        {
            var result = await _unitOfWork.Category.AddAsync(category);
            await _unitOfWork.CommitAsync();
            if (result.Status == ResultStatus.Success)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }

        public async Task<IResult> Update(Category category)
        {
            var result = await _unitOfWork.Category.UpdateAsync(category);
            await _unitOfWork.CommitAsync();
            if (result.Status == ResultStatus.Success)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }
    }
}
