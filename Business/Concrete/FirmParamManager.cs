using Business.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FirmParamManager : IFirmParamService
    {
        private readonly IFirmParamRepository _firmParamRepository;

        public FirmParamManager(IFirmParamRepository firmParamRepository)
        {
            _firmParamRepository = firmParamRepository;
        }

        public FirmParam Create(FirmParam firmParam)
        {
            return _firmParamRepository.Create(firmParam);
        }

        public FirmParam Delete(FirmParam firmParam)
        {
            return _firmParamRepository.Delete(firmParam);
        }

        public object Get(int no)
        {
            return _firmParamRepository.Get(no);
        }

        public List<FirmParam> GetAll()
        {
            return _firmParamRepository.GetAll();
        }

        public FirmParam Update(FirmParam firmParam)
        {
            return _firmParamRepository.Update(firmParam);
        }

        public string ToString(int no)
        {
            return Encoding.UTF8.GetString((byte[])Get(no));
        }
        public bool ToBoolean(int no)
        {
            return Convert.ToBoolean(ToString(no));
        }

        public int ToInteger(int no)
        {
            return Convert.ToInt32(ToString(no));
        }

    }
}
