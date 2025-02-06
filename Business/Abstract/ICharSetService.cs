
using Core.Abstract;
using Entity;

namespace Business.Abstract
{
    public interface ICharSetService
    {
        Task<CharSet> GetByCode(string code);
        Task DeleteAll();
        Task<IResult> Insert(CharSet charSet);   
        Task<IResult> Update(CharSet charSet);
      
    }
}
