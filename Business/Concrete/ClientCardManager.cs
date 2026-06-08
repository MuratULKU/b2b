using Business.Abstract;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;


namespace Business.Concrete
{
    public class ClientCardManager : IClientCardService
    {
        private readonly IUnitofWork _unitOfWork;

        public ClientCardManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> DeleteAll()
        {
            return await _unitOfWork.Product.DeleteAll();
        }

        public async Task<List<Client>> GetAll()
        {
            return await _unitOfWork.Repository<Client>().GetAllAsync();

        }

        public List<Client> GetAll(int currentPage, int pageSize)
        {
            return GetAll().Result;
        }

        public Task<List<Client>> GetAllAsync(string Filtre, int CurrentPage, int PageSize)
        {
            return _unitOfWork.Repository<Client>().Find(x => x.Name.ToLower().Contains(Filtre.ToLower()), null, CurrentPage, PageSize);
        }

        public async Task<Client> GetByCode(string Code)
        {
            return await _unitOfWork.Repository<Client>().SingleOrDefaultAsync(x => x.Code == Code);

        }

        public async Task<Client> GetByGuid(Guid id)
        {
            return await _unitOfWork.Repository<Client>().SingleOrDefaultAsync(x => x.Id == id);

        }

        public async Task<bool> Insert(Client client)
        {
            if (client != null)
            {
                await _unitOfWork.Repository<Client>().AddAsync(client);
                var result = await _unitOfWork.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public Task<int> TotalCount(string Filtre, int CurrentPage, int PageSize)
        {
            return _unitOfWork.Repository<Client>().RowCount(x => x.Name.ToLower().Contains(Filtre.ToLower()));
        }

        public async Task<bool> Update(Client client)
        {
            await _unitOfWork.Repository<Client>().UpdateAsync(client);
            var result = await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<List<Client>> Search(string text)
        {
            text = text.ToLower();

            var data = await _unitOfWork.Repository<Client>()
               .GetAllAsync();

            return data
                .Where(x =>
                    x.Name.ToLower().Contains(text.ToLower()) ||
                    x.Code.ToLower().Contains(text.ToLower()) ||
                    x.VKN.ToLower().Contains(text.ToLower())
                )
                .ToList();
        }


    }
}