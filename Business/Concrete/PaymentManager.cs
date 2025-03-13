using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;


namespace Business.Concrete
{
    public class PaymentManager:IPaymentService
    {
        private readonly IUnitofWork _unitOfWork;

        public PaymentManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IDataResult<PaymentTransaction>> GetById(Guid id, bool includeBank = false)
        {
            return _unitOfWork.Payment.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IDataResult<PaymentTransaction>> GetByOrderNumber(Guid orderNumber, bool includeBank = false)
        {
            var result =  await _unitOfWork.Payment.SingleOrDefaultAsync(x=>x.OrderNumber == orderNumber,q=>q.Include(x=>x.VirtualPos).ThenInclude(x=>x.BankCard));
            return new DataResult<PaymentTransaction>(ResultStatus.Success, result);
        }

        public Task<int> GetCompanyTotalCount(Guid companyId, DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Payment.RowCount(x=>x.CompanyId == companyId && x.CreateDate >= startDate && x.CreateDate <= endDate);
        }

        public   Task<List<PaymentTransaction>> GetFull(DateTime startDate, DateTime endDate, int currentPage, int pageSize)
        {
            return _unitOfWork.Payment.GetPaymentTransaction(startDate,endDate,currentPage,pageSize);
        }

        public Task<List<PaymentTransaction>> GetPaymentTransaction(Guid userId, DateTime startDate, DateTime endDate, int currentPage = 0, int pageSize = 10)
        {
           return _unitOfWork.Payment.GetPaymentTransaction(userId, startDate, endDate, currentPage, pageSize); 
        }

        public decimal GetTotalAmount()
        {
            return _unitOfWork.Payment.GetAllAsync().Result.Data.Sum(x => x.TotalAmount);
        }

        public decimal GetTotalAmount(DateTime startDate)
        {
           return _unitOfWork.Payment.Find(x=>x.PaidDate == startDate).Result.Data.Sum(x=>x.TotalAmount);
        }

        public Task<IDataResult<List<PaymentTransaction>>> GetUserId(Guid userId, DateTime startDate, DateTime endDate,int currentPage = 0, int pageSize =10)
        {
            return _unitOfWork.Payment.Find(x => x.UserId == userId && x.CreateDate >= startDate || x.CreateDate <= endDate, includes:x=>x.Include(q=>q.User).Include(q=>q.Company));
        }

        public Task<IDataResult<List<PaymentTransaction>>> GetUserId(Guid userId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalCount(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Payment.RowCount(x=>x.CreateDate >= startDate && x.CreateDate <= endDate);
        }

        public async Task<IResult> Insert(PaymentTransaction paymentTransaction)
        {
           var result = await _unitOfWork.Payment.AddAsync(paymentTransaction);
            await _unitOfWork.VirtualPoses.GetByIdAsync(paymentTransaction.VirtualPosId);
           
            await _unitOfWork.CommitAsync();
            return result;
        }

        public  async Task<IResult> Update(PaymentTransaction paymentTransaction)
        {
            var result = await  _unitOfWork.Payment.UpdateAsync(paymentTransaction);
            var clFiche = await _unitOfWork.ClFiche.GetByIdAsync(paymentTransaction.Id);
            if (clFiche.Data == null)
            {
                await _unitOfWork.Company.GetByIdAsync(paymentTransaction.CompanyId);
                ClFiche _clFiche = new ClFiche();
                _clFiche.Id = paymentTransaction.Id;
                _clFiche.BankCode = paymentTransaction.VirtualPos.AccountCode;
                _clFiche.CreateUser = paymentTransaction.CreateUser;
                _clFiche.UpdateUser = paymentTransaction.UpdateUser;
                _clFiche.CreateDate = paymentTransaction.CreateDate;
                _clFiche.UpdateDate = paymentTransaction.UpdateDate;
                _clFiche.TrCode = 70;
                _clFiche.DocNo = "";
                _clFiche.Date = paymentTransaction.PaidDate ?? paymentTransaction.CreateDate;
                _clFiche.CardCode = paymentTransaction.Company.ProgramCode;
                _clFiche.ModulNr = 8;
                _clFiche.Sing = 1;
                _clFiche.Send = 1;
                _clFiche.LineExp = "";
                _clFiche.Amount = (double)paymentTransaction.TotalAmount;

                await _unitOfWork.ClFiche.AddAsync(_clFiche);
            }
            await _unitOfWork.CommitAsync();
            return result;
        }
    }
}
