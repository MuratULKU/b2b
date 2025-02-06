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
    public class CharCodeManager : ICharCodeService
    {
        private readonly IUnitofWork _unitOfWork;

        public CharCodeManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAll()
        {
            try
            {
                var charcodes = await _unitOfWork.CharCode.GetAllAsync();
                foreach (var charcode in charcodes.Data)
                {
                    await _unitOfWork.CharCode.Delete(charcode);
                }
                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting char code.", ex);

            }
        }

        public async Task<CharCode> GetByCode(string code)
        {
            var result = await _unitOfWork.CharCode.SingleOrDefaultAsync(x => x.Code == code);
            return result.Data;
        }

        public async Task<IResult> Insert(CharCode code)
        {

            try
            {
                await _unitOfWork.CharCode.AddAsync(code);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Charcode set inserted successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"Insertion failed: {ex.Message}");
            }
        }

        public async Task<IResult> Update(CharCode code)
        {
            try
            {
                await _unitOfWork.CharCode.UpdateAsync(code);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Charasgn set updated successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"Update failed: {ex.Message}");
            }
        }
    }
}
