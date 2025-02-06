using Business.Abstract;
using Core.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CharValManager : ICharValService
    {
        private readonly IUnitofWork _unitOfWork;

        public CharValManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CharVal> GetByCode(string code)
        {
           var result = await _unitOfWork.CharVal.SingleOrDefaultAsync(x => x.Code == code);
            return result.Data;
        }
    }
}
