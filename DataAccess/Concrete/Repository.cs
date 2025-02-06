using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext dbContext;

        public Repository(DbContext context)
        {
            dbContext = context;
        }

        public async Task<IResult> AddAsync(T entity)
        {
            var result = await dbContext.Set<T>().AddAsync(entity);
            if (result.State == EntityState.Added)
                return new Result(ResultStatus.Success, message: "Kayıt Basarılı Şekilde Eklenmiştir.");
            return new Result(ResultStatus.Error, message: result.ToString());
        }

        public async Task<IResult> Delete(T entity)
        {
            var result = dbContext.Set<T>().Remove(entity);
            if (result.State == EntityState.Deleted)
                return new Result(ResultStatus.Success, message: "Kayıt Basarılı Şekilde Silinmiştir.");
            return new Result(ResultStatus.Error, message: result.ToString());
        }

        public async Task<IDataResult<List<T>>> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null, int currentPage = 0, int pageSize = 10000)
        {
            int skip = currentPage * pageSize;

            IQueryable<T> query = dbContext.Set<T>();

            if (includes != null)
                query = includes(query);
            if (pageSize > 0)
            {
                query = query.Skip(skip);
                query = query.Take(pageSize);
            }
            var result = await query.Where(predicate).ToListAsync();
            return new DataResult<List<T>>(ResultStatus.Success, result);

        }

        public async Task<int> RowCount(Expression<Func<T, bool>> predicate)
        {
            var result = await dbContext.Set<T>().Where(predicate).CountAsync();
            return result;
        }

        public async Task<IDataResult<List<T>>> GetAllAsync()
        {
            var result = await dbContext.Set<T>().ToListAsync();
            return new DataResult<List<T>>(ResultStatus.Success, result);
        }

        public async Task<IDataResult<T>> GetByIdAsync(Guid id)
        {
            var result = await dbContext.Set<T>().FindAsync(id);
            return new DataResult<T>(ResultStatus.Success, result);
        }

        public async Task<IDataResult<T>> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await dbContext.Set<T>().SingleOrDefaultAsync(predicate);
            return new DataResult<T>(ResultStatus.Success, result);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes)
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (includes != null)
            {
                query = includes(query);
            }

            return await query.SingleOrDefaultAsync(predicate);
        }


        public async Task<IResult> UpdateAsync(T entity)
        {
            var result = dbContext.Set<T>().Update(entity);

            if (result.State == EntityState.Added) return new Result(ResultStatus.Success, "Kayıt Başarıyla Eklendi");
            else if (result.State == EntityState.Modified) return new Result(ResultStatus.Success, "Kayıt Başarıyla Bubcellendi");
            return new Result(ResultStatus.Information, "İşlem Yapılmadı");
        }

        public async Task<int> RowCount()
        {
            var result = await dbContext.Set<T>().CountAsync();
            return result;
        }
    }
}
