using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
          
            dbContext.Set<T>().Remove(entity);
            return entity;
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null, int currentPage = 0, int pageSize = 10000)
        {
            int skip = currentPage * pageSize;

            IQueryable<T> query = dbContext.Set<T>();

            query = includes?.Invoke(query) ?? query;
            if (pageSize > 0)
                query = query.Skip(skip).Take(pageSize);

            return await query.AsNoTracking().AsNoTracking().Where(predicate).ToListAsync();

        }

        public async Task<int> RowCount(Expression<Func<T, bool>> predicate)
        {
            var result = await dbContext.Set<T>().Where(predicate).CountAsync();
            return result;
        }

        public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (includes != null)
            {
                query = includes(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (includes != null)
            {
                query = includes(query);
            }
            return query.Where(predicate).ToListAsync();
        }


        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
           return await dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes)
        {
            IQueryable<T> query = dbContext.Set<T>();

            if (includes != null)
            {
                query = includes(query);
            }

            return await query.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            IQueryable<T> query = dbContext.Set<T>().AsNoTracking();

            if (includes != null)
            {
                query = includes(query);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return entity;
        }

        public async Task<int> RowCount()
        {
            var result = await dbContext.Set<T>().CountAsync();
            return result;
        }

       
    }
}
