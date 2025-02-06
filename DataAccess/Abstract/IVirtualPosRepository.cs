using Core.Abstract;
using Entity;
using System.Linq.Expressions;


namespace DataAccess.Abstract
{
    public interface IVirtualPosRepository:IRepository<VirtualPos>
    {
      public Task<VirtualPos> GetVirtualPosAsync(Guid id, Expression<Func<VirtualPos, object>>[]includes);
    }
}
