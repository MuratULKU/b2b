using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICharAsgnService
    {
        Task<CharAsgn> GetByCode(string code);
        Task<IResult> Insert(CharAsgn charAsgn);
        Task<IResult> Update(CharAsgn charAsgn);
        Task DeleteAll();
    }
}
