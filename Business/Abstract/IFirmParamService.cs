using Core.Abstract;
using Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFirmParamService
    {
        Task<List<FirmParam>> GetAll();
        Task<FirmParam> Get(int No);
        string ToString(int no);
        bool ToBoolean(int no);
        int ToInteger(int no);
        Task<IResult> Create(FirmParam firmParam);
        Task<IResult> Update(FirmParam firmParam);
        Task<IResult> Delete(FirmParam firmParam);
    }
}
