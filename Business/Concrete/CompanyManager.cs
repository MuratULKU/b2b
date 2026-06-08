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

        public CompanyManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Company> Get(Guid id)
        {
            return await _unitOfWork.Repository<Company>().SingleOrDefaultAsync(x => x.Id == id);

        }

       
        public async Task<List<Company>> GetAllAsync(int CurrentPage, int PageSize)
        {
            return await _unitOfWork.Repository<Company>().GetPagedAsync(CurrentPage, PageSize, order: x => x.Id);

        }

        public async Task<Company> GetByUserId(Guid userId)
        {
            var user = await _unitOfWork.Repository<User>().SingleOrDefaultAsync(x => x.Id == userId);
            var company = await _unitOfWork.Repository<Company>().SingleOrDefaultAsync(x => x.Id == user.CompanyId);
            return company;

        }

        public async Task<bool> Insert(Company company)
        {
            await _unitOfWork.Repository<Company>().AddAsync(company);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Insert(Company company, User user, UserRole userRole)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.Repository<Company>().AddAsync(company);
                await _unitOfWork.Repository<User>().AddAsync(user);
                await _unitOfWork.Repository<UserRole>().AddAsync(userRole);

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

        public async Task<int> TotalCount()
        {
            return await _unitOfWork.Repository<Company>().RowCount();
        }

        public async Task<bool> Update(Company company)
        {
            await _unitOfWork.Repository<Company>().UpdateAsync(company);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 1) { return true; }
            return false;
        }

        public async Task<bool> Delete(Company company)
        {
            await _unitOfWork.Repository<Company>().Delete(company);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result == 1) { return true; }
            return false;
        }
    }
}
