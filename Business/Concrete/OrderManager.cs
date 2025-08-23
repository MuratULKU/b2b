using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using Core.Logger;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService,IDisposable
    {
      

        private readonly IUnitofWork _unitOfWork;
        private readonly ILoggerService _logger;

        public OrderManager(IUnitofWork unitOfWork, ILoggerService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task DeleteLine(OrdLine ordLine)
        {
            await _unitOfWork.OrdLine.Delete(ordLine);
        }

        public async Task Save(OrdFiche ordFiche)
        {
            try
            {
                var r = _unitOfWork.ChangedEntries().ToArray();
                if (ordFiche.Lines == null || ordFiche.Lines.Count == 0)
                 await   _unitOfWork.OrdFiche.Delete(ordFiche);

                else
                {
                    if(ordFiche.Id == Guid.Empty)
                    {
                        foreach(OrdLine line in ordFiche.Lines)
                        {
                            line.Product = null;
                            await _unitOfWork.OrdLine.AddAsync(line);
                        }
                       
                        await _unitOfWork.OrdFiche.AddAsync(ordFiche);
                    }
                    else
                    {
                        if (ordFiche.Lines != null)
                        {
                            foreach (OrdLine line in ordFiche.Lines)
                            {
                                var entity = _unitOfWork.ChangedEntity<OrdLine>(line);
                                if (line.Id == Guid.Empty)
                                {
                                    line.Product = null;
                                    await _unitOfWork.OrdLine.AddAsync(line);
                                }
                                else
                                    await _unitOfWork.OrdLine.UpdateAsync(line);
                            }
                        }
                        await _unitOfWork.OrdFiche.UpdateAsync(ordFiche);
                    }
                }
              
              await  _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
               await Task.FromException(ex);
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
                x => x.Include(x => x.Lines)  // Include the Lines collection
                      .ThenInclude(line => line.Product).AsNoTracking()  // Then include the Product for each Line
);

            return result;
        }

        public async Task<OrdFiche> GetOrderFiche(Guid id)
        {
            var result = await _unitOfWork.OrdFiche.SingleOrDefaultAsync(
                x => x.Id == id,
                x => x.Include(x => x.Lines)  // Include the Lines collection
                      .ThenInclude(line => line.Product).AsNoTracking()  // Then include the Product for each Line
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
