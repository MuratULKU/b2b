using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;

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
