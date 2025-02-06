using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CreditCardInstallmentManager : ICreditCardInstallmentService
    {
        private readonly IUnitofWork _unitOfWork;

        public CreditCardInstallmentManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public  CreditCardInstallment Create(CreditCardInstallment creditCardInstallment)
        {
            _unitOfWork.CreditCardInstallment.AddAsync(creditCardInstallment);
            _unitOfWork.CommitAsync();
            return creditCardInstallment;
        }

        public CreditCardInstallment Delete(CreditCardInstallment creditCardInstallment)
        {
            var result = _unitOfWork.CreditCardInstallment.Delete(creditCardInstallment);
            return creditCardInstallment;
        }

        public CreditCardInstallment Get(Guid id)
        {
            var result = _unitOfWork.CreditCardInstallment.SingleOrDefaultAsync(x => x.Id == id);  
            return result.Result.Data;
        }

        public List<CreditCardInstallment> GetAll()
        {
            return _unitOfWork.CreditCardInstallment.GetAllAsync().Result.Data;
        }

        public List<CreditCardInstallment> GeyBankId(Guid id)
        {
            return _unitOfWork.CreditCardInstallment.Find(x=>x.Id == id).Result.Data;
        }

        public List<CreditCardInstallment> GeyCreditId(Guid id)
        {
            return _unitOfWork.CreditCardInstallment.Find(x=>x.CreditCardId == id).Result.Data;  
        }

        public IResult Update(CreditCardInstallment creditCardInstallment)
        {
            var test = _unitOfWork.CreditCardInstallment.SingleOrDefaultAsync(x => x.CreditCardId == creditCardInstallment.CreditCardId && x.Installment == creditCardInstallment.Installment);
            if (test != null)
            {
                _unitOfWork.CreditCardInstallment.UpdateAsync(creditCardInstallment);
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            }
            else
                return new Result(ResultStatus.Error, "Taksit Daha Önce Eklenmiş");
        }
    }
}
