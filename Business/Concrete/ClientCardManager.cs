using Business.Abstract;
using DataAccess.Abstract;
using Entity;


namespace Business.Concrete
{
    public class ClientCardManager : IClientCardService
    {
        private readonly IUnitofWork _unitOfWork;

        public ClientCardManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Client>> GetAll()
        {
            var result = await _unitOfWork.Client.GetAllAsync();
            if (result.Status == Core.Abstract.ResultStatus.Success)
            {
                return result.Data;
            }
            return null!;
        }

        public List<Client> GetAll(int currentPage, int pageSize)
        {
            return GetAll().Result;
        }

        public Task<List<Client>> GetAllAsync(string Filtre, int CategoryId, int CurrentPage, int PageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Client> GetByCode(string Code)
        {
            var result = await _unitOfWork.Client.SingleOrDefaultAsync(x => x.Code == Code);
            return result.Data;
        }

        public async Task<Client> GetByGuid(Guid id)
        {
            var result = await _unitOfWork.Client.SingleOrDefaultAsync(x => x.Id == id);
            return result.Data;
        }

        public async Task<bool> Insert(Client client)
        {
            if (client != null)
            {
                await _unitOfWork.Client.AddAsync(client);
                await _unitOfWork.CommitAsync();
                return true;

            }
            return false;
        }

        public Task<int> TotalCount(string Filtre, int CategoryId, int CurrentPage, int PageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Client client)
        {
            await _unitOfWork.Client.UpdateAsync(client);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
