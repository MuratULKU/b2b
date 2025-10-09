using Business.Abstract;
using Business.Validaton;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
       
        private readonly IUnitofWork _unitOfWork;

        public ProductManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Product>> GetAll()
        {
           return await _unitOfWork.Product.GetAllAsync();
           
        }

       

        public async Task<List<Product>> GetAllAsync(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize)
        {
            return await _unitOfWork.Product.GetAllAsync(Filtre, PropertySet, CategoryId, CurrentPage, PageSize);
        }

        public async Task<Product> GetByCode(string Code)
        {
            var result = await _unitOfWork.Product.GetByCode(Code);
            return result;
        }

        public async Task<Product> GetByGuid(Guid id)
        {
            var result = await _unitOfWork.Product.GetByGuid(id);
            return result;
        }

        public async Task<Product> GetByLogicalref(int Logicalref)
        {
            var result = await _unitOfWork.Product.GetByLogicalref(Logicalref);
            return result;
        }

        public async Task<IResult> Insert(Product product)
        {
          var result = await _unitOfWork.Product.AddAsync(product);
          await _unitOfWork.CommitAsync();
          return new Result(ResultStatus.Success, "Product inserted successfully.");
          

        }

        public async Task<IResult> Save(Product product)
        {
            var validator = new ProductValidator();
            var validationResult = validator.Validate(product);

            if (!validationResult.IsValid)
            {
                return new ErrorResult("Doğrulama Hatası: " + string.Join(", ", validationResult.Errors));
            }

            if (product.Id == Guid.Empty)
            {
                await _unitOfWork.Product.AddAsync(product);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Kayıt Başarılı");
            }else
            {
                await _unitOfWork.Product.UpdateAsync(product);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Günceleme Başarılı");
            }

        }

        public Task<int> TotalCount(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize)
        {
           return _unitOfWork.Product.TotalCount(Filtre,PropertySet, CategoryId, CurrentPage, PageSize);
        }
    }
}
