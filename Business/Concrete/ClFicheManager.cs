using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ClFicheManager : IClFicheService
    {
        private readonly IUnitofWork _unitOfWork;

        public ClFicheManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ClFiche> GetClFiche(Guid id)
        {
           return _unitOfWork.ClFiche.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<ClFiche>> GetClFicheFiche(int trCode, byte send)
        {
           var result = await _unitOfWork.ClFiche.Find(x=>x.Send == send && x.TrCode == trCode);
            return result;
        }

        public Task<ClFiche> GetPaymetId(Guid id)
        {
            return _unitOfWork.ClFiche.SingleOrDefaultAsync(x=>x.PaymentTransactionId == id);
        }

        public async Task<IResult> Insert(ClFiche clFiche)
        {
           await _unitOfWork.ClFiche.AddAsync(clFiche);
           var result = await _unitOfWork.CommitAsync();
            if(result == 1)
            {
                return new Result(ResultStatus.Success, "Inserted Client Fiche");
            }
            return new Result(ResultStatus.Error, "Insert failed Client Fiche");
        }

        public async Task<IResult> Update(ClFiche clFiche)
        {
            await _unitOfWork.ClFiche.UpdateAsync(clFiche);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
            {
                return new Result(ResultStatus.Success, "Updated Client Fiche");
            }
            return new Result(ResultStatus.Error, "Update failed Client Fiche");
        }
    }
}
