using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RepositoryContext _dbContext;

        public CategoryRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Category> Get(int parentref)
        {
            return _dbContext.Categories.Where(x=>x.Parent == parentref).ToList();
        }

        public List<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public async Task<Category> GetByCode(string code)
        {
            return _dbContext.Categories.FirstOrDefault(x => x.Code == code);
        }

        public async Task<int> DeleteAll()
        {
            _dbContext.Categories.ExecuteDelete();
            return await _dbContext.SaveChangesAsync();
        }
        public void Insert(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
        }
    }
}
