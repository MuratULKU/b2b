using Entity;


namespace DataAccess.Abstract
{
    public interface IFirmDocRepository
    {
        void Insert(FirmDoc doc);
        void Update(FirmDoc doc);
        void Delete(FirmDoc doc);
        Task<int> DeleteAll();
    }
}
