using DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstract;
using Entity;


namespace SanalMagaza.DataAccess.Concrete
{
    public class VirtualPosRepository : IVirtualPosRepository
    {
        private readonly RepositoryContext _dbContext;

        public VirtualPosRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public VirtualPos Create(VirtualPos virtualPos)
        {
            _dbContext.VirtualPos.Add(virtualPos);
            _dbContext.SaveChanges();
            return virtualPos;
        }

        public VirtualPos Delete(VirtualPos virtualPos)
        {
            _dbContext.VirtualPos.Remove(virtualPos);
            _dbContext.SaveChanges();
            return virtualPos;
        }

        public VirtualPos Get(Guid id)
        {
            return _dbContext.VirtualPos.
                Include(x=>x.BankCard).
                FirstOrDefault(x => x.Id == id);
        }

        public VirtualPos GetByBankId(int bankCode,bool isBusiness)
        {
            var id = _dbContext.BankCards.FirstOrDefault(x=>x.BankCode == bankCode).Id;
            return _dbContext.VirtualPos.
                Include(x=>x.BankCard).
                ThenInclude(x=>x.Installments.Where(i=>i.Business == isBusiness && i.BankCardId == id)).
                FirstOrDefault(x => x.BankCardId == id);
            
        }

        
        public List<VirtualPos> GetAll()
        {
            return _dbContext.VirtualPos
                .Include(x=>x.BankCard)
                .Include(x=>x.BankCard.CreditCards)
                .ThenInclude(x=>x.Installments)
                .ToList();
        }

        public VirtualPos Update(VirtualPos virtualPos)
        {
            _dbContext.VirtualPos.Update(virtualPos);
            _dbContext.SaveChanges();
            return virtualPos;
        }

        public VirtualPos GetByBrandCode(int brandCode,bool isBusiness)
        {
            if (_dbContext.VirtualPos.FirstOrDefault(x => x.CardBrands == brandCode) == null)
                return null;
            var id = _dbContext.VirtualPos.FirstOrDefault(x => x.CardBrands == brandCode).BankCardId;
            return _dbContext.VirtualPos
                .Include(x => x.BankCard)
                .ThenInclude(x => x.Installments.Where(i => i.Business == isBusiness && i.BankCardId == id))
                .FirstOrDefault(x => x.BankCardId == id);



        }
    }
}
