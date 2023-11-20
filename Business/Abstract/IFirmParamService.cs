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
        List<FirmParam> GetAll();
        object Get(int No);
        string ToString(int no);
        bool ToBoolean(int no);
        int ToInteger(int no);
        FirmParam Create(FirmParam firmParam);
        FirmParam Update(FirmParam firmParam);
        FirmParam Delete(FirmParam firmParam);
    }
}
