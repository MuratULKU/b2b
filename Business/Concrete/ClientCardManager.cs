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
           return await _unitOfWork.Client.GetAllAsync();
           
        }

        public List<Client> GetAll(int currentPage, int pageSize)
        {
            return GetAll().Result;
        }

        public Task<List<Client>> GetAllAsync(string Filtre, int CurrentPage, int PageSize)
        {
            return _unitOfWork.Client.Find(x=>x.Name.ToLower().Contains(Filtre.ToLower()),null,CurrentPage,PageSize);
        }

        public async Task<Client> GetByCode(string Code)
        {
           return await _unitOfWork.Client.SingleOrDefaultAsync(x => x.Code == Code);
          
        }

        public async Task<Client> GetByGuid(Guid id)
        {
            return await _unitOfWork.Client.SingleOrDefaultAsync(x => x.Id == id);
         
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

        public Task<int> TotalCount(string Filtre, int CurrentPage, int PageSize)
        {
            return _unitOfWork.Client.RowCount(x => x.Name.ToLower().Contains(Filtre.ToLower()));
        }

        public async Task<bool> Update(Client client)
        {
            await _unitOfWork.Client.UpdateAsync(client);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
