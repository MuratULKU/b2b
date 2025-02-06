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
    public class PriceListManager : IPriceListService
    {
        private readonly IUnitofWork _unitOfWork;

        public PriceListManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAll()
        {
            try
            {
                var pricelists = await _unitOfWork.PriceList.GetAllAsync();
                foreach (var pricelist in pricelists.Data)
                {
                    await _unitOfWork.PriceList.Delete(pricelist);
                }
                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting character sets.", ex);

            }
        }

        public async Task<PriceList> GetByCode(string code)
        {
            var result = await _unitOfWork.PriceList.SingleOrDefaultAsync(x => x.Code == code);
            return result.Data;
        }

        public async Task<PriceList> GetByLogicalref(int Logicalref)
        {
            var result = await _unitOfWork.PriceList.SingleOrDefaultAsync(x => x.Logicalref == Logicalref);
            return result.Data;
        }

        public async Task<IResult> Insert(PriceList priceList)
        {
            try
            {
                await _unitOfWork.PriceList.AddAsync(priceList);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "PriceList inserted successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"Insertion failed: {ex.Message}");
            }
        }

        public async Task<IResult> Update(PriceList priceList)
        {
            try
            {
                await _unitOfWork.PriceList.UpdateAsync(priceList);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "PriceList updated successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"Update failed: {ex.Message}");
            }
        }
    }
}
