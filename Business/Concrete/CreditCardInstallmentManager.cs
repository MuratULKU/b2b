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

        public async Task<CreditCardInstallment> Create(CreditCardInstallment creditCardInstallment)
        {
            await _unitOfWork.CreditCardInstallment.AddAsync(creditCardInstallment);
            var result = await _unitOfWork.CommitAsync();
            return creditCardInstallment;
        }

        public async Task<CreditCardInstallment> Delete(CreditCardInstallment creditCardInstallment)
        {
            await _unitOfWork.CreditCardInstallment.Delete(creditCardInstallment);
            var result = await _unitOfWork.CommitAsync();
          

            return creditCardInstallment;
        }

        public async Task<CreditCardInstallment> Get(Guid id)
        {
            return await _unitOfWork.CreditCardInstallment.SingleOrDefaultAsync(x => x.Id == id);

        }

        public async Task<List<CreditCardInstallment>> GetAll()
        {
            return await _unitOfWork.CreditCardInstallment.GetAllAsync();
        }

        public Task<List<CreditCardInstallment>> GeyBankId(Guid id)
        {
            return _unitOfWork.CreditCardInstallment.Find(x => x.CreditCard.BankCardId == id);
        }

        public Task<List<CreditCardInstallment>> GeyCreditId(Guid id)
        {
            return _unitOfWork.CreditCardInstallment.Find(x => x.CreditCardId == id);
        }

        public async Task<IResult> Update(CreditCardInstallment creditCardInstallment)
        {
            await _unitOfWork.CreditCardInstallment.UpdateAsync(creditCardInstallment);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Başarılı");
            return new Result(ResultStatus.Error, "Taksit Daha Önce Eklenmiş");
        }
    }
}
