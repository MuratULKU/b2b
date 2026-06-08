using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using Core.Logger;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Business.Concrete
{
    public class OrderManager : IOrderService, IDisposable
    {


        private readonly IUnitofWork _unitOfWork;
        private readonly ILoggerService _logger;
        public OrderManager(IUnitofWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task DeleteLine(Guid id)
        {
            var entity = new OrdLine { Id = id };

            _unitOfWork.Repository<OrdLine>().Delete(entity);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddLine(OrdLine ordLine)
        {
            await _unitOfWork.Repository<OrdLine>().AddAsync(ordLine);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IResult> UpdateLine(OrdLine ordLine)
        {
            await _unitOfWork.Repository<OrdLine>().UpdateAsync(ordLine);

            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0
                ? new Result(ResultStatus.Success, "Kayıt işlemi tamamlandı")
                : new Result(ResultStatus.Error, "Güncelleme başarısız");
        }



        //public async Task<IResult> Save(OrdFiche ordFiche)
        //{
        //    try
        //    {
        //        //var deletedEntries = _unitOfWork.ChangedEntries().Where(e => e.State == EntityState.Deleted)
        //        //    .ToArray();

        //        var addEntries = _unitOfWork.ChangeTracker.Entries();
        //        //if(addEntries.)
        //        //var updateEntries = _unitOfWork.ChangedEntries().Where(e =>e.State == EntityState.Modified).ToArray();


        //        if (ordFiche.Lines == null || ordFiche.Lines.Count == 0)
        //        {
        //            var existing = await _unitOfWork.OrdFiche.SingleOrDefaultAsync(x => x.Id == ordFiche.Id);
        //            if (existing != null)
        //            {
        //                var entyr = _unitOfWork.Entry(existing);
        //                entyr.CurrentValues.SetValues(ordFiche);
        //                entyr.State = EntityState.Deleted;
        //            }
        //            else
        //            {

        //                await _unitOfWork.OrdFiche.Delete(ordFiche);
        //            }
        //        }
        //        else
        //        {
        //            if (ordFiche.Id == Guid.Empty)
        //            {
        //                foreach (OrdLine ordLine in ordFiche.Lines)
        //                {
        //                    ordLine.Product = null;
        //                }
        //                await _unitOfWork.Repository<OrdFiche>().AddAsync(ordFiche);
        //            }
        //            else
        //            {
        //                // kayıt takip edilyormu diye kontrol etmek gerekiyor
        //                //updateden sonra commit yapılsa bile traked poz. kalıyor.
        //                var trordfiche = _unitOfWork.ChangeTracker.Entries<OrdFiche>().Where(e => e.Entity.Id == ordFiche.Id);
        //                    if (trordfiche.Count() > 0)
        //                    return new Result(ResultStatus.Error,"Kayıt Kullanılıyor.");
        //                    await _unitOfWork.Repository<OrdFiche>().UpdateAsync(ordFiche);
        //            }
        //        }

        //        var result = await _unitOfWork.SaveChangesAsync();
        //       var trordfiche = _unitOfWork.ChangeTracker.Entries<OrdFiche>().Where(e => e.Entity.Id == ordFiche.Id)
        //       .ToList();

        //       foreach (var entry in trordfiche)
        //            entry.State = EntityState.Detached;
        //        if (result > 0)
        //        {
        //            return new Result(ResultStatus.Success, "Kayıt Başarılı");

        //        }
        //        else
        //        {
        //            return new Result(ResultStatus.Error, "Kayıt Hatalı");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Result(ResultStatus.Error, ex.Message);
        //    }

        //}

        public async Task<IResult> Save(OrdFiche ordFiche)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (ordFiche.Id == Guid.Empty)
                {
                    foreach (var line in ordFiche.Lines)
                    {
                        line.Product = null;
                    }

                    await _unitOfWork.Repository<OrdFiche>().AddAsync(ordFiche);
                }
                else
                {
                    var existing = await _unitOfWork.OrdFiche
                        .FirstOrDefaultAsync(x => x.Id == ordFiche.Id, x => x.Include(x => x.Lines));
                    // ordFiche.User = null; pss hata veriyor

                    if (existing == null)
                        throw new Exception("Kayıt Bulunamadı");

                    var trackedEntry = _unitOfWork.ChangeTracker
                        .Entries<OrdFiche>()
                        .FirstOrDefault(x => x.Entity.Id == ordFiche.Id);

                    if (trackedEntry == null)
                    {
                        await _unitOfWork.Repository<OrdFiche>().UpdateAsync(ordFiche);
                    }
                    else
                    {
                        trackedEntry.CurrentValues.SetValues(ordFiche);
                    }
                    if (ordFiche.Lines != null)
                        SyncLines(existing, ordFiche);
                }

                var result = await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();
                return result >= 0
                    ? new Result(ResultStatus.Success, "Kayıt Başarılı")
                    : new Result(ResultStatus.Error, "Kayıt Hatalı");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new Result(ResultStatus.Error, ex.Message);
            }
        }

        private void SyncLines(OrdFiche existing, OrdFiche incoming)
        {

            var deletedLines = existing.Lines
                .Where(x => !incoming.Lines.Any(i => i.Id == x.Id))
                .ToList();

            foreach (var line in deletedLines)
            {

                _unitOfWork.Repository<OrdLine>().Delete(line);
            }


            foreach (var line in incoming.Lines)
            {
                var existingLine = existing.Lines
                    .FirstOrDefault(x => x.Id == line.Id);


                if (existingLine == null)
                {
                    _unitOfWork.Repository<OrdLine>().AddAsync(line);
                }
                else
                {
                    _unitOfWork.Repository<OrdLine>().UpdateAsync(line);
                }
            }
        }

        public async Task<int> GetOrderFicheCount(int trCode)
        {
            return await _unitOfWork.Repository<OrdFiche>().RowCount(x => x.TrCode == trCode);
        }

        public async Task<int> GetOrderFicheCount(Guid firmId, int trCode)
        {
            return await _unitOfWork.Repository<OrdFiche>().RowCount(x => x.TrCode == trCode && x.CompanyId == firmId);
        }

        public async Task<List<OrdFiche>> GetOrderFiche(int trCode, int CurrentPage, int PageSize)
        {
            var result = await _unitOfWork.OrdFiche
                .GetOrderFiche(trCode, CurrentPage: CurrentPage, PageSize: PageSize);

            return result;
        }

        public async Task<List<OrdFiche>> GetOrderFiche(int trCode)
        {
            var result = await _unitOfWork.Repository<OrdFiche>().Find(x => x.TrCode == trCode);
            return result;
        }

        public async Task<OrdFiche> GetOrderFiche(short send, Guid userId)
        {
            var result = await _unitOfWork.Repository<OrdFiche>().SingleOrDefaultAsync(
                x => x.Send == send && x.UserId == userId,
                x => x.AsNoTracking().Include(x => x.Lines)  // Include the Lines collection
                      .ThenInclude(line => line.Product) // Then include the Product for each Line
);

            return result;
        }

        public async Task<OrdFiche> GetOrderFiche(Guid id)
        {
            var result = await _unitOfWork.Repository<OrdFiche>().SingleOrDefaultAsync(
                x => x.Id == id,
                x => x.Include(x => x.Lines)  // Include the Lines collection
                      .ThenInclude(line => line.Product)  // Then include the Product for each Line
);

            return result;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

        public async Task<List<OrdFiche>> GetOrderFiche(
     int trCode,
     byte send,
     bool include = false)
        {
            if (include)
            {
                return await _unitOfWork.Repository<OrdFiche>()
                    .Find(
                        x => x.TrCode == trCode && x.Send == send,
                        includes: x => x
                            .Include(x => x.Lines)
                                .ThenInclude(y => y.Product)
                            .Include(x => x.User)
                            .AsNoTracking()
                    );
            }

            return await _unitOfWork.Repository<OrdFiche>()
                .Find(
                    x => x.TrCode == trCode && x.Send == send,
                    includes: x => x
                            .Include(x => x.Lines)
                );
        }

        public async Task DeleteOrderFiche(OrdFiche ordFiche)
        {
            var tracked = _unitOfWork.ChangeTracker
     .Entries<OrdFiche>()
     .FirstOrDefault(x => x.Entity.Id == ordFiche.Id);

            if (tracked != null)
            {
                // Zaten tracking'de olan entity
                await _unitOfWork.Repository<OrdFiche>().Delete(tracked.Entity);
            }
            else
            {
                // Tracking'de yok → attach et
                await _unitOfWork.Repository<OrdFiche>().Delete(ordFiche);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<OrdFiche> GetOrderFiche(int send, Guid userId)
        {
            var result = await _unitOfWork.Repository<OrdFiche>().FirstOrDefaultAsync(x => x.Send == send && x.UserId == userId, x => x.Include(x => x.Lines).ThenInclude(x => x.Product).AsNoTracking());
            return result;
        }

        public async Task<List<OrdFiche>> GetOrderFiche(Guid FirmId, int trCode, int CurrentPage, int PageSize)
        {
            var result = await _unitOfWork.OrdFiche
                  .GetOrderFiche(FirmId, trCode, CurrentPage, PageSize);

            return result;
        }

        Task<OrdFiche> IOrderService.GetOrderFiche(int id)
        {
            return _unitOfWork.Repository<OrdFiche>().FirstOrDefaultAsync(x => x.LogicalRef == id);
        }

        public async Task DeleteLine(OrdLine ordLine)
        {
            var existing = await _unitOfWork.Repository<OrdLine>().SingleOrDefaultAsync(x => x.Id == ordLine.Id);
            if (existing != null)
            {
                await _unitOfWork.Repository<OrdLine>().Delete(ordLine);
                await _unitOfWork.SaveChangesAsync();
            }


        }

    }
}
