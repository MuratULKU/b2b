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

        public  Task<IDataResult<List<PaymentTransaction>>> GetFull(DateTime startDate, DateTime endDate)
        {
            return  _unitOfWork.Payment.Find(x=>x.CreateDate >= startDate || x.CreateDate <= endDate,x=>x.Include(q=>q.User).Include(q=>q.Company));
        }

        public decimal GetTotalAmount()
        {
            return _unitOfWork.Payment.GetAllAsync().Result.Data.Sum(x => x.TotalAmount);
        }

        public decimal GetTotalAmount(DateTime startDate)
        {
           return _unitOfWork.Payment.Find(x=>x.PaidDate == startDate).Result.Data.Sum(x=>x.TotalAmount);
        }

        public Task<IDataResult<List<PaymentTransaction>>> GetUserId(Guid userId, DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Payment.Find(x => x.UserId == userId && x.CreateDate >= startDate || x.CreateDate <= endDate, includes:x=>x.Include(q=>q.User).Include(q=>q.Company));
        }

        public async Task<IResult> Insert(PaymentTransaction paymentTransaction)
        {
           var result = await _unitOfWork.Payment.AddAsync(paymentTransaction);
            await _unitOfWork.CommitAsync();
            return result;
        }

        public  async Task<IResult> Update(PaymentTransaction paymentTransaction)
        {
            var result = await  _unitOfWork.Payment.UpdateAsync(paymentTransaction);
            await _unitOfWork.CommitAsync();
            return result;
        }
    }
}
