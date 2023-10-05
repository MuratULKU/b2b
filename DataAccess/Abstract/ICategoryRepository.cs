using Entity;

namespace DataAccess.Abstract
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        List<Category> Get(int parentref);
        void Insert(Category category);
        void Update(Category category);

        Task<Category> GetByCode(string code);
    }
}
