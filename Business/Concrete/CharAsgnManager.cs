using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;

using Entity;


namespace Business.Concrete
{
    public class CharAsgnManager :  ICharAsgnService
    {
        private readonly IUnitofWork _unitOfWork;

        public CharAsgnManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAll()
        {
            try
            {
                var charasgns = await _unitOfWork.CharAsgn.GetAllAsync();
                foreach (var charasgn in charasgns.Data)
                {
                    await _unitOfWork.CharAsgn.Delete(charasgn);
                }
                await _unitOfWork.CommitAsync();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while deleting character sets.", ex);

            }
        }

        public async Task<CharAsgn> GetByCode(string code)
        {
            var result = await _unitOfWork.CharAsgn.SingleOrDefaultAsync(x => x.Code == code);
            return result.Data;
        }

        public async Task<IResult> Insert(CharAsgn charAsgn)
        {
            try
            {
                await _unitOfWork.CharAsgn.AddAsync(charAsgn);
                await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Charasgn set inserted successfully.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, $"Insertion failed: {ex.Message}");
            }
        }

        public async Task<IResult> Update(CharAsgn charAsgn)
        {
            try
            {
                await _unitOfWork.CharAsgn.UpdateAsync(charAsgn);
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
