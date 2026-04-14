using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using Core.Logger;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;


namespace Business.Concrete
{
    public class OrderManager : IOrderService,IDisposable
    {
      

        private readonly IUnitofWork _unitOfWork;
      
        public OrderManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteLine(OrdLine ordLine)
        {
            var existing = await _unitOfWork.OrdLine.SingleOrDefaultAsync(x=>x.Id == ordLine.Id);
            if (existing != null)
            {
                var entyr = _unitOfWork.Entry(existing);
                entyr.CurrentValues.SetValues(ordLine);
                entyr.State = EntityState.Deleted;
            }
            else
            {
               await _unitOfWork.OrdLine.Delete(ordLine);
            }
            await _unitOfWork.CommitAsync();
        }  

        public async Task AddLine(OrdLine ordLine)
        {
            await _unitOfWork.OrdLine.AddAsync(ordLine);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IResult> UpdateLine(OrdLine ordLine)
        {
            var existing = await _unitOfWork.OrdLine.SingleOrDefaultAsync(x=>x.Id == ordLine.Id);
            if(existing != null)
            {
                // await _unitOfWork.OrdLine.UpdateAsync(ordLine);
                var entyr = _unitOfWork.Entry(existing);
                entyr.CurrentValues.SetValues(ordLine);
                entyr.State = EntityState.Modified;
                var result = await _unitOfWork.CommitAsync();
                if (result == 1)
                    return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
                else
                    return new Result(ResultStatus.Error, "Hatalı İşlem");
            }
            else
            {
                await _unitOfWork.OrdLine.UpdateAsync(ordLine);
                return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
            }
          
        }

        public async Task<IResult> Save(OrdFiche ordFiche)
        {
            try
            {
                //var deletedEntries = _unitOfWork.ChangedEntries().Where(e => e.State == EntityState.Deleted)
                //    .ToArray();

                //var addEntries = _unitOfWork.ChangeTracker.Entries();
                //if(addEntries.)
                //var updateEntries = _unitOfWork.ChangedEntries().Where(e =>e.State == EntityState.Modified).ToArray();
               

                if (ordFiche.Lines == null || ordFiche.Lines.Count == 0)
                {
                    var existing = await _unitOfWork.OrdFiche.SingleOrDefaultAsync(x => x.Id == ordFiche.Id);
                    if (existing != null)
                    {
                        var entyr = _unitOfWork.Entry(existing);
                        entyr.CurrentValues.SetValues(ordFiche);
                        entyr.State = EntityState.Deleted;
                    }
                    else
                    {

                        await _unitOfWork.OrdFiche.Delete(ordFiche);
                    }
                }
                else
                {
                    if (ordFiche.Id == Guid.Empty)
                    {
                        foreach(OrdLine ordLine in ordFiche.Lines)
                        {
                            ordLine.Product = null;
                        }
                      await _unitOfWork.OrdFiche.AddAsync(ordFiche);
                    }
                    else
                    {
                        // kayıt takip edilyormu diye kontrol etmek gerekiyor
                        //updateden sonra commit yapılsa bile traked poz. kalıyor.
                        var trackedOrdFiche = _unitOfWork.ChangeTracker
                            .Entries<OrdFiche>()
                            .Where(e => e.Entity.Id == ordFiche.Id)
                            .ToList();
                        foreach (var entry in trackedOrdFiche)
                            entry.State = EntityState.Detached;
                        _unitOfWork.Entry(ordFiche).State = EntityState.Modified;
                    }
                }

                var result =  await  _unitOfWork.CommitAsync();
                if(result > 0)
                {
                    return new Result(ResultStatus.Success, "Kayıt Başarılı");
                }
                else
                {
                    return new Result(ResultStatus.Error, "Kayıt Hatalı");
                }
            }
            catch (Exception ex)
            {

                return new Result(ResultStatus.Error, ex.Message);
            }

        }

        public async Task<int> GetOrderFicheCount(int trCode)
        {
            return await _unitOfWork.OrdFiche.RowCount(x=>x.TrCode == trCode);
        }

        public async Task<int> GetOrderFicheCount(Guid firmId,int trCode)
        {
            return await _unitOfWork.OrdFiche.RowCount(x => x.TrCode == trCode && x.CompanyId == firmId);
        }

        public async Task<List<OrdFiche>> GetOrderFiche(int trCode, int CurrentPage, int PageSize)
        {
            var result = await _unitOfWork.OrdFiche
                .GetOrderFiche(trCode,CurrentPage:CurrentPage,PageSize:PageSize );

            return result;
        }

        public async Task<List<OrdFiche>> GetOrderFiche(int trCode)
        {
            var result = await _unitOfWork.OrdFiche.Find(x => x.TrCode == trCode);
            return result;
        }

        public async Task<OrdFiche> GetOrderFiche(short send, Guid userId)
        {
            var result = await _unitOfWork.OrdFiche.SingleOrDefaultAsync(
                x => x.Send == send && x.UserId == userId,
                x => x.AsNoTracking().Include(x => x.Lines)  // Include the Lines collection
                      .ThenInclude(line => line.Product) // Then include the Product for each Line
);

            return result;
        }

        public async Task<OrdFiche> GetOrderFiche(Guid id)
        {
            var result = await _unitOfWork.OrdFiche.SingleOrDefaultAsync(
                x => x.Id == id,
                x => x.AsNoTracking().Include(x => x.Lines)  // Include the Lines collection
                      .ThenInclude(line => line.Product)  // Then include the Product for each Line
);

            return result;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

        public async Task<List<OrdFiche>> GetOrderFiche(int trCode, byte send)
        {
            var result = await _unitOfWork.OrdFiche.Find(x => x.TrCode == trCode && x.Send == send,includes: x=>x.Include(x=>x.Lines).Include(x=>x.User).AsNoTracking());
            return result;
        }

        public async Task DeleteOrderFiche(OrdFiche ordFiche)
        {
           await _unitOfWork.OrdFiche.Delete(ordFiche);
           await _unitOfWork.CommitAsync();
        }

        public async Task<OrdFiche> GetOrderFiche(int send, Guid userId)
        {
           var result = await _unitOfWork.OrdFiche.SingleOrDefaultAsync(x => x.Send == send && x.UserId == userId,x=>x.Include(x=>x.Lines).ThenInclude(x=>x.Product));
           return result;
        }

        public async Task<List<OrdFiche>> GetOrderFiche(Guid FirmId, int trCode, int CurrentPage, int PageSize)
        {
            var result = await _unitOfWork.OrdFiche
                  .GetOrderFiche(FirmId,trCode,  CurrentPage, PageSize);

            return result;
        }

        Task<OrdFiche> IOrderService.GetOrderFiche(int id)
        {
           return _unitOfWork.OrdFiche.GetOrderFiche(id);
        }
    }
}
