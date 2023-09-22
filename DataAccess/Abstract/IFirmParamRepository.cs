using Entity;

namespace DataAccess.Abstract
{
    public interface IFirmParamRepository
    {
        FirmParam firmParam();
        void Update(FirmParam param);
    }
}
