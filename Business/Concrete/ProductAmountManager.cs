using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductAmountManager : IProductAmountService
    {
        private readonly IUnitofWork _unitOfWork;

        public ProductAmountManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAll()
        {
            try
            {
                var productamounts = await _unitOfWork.ProductAmount.GetAllAsync();
                foreach (var productamount in productamounts.Data)
                {
                    await _unitOfWork.ProductAmount.Delete(productamount);
                }
                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting character sets.", ex);

            }
        }

        public async Task<ProductAmount> GetByCode(string code)
        {
            var result = await _unitOfWork.ProductAmount.SingleOrDefaultAsync(x => x.Code == code);
            return result.Data;
        }

        public async Task<IResult> Insert(ProductAmount productamount)
        {
            try
            {
                await _unitOfWork.ProductAmount.AddAsync(productamount);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Product Amount inserted successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"Insertion failed: {ex.Message}");
            }
        }

        public async Task<IResult> Update(ProductAmount productamount)
        {
            try
            {
                await _unitOfWork.ProductAmount.UpdateAsync(productamount);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Product Amount updated successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"Update failed: {ex.Message}");
            }
        }
    }
}
