using Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task<IDataResult<T>> GetByIdAsync(Guid id);
        Task<IDataResult<List<T>>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> includes = null);
        Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null);
        Task<IResult> AddAsync(T entity);
        Task<IResult> UpdateAsync(T entity);
        Task<IResult> Delete(T entity);
        Task<int> RowCount();
        Task<int> RowCount(Expression<Func<T, bool>> predicate);
        Task<IDataResult<List<T>>> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null,int CurrentPage = 0, int PageSize=0);
        Task<IDataResult<T>> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes);
    }
}
