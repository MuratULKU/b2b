using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DataAccess.Concrete
{
    public class FirmParamRepository : IFirmParamRepository
    {
        private readonly RepositoryContext _dbContext;
        private FirmParam _firmParam;
        public FirmParamRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FirmParam Create(FirmParam firmParam)
        {
            var result = _dbContext.FirmParams.Add(firmParam).Entity;
            _dbContext.SaveChanges();
            return result;
        }

        public FirmParam Delete(FirmParam firmParam)
        {
            var result = _dbContext.FirmParams.Remove(firmParam).Entity;
            _dbContext.SaveChanges();
            return result;
        }

        public object Get(int no)
        {
            return _dbContext.FirmParams.FirstOrDefaultAsync(x => x.No == no).Result.Value;
            
        }

        public List<FirmParam> GetAll()
        {
           return _dbContext.FirmParams.ToList();
        }

        public FirmParam Update(FirmParam firmParam)
        {
            var result = _dbContext.FirmParams.Update(firmParam).Entity;
            _dbContext.SaveChanges();
            return result;
        }

        public void Update(int No, object Value)
        {
            var firmparam = _dbContext.FirmParams.FirstOrDefault(x => x.No == No);
            if(firmparam != null)
            {
                firmparam.Value = ASCIIEncoding.ASCII.GetBytes(Value.ToString());
                _dbContext.SaveChanges();
            }
        }

    }
}
