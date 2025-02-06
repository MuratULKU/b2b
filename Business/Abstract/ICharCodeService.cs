using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICharCodeService
    {
        Task DeleteAll();
        Task<CharCode> GetByCode(string code);
        Task<IResult> Insert(CharCode code);
        Task<IResult> Update(CharCode code);
    }
}
