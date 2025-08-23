using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task DeleteAll();
        Task<List<Category>> Get(int parentref);
        Task<Category> GetByRefNo(int refno);
        Task<Category> Get(Guid id);
        Task<Category> GetByCode(string code);
        Task<IResult> Insert(Category category);
        Task<IResult> Update(Category category);
        Task<IResult> Delete(Category category);
        Task<int> Refno();
    }
}
