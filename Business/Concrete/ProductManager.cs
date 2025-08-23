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

        public Task<int> TotalCount(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize)
        {
           return _unitOfWork.Product.TotalCount(Filtre,PropertySet, CategoryId, CurrentPage, PageSize);
        }
    }
}
