using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;

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

        public FirmParam firmParam()
        {
            _firmParam = _dbContext.FirmParams.FirstOrDefault(x => x.FirmId == 1);
            return _firmParam;


        }

        public void Update(FirmParam param)
        {
            _dbContext.FirmParams.Update(param);
            _dbContext.SaveChanges();
        }
    }
}
