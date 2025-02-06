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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Concrete
{
    public class CharSetManager : ICharSetService
    {
        private readonly IUnitofWork _unitOfWork;

        public CharSetManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAll()
        {
            try
            {
                var charsets = await _unitOfWork.CharSet.GetAllAsync();
                foreach (var charset in charsets.Data)
                {
                    await _unitOfWork.CharSet.Delete(charset);
                }
                await _unitOfWork.CommitAsync();
               
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting character sets.", ex);

            }
            
        }

        public async Task<CharSet> GetByCode(string code)
        {
            var result =  await _unitOfWork.CharSet.SingleOrDefaultAsync(x => x.Code == code);
            return result.Data;
        }

        public async Task<IResult> Insert(CharSet charSet)
        {
            try
            {
                await _unitOfWork.CharSet.AddAsync(charSet);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Character set inserted successfully.");
            }
            catch (Exception ex)
            {
               return new Result(ResultStatus.Error, $"Insertion failed: {ex.Message}");
            }
        }

        public async Task<IResult> Update(CharSet charSet)
        {
            try
            {
               await _unitOfWork.CharSet.UpdateAsync(charSet);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Character set updated successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"Update failed: {ex.Message}");
            }
        }
    }
}
