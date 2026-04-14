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

        public async Task<IDataResult<PaymentTransaction>> GetById(Guid id, bool includeBank = false)
        {
           
            return new DataResult<PaymentTransaction>(ResultStatus.Success, (await _unitOfWork.Payment.SingleOrDefaultAsync(x => x.Id == id)));
        }

        public async Task<IDataResult<PaymentTransaction>> GetByOrderNumber(Guid orderNumber, bool includeBank = false)
        {
            var result =  await _unitOfWork.Payment.SingleOrDefaultAsync(x=>x.OrderNumber == orderNumber,q=>q.Include(x=>x.VirtualPos).ThenInclude(x=>x.BankCard).Include(y=>y.Company));
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
            return _unitOfWork.Payment.GetAllAsync().Result.Sum(x => x.TotalAmount);
        }

        public decimal GetTotalAmount(DateTime startDate)
        {
           return _unitOfWork.Payment.Find(x=>x.PaidDate == startDate).Result.Sum(x=>x.TotalAmount);
        }

        public async Task<IDataResult<List<PaymentTransaction>>> GetUserId(Guid userId, DateTime startDate, DateTime endDate,int currentPage = 0, int pageSize =10)
        {
            var result =  await _unitOfWork.Payment.Find(x => x.UserId == userId && x.CreateDate >= startDate || x.CreateDate <= endDate, includes:x=>x.Include(q=>q.User).Include(q=>q.Company));
            if (result != null)
                return new DataResult<List<PaymentTransaction>>(ResultStatus.Success, result);
            return new DataResult<List<PaymentTransaction>>(ResultStatus.Error, result);
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
          await _unitOfWork.Payment.AddAsync(paymentTransaction);
          // await _unitOfWork.VirtualPoses.GetByIdAsync(paymentTransaction.VirtualPosId);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
            return new Result(ResultStatus.Error, "Hatalı İşlem");
        }

        public  async Task<IResult> Update(PaymentTransaction paymentTransaction)
        {
            await  _unitOfWork.Payment.UpdateAsync(paymentTransaction);
           
            var result = await _unitOfWork.CommitAsync();
            if (result > 0 )
                return new Result(ResultStatus.Success, "Kayıt İşlemi Tamanlandı");
            return new Result(ResultStatus.Error, "Hatalı İşlem");
        }
    }
}
