using Business.Abstract;
using Core.Abstract;
using Core.Logger;
using DataAccess.Abstract;
using Entity;


namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly ILoggerService _logger;

        public CompanyManager(IUnitofWork unitOfWork, ILoggerService logger = null)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Company> Get(Guid id)
        {
          var result = await _unitOfWork.Company.GetByIdAsync(id);
            if (result.Status == ResultStatus.Success)
                return result.Data;
            else
                return null;
        }

        public List<Company> GetAll(int currentPage, int pageSize)
        {
           return GetAllAsync(currentPage, pageSize).Result;
        }

        public async Task<List<Company>> GetAllAsync(int CurrentPage, int PageSize)
        {
           var result = await _unitOfWork.Company.GetPagedCompanies(CurrentPage, PageSize);
            return result;
        }

        public async Task<Company> GetByUserId(Guid userId)
        {
            var user = await _unitOfWork.User.GetByIdAsync(userId);
            var result = user.Data;
            var company = await _unitOfWork.Company.SingleOrDefaultAsync(x => x.Id == result.CompanyId);
            return company.Data;
            
        }

        public async Task<bool> Insert(Company company)
        {
           var result = await _unitOfWork.Company.AddAsync(company);
            if (result.Status == ResultStatus.Success)
            {
                await _unitOfWork.CommitAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Insert(Company company, User user, UserRole userRole)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var companyResult = await _unitOfWork.Company.AddAsync(company);
                if (companyResult.Status != ResultStatus.Success)
                    throw new Exception("There was an error adding the company");
                var userResult = await _unitOfWork.User.AddAsync(user);
                if (userResult.Status != ResultStatus.Success)
                    throw new Exception("There was an error adding the user");
                var userRoleResult = await _unitOfWork.UserRole.AddAsync(userRole);
                if (userRoleResult.Status != ResultStatus.Success)
                    throw new Exception("There wan an error adding the UserRole");
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                _logger.Error(ex);
                throw;
            }
          
            
        }

        public async Task<int> TotalCount(int CurrentPage, int PageSize)
        {
            return await _unitOfWork.Company.RowCount();
        }

        public async Task<bool> Update(Company company)
        {
             await _unitOfWork.Company.UpdateAsync(company);
            var result = await _unitOfWork.CommitAsync();
            if(result == 1)   { return true; }
            return false;
        }
    }
}
