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

        public async Task<IResult> Delete(Category category)
        {
            await _unitOfWork.Category.Delete(category);
            var result =await  _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt Silindi");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }

        public async Task DeleteAll()
        {
           var list = await _unitOfWork.Category.GetAllAsync();
           List<Category> allcategory = list;
            foreach (var category in allcategory)
            {
               await _unitOfWork.Category.Delete(category); 
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<Category>> Get(int parentref)
        {
           return await _unitOfWork.Category.Find(x => x.Parent == parentref);
         
        }

        public async Task<Category> Get(Guid id)
        {
           return await _unitOfWork.Category.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category> GetByCode(string code)
        {
         return await _unitOfWork.Category.SingleOrDefaultAsync(x => x.Code == code);
        
        }

        public async Task<Category> GetByRefNo(int refno)
        {
           return await _unitOfWork.Category.SingleOrDefaultAsync(x=>x.LogicalRef == refno);  
        }

        public async Task<IResult> Insert(Category category)
        {
            await _unitOfWork.Category.AddAsync(category);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }

        public async Task<int> Refno()
        {
         return _unitOfWork.Category.MaxRefNo() + 1;
        }

        public async Task<IResult> Update(Category category)
        {
             await _unitOfWork.Category.UpdateAsync(category);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Kayıt İşlemi Hatalı");
        }
    }
}
