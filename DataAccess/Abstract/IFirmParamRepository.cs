using Entity;

namespace DataAccess.Abstract
{
    public interface IFirmParamRepository
    {
       object Get(int No);
        List<FirmParam> GetAll();
        FirmParam Create(FirmParam firmParam);
        FirmParam Update(FirmParam firmParam);
        FirmParam Delete(FirmParam firmParam);
        void Update(int No, object Value);
    }
}
