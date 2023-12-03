using DataAccess.Abstract;
using DataAccess.EFCore;
using DataAccess.Helpers.Product;
using Entity;
using Microsoft.EntityFrameworkCore;



namespace DataAccess.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly RepositoryContext _dbContext;

        public ProductRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAll()
        {
            return _dbContext.Products
                .Include(x => x.PriceLists)
                .Include(x => x.ProductAmounts)
                .ToList();
        }

        public List<Product> GetAll(int currentPage, int pageSize)
        {
            return _dbContext.Products
                .Include(x => x.PriceLists)
                .Include(x => x.ProductAmounts)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Product> GetAll(string Filtre, int CategoryId,int CurrentPage, int PageSize)
        {
            Filtre = Filtre ?? "*";
            return
                CategoryId == 0
                 ? _dbContext.Products
                 .Include(x => x.PriceLists)
                 .Include(x => x.ProductAmounts)
                 .Include(x => x.firmDocs)
                 .Where(x => EF.Functions.Like(x.Name, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.Name, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.Code, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.Code, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.ProducerCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.ProducerCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.StGrupCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.StGrupCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode2, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode2, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode3, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode3, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode4, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode4, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode5, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode5, $"%{Filtre.ToUpper()}%")
                             )
                 .Skip((CurrentPage - 1) * PageSize)
                 .Take(PageSize)
                 .ToList()
                : _dbContext.Products
                 .Include(x => x.PriceLists)
                 .Include(x => x.ProductAmounts)
                 .Include(x => x.firmDocs)
                 .Where(x => (x.ParentRef == CategoryId) && (EF.Functions.Like(x.Name, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.Name, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.Code, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.Code, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.ProducerCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.ProducerCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.StGrupCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.StGrupCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode2, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode2, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode3, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode3, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode4, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode4, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode5, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode5, $"%{Filtre.ToUpper()}%"))
                             )
                 .Skip((CurrentPage - 1) * PageSize)
                 .Take(PageSize)
                 .ToList();
        }

        public async Task<Product> GetByCode(string code)
        {
            return await _dbContext.Products
                .Include(x => x.firmDocs)
                .Include(x => x.PriceLists)
                .FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<Product> GetByLogicalref(int logicalref)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.LogicalRef == logicalref);
        }

        public void Insert(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }
        public async Task<int> DeleteAll()
        {
            _dbContext.Products.ExecuteDelete();
            return await _dbContext.SaveChangesAsync();
        }
        public int TotalCount(string Filtre, int CategoryId, int CurrentPage, int PageSize)
        {
            try
            {
                Filtre = Filtre ?? string.Empty;
                return
                CategoryId == 0
                 ? _dbContext.Products
                 .Where(x => EF.Functions.Like(x.Name, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.Name, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.Code, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.Code, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.ProducerCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.ProducerCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.StGrupCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.StGrupCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode2, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode2, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode3, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode3, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode4, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode4, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode5, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode5, $"%{Filtre.ToUpper()}%")
                             )
                 .Count()
                : _dbContext.Products
                 .Where(x => (x.ParentRef == CategoryId) && (EF.Functions.Like(x.Name, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.Name, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.Code, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.Code, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.ProducerCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.ProducerCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.StGrupCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.StGrupCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode2, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode2, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode3, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode3, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode4, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode4, $"%{Filtre.ToUpper()}%") |
                             EF.Functions.Like(x.SpeCode5, $"%{Filtre.ToLower()}%") |
                             EF.Functions.Like(x.SpeCode5, $"%{Filtre.ToUpper()}%"))
                             )
                 .Count();

            }
            catch (Exception)
            {

                return 1;
            }
            
            
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();
        }
    }
}
