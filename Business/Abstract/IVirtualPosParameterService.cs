using Core.Abstract;
using Entity;


namespace Business.Abstract
{
    public interface IVirtualPosParameterService
    {
        Task<List<VirtualPosParameter>> GetAll(Guid BankId);
        Task<IResult> CreateBankParameter(VirtualPosParameter bankParameter);
        Task<IResult> UpdateBankParameter(VirtualPosParameter bankParameter);
        Task<IResult> DeleteBankParameter(VirtualPosParameter bankParameter);
    }
}
