using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;



namespace DataAccess.Concrete
{
    public class ProductRepository :Repository<Product>, IProductRepository
    {
        
        public ProductRepository(RepositoryContext context) : base(context)
        {

        }

        public List<Product> GetAll()
        {
            return dbContext.Set<Product>()
                .Include(x => x.PriceLists)
                .Include(x => x.ProductAmounts)
                .OrderBy(x => x.Code)
                .ToList();
        }

        public List<Product> GetAll(int currentPage, int pageSize)
        {
            return dbContext.Set<Product>()
                .Include(x => x.PriceLists)
                .ThenInclude(y => y.Currency)
                .Include(x => x.ProductAmounts)
                .OrderBy(x => x.Code)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        private string WhereSentence(string text)
        {
            var filter = text.Split(' ');
            string whereSentence = "where active = 0";
            for (var i = 0; i < filter.Length; i++)
            {
                if (filter[i] != "")
                {

                    if (whereSentence == "")
                        whereSentence = "where ";
                    else whereSentence += " and ";
                    whereSentence += $"(((instr(Name, '{filter[i].ToUpper()}') > 0 or instr(Name, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(Code, '{filter[i].ToUpper()}') > 0 or instr(Code, '{filter[i].ToLower()}') > 0 )) or ";
                    if (filter[i] != filter[i].Replace("s", "ş"))
                    {
                        whereSentence += $"((instr(Code, '{filter[i].Replace("s", "ş").ToUpper()}') > 0 or instr(Code, '{filter[i].Replace("s", "ş").ToLower()}') > 0 )) or ";
                        whereSentence += $"((instr(Name, '{filter[i].Replace("s", "ş").ToUpper()}') > 0 or instr(Name, '{filter[i].Replace("s", "ş").ToLower()}') > 0 )) or ";
                    }
                    if (filter[i] != filter[i].Replace("i", "I"))
                    {
                        whereSentence += $"((instr(Code, '{filter[i].Replace("i", "I").ToUpper()}') > 0 or instr(Code, '{filter[i].Replace("i", "ı").ToLower()}') > 0 )) or ";
                        whereSentence += $"((instr(Name, '{filter[i].Replace("i", "I").ToUpper()}') > 0 or instr(Name, '{filter[i].Replace("i", "ı").ToLower()}') > 0 )) or ";
                    }
                    if (filter[i] != filter[i].Replace("c", "ç"))
                    {
                        whereSentence += $"((instr(Code, '{filter[i].Replace("c", "ç").ToUpper()}') > 0 or instr(Code, '{filter[i].Replace("c", "ç").ToLower()}') > 0 )) or ";
                        whereSentence += $"((instr(Name, '{filter[i].Replace("c", "ç").ToUpper()}') > 0 or instr(Name, '{filter[i].Replace("c", "ç").ToLower()}') > 0 )) or ";
                    }
                    if (filter[i] != filter[i].Replace("g", "ğ"))
                    {
                        whereSentence += $"((instr(Code, '{filter[i].Replace("g", "ğ").ToUpper()}') > 0 or instr(Code, '{filter[i].Replace("g", "ğ").ToLower()}') > 0 )) or ";
                        whereSentence += $"((instr(Name, '{filter[i].Replace("g", "ğ").ToUpper()}') > 0 or instr(Name, '{filter[i].Replace("g", "ğ").ToLower()}') > 0 )) or ";
                    }
                    whereSentence += $"((instr(StGrupCode, '{filter[i].ToUpper()}') > 0 or instr(StGrupCode, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(SpeCode, '{filter[i].ToUpper()}') > 0 or instr(SpeCode, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(SpeCode2, '{filter[i].ToUpper()}') > 0 or instr(SpeCode2, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(SpeCode3, '{filter[i].ToUpper()}') > 0 or instr(SpeCode3, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(SpeCode4, '{filter[i].ToUpper()}') > 0 or instr(SpeCode4, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(SpeCode5, '{filter[i].ToUpper()}') > 0 or instr(SpeCode5, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(Keyword1, '{filter[i].ToUpper()}') > 0 or instr(Keyword1, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(Keyword2, '{filter[i].ToUpper()}') > 0 or instr(Keyword2, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(Keyword3, '{filter[i].ToUpper()}') > 0 or instr(Keyword3, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(Keyword4, '{filter[i].ToUpper()}') > 0 or instr(Keyword4, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(Keyword5, '{filter[i].ToUpper()}') > 0 or instr(Keyword5, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(Name2, '{filter[i].ToUpper()}') > 0 or instr(Name2, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(Name3, '{filter[i].ToUpper()}') > 0 or instr(Name3, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(Name4, '{filter[i].ToUpper()}') > 0 or instr(Name4, '{filter[i].ToLower()}') > 0 )) or ";
                    whereSentence += $"((instr(ProducerCode, '{filter[i].ToUpper()}') > 0 or instr(ProducerCode, '{filter[i].ToLower()}') > 0 ))) ";
                    
                    if (i != filter.Length - 1)
                        whereSentence += " and ";
                }
            }
            return whereSentence;
        }

        private bool isParent(int CategoryId)
        {
            var result = dbContext.Set<Category>().Count(x=>x.Parent == CategoryId);
            if(result > 1)
                return true;
            return false;
        }

        private string ParentRef(int CategoryId)
        {
            int result = dbContext.Set<Category>().Where(x => x.LogicalRef == CategoryId).Select(x=>x.Parent).FirstOrDefault();
          return result.ToString();
        }

        public Task<List<Product>> GetAllAsync(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize)
        {
            //gelen id nin içerisinde parentler var ise parent refe göre sorgula

            var where = WhereSentence(Filtre);
            string category = "";
            if (isParent(CategoryId))
            {
                var parent = dbContext.Set<Category>().Where(x => x.Parent == CategoryId).Select(x => x.LogicalRef).ToArray();
                category = string.Join(",", parent);
                category += "," + ParentRef(CategoryId);
            }
            else
            {
                category = CategoryId.ToString();
            }

            if (CategoryId != 0)
            {
                if (where == null || where == string.Empty)
                {
                    where = "where ParentRef in (" + category + ")";
                }
                else
                {
                    where += " and ParentRef in (" + category + ")";
                }
            }
          
           
            if (PropertySet != null && PropertySet.Values.Count >= 1)
            {
                where += " and Id in (select ProductId from CharAsgns WHERE (";
                for (int p = 0; p < PropertySet.Count; p++)
                {
                    List<string> property = PropertySet.ElementAt(p).Value;
                    for (int i = 0; i < property.Count; i++)
                    {
                        where += $"CharValCode = '{property.ElementAt(i)}'";
                        if (i < property.Count - 1)
                        {
                            where += " or ";
                        }
                    }
                    if (p < PropertySet.Count - 1)
                    {
                        where += " or ";
                    }

                }

                where += $")GROUP by ProductId having count(ProductId)>={PropertySet.Count})";
            }

            var queryText = $"select * from Products {where} order by Code LIMIT {PageSize} OFFSET {(CurrentPage - 1) * PageSize}";


             return Task.FromResult(dbContext.Set<Product>()
                .FromSqlRaw(queryText)
                .Include(x => x.PriceLists)
                .ThenInclude(y => y.Currency)
                .Include(x => x.ProductAmounts)
                .Include(x => x.firmDocs)
                .Include(x => x.CharAsgn)
                .ToList());
        }

     

        public Task<List<Product>> GetAll(string Filtre, int CategoryId, int CurrentPage, int PageSize)
        {
            Filtre = Filtre ?? "*";
            var result =

                CategoryId == 0
                 ? dbContext.Set<Product>()
                 .Include(x => x.PriceLists)
                 .ThenInclude(y => y.Currency)
                 .Include(x => x.ProductAmounts)
                 .Include(x => x.firmDocs)
                 .OrderBy(x => x.Code)
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
                : dbContext.Set<Product>()
                 .Include(x => x.PriceLists)
                 .ThenInclude(y => y.Currency)
                 .Include(x => x.ProductAmounts)
                 .Include(x => x.firmDocs)
                 .OrderBy(x => x.Code)
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
            return Task.FromResult(result);
        }

        public async Task<Product> GetByGuid( Guid id )
        {
            return await dbContext.Set<Product>().FirstOrDefaultAsync(x=>x.Id == id);
        }
        public async Task<Product> GetByCode(string code)
        {
            return await dbContext.Set<Product>()
                .Include(x => x.firmDocs)
                .Include(x => x.PriceLists)
                .ThenInclude(y => y.Currency)
                .Include(x => x.ProductAmounts)
                .FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<Product> GetByLogicalref(int logicalref)
        {
            return await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.LogicalRef == logicalref);
        }

        public void Insert(Product product)
        {
            dbContext.Set<Product>().Add(product);
            dbContext.SaveChanges();
        }

        public void InsertImage(FirmDoc firmDoc)
        {
            dbContext.Set<FirmDoc>().Add(firmDoc);
            dbContext.SaveChanges();
        }
        public async Task<int> DeleteAll()
        {
            dbContext.Set<Product>().ExecuteDelete();
            return await dbContext.SaveChangesAsync();
        }
        public Task<int> TotalCount(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize)
        {
            try
            {
                var where = WhereSentence(Filtre);
                string category = "";
                if (isParent(CategoryId))
                {
                    var parent = dbContext.Set<Category>().Where(x => x.Parent == CategoryId).Select(x => x.LogicalRef).ToArray();
                    category = string.Join(",", parent);
                    category += "," + ParentRef(CategoryId);
                }
                else
                {
                    category = CategoryId.ToString();
                }
                if (CategoryId != 0)
                {
                    if (where == null || where == string.Empty)
                    {
                        where = "where ParentRef in (" + category + ")";
                    }
                    else
                    {
                        where += " and ParentRef in (" + category + ")";
                    }
                }
                if (PropertySet != null && PropertySet.Values.Count >= 1)
                {
                    where += " and Id in (select ProductId from CharAsgns WHERE (";
                    for (int p = 0; p < PropertySet.Count; p++)
                    {
                        List<string> property = PropertySet.ElementAt(p).Value;
                        for (int i = 0; i < property.Count; i++)
                        {
                            where += $"CharValCode = '{property.ElementAt(i)}'";
                            if (i < property.Count - 1)
                            {
                                where += " or ";
                            }
                        }
                        if (p < PropertySet.Count - 1)
                        {
                            where += " or ";
                        }

                    }

                    where += $")GROUP by ProductId having count(ProductId)>={PropertySet.Count})";
                }
                var queryText = $"select * from Products {where}";

                return Task.FromResult(dbContext.Set<Product>()
                    .FromSqlRaw(queryText).Count());


            }
            catch (Exception ex)
            {

                return Task.FromResult(0);
            }


        }

        public void Update(Product product)
        {
            dbContext.Set<Product>().Update(product);
            dbContext.SaveChanges();
        }

        public void UpdateImage(FirmDoc firmdoc)
        {
            dbContext.Set<FirmDoc>().Update(firmdoc);
            dbContext.SaveChanges();
        }

        public Task<bool> Delete(Product product)
        {
            dbContext.Set<Product>().Remove(product);
            dbContext.SaveChanges();
            return Task.FromResult(true);
        }

       
    }

}
