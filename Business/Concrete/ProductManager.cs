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
    public class ProductManager : IProductServices
    {
        private readonly IProductRepository produtctRepository;

        public ProductManager(IProductRepository produtctRepository)
        {
            this.produtctRepository = produtctRepository;
        }

        public List<Product> GetAll()
        {
           return produtctRepository.GetAll();
        }

        public List<Product> GetAll(int currentPage, int pageSize)
        {
            return produtctRepository.GetAll(currentPage, pageSize);
        }

        public async Task<List<Product>> GetAllAsync(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize)
        {
            return await produtctRepository.GetAllAsync(Filtre, PropertySet, CategoryId, CurrentPage, PageSize);
        }

        public async Task<Product> GetByCode(string Code)
        {
            var result = await produtctRepository.GetByCode(Code);
            return result;
        }

        public async Task<Product> GetByGuid(Guid id)
        {
            var result = await produtctRepository.GetByGuid(id);
            return result;
        }

        public async Task<Product> GetByLogicalref(int Logicalref)
        {
            var result = await produtctRepository.GetByLogicalref(Logicalref);
            return result;
        }

        public Task<int> TotalCount(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize)
        {
           return produtctRepository.TotalCount(Filtre,PropertySet, CategoryId, CurrentPage, PageSize);
        }
    }
}
