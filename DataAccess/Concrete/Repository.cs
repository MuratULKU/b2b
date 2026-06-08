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

        public async Task<List<T>> GetPagedAsync<TKey>(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IQueryable<T>> includes = null,
            Expression<Func<T, TKey>> orderBy = null)
        {
            IQueryable<T> query = dbContext.Set<T>();


            if (predicate != null)
                query = query.Where(predicate);


            if (includes != null)
                query = includes(query);
            if (orderBy != null)
                query = query.OrderBy(orderBy);


            query = query.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize);

            return await query.ToListAsync();

        }

        public async Task<TProperty> MaxAsync<TProperty>(
         Expression<Func<T, TProperty>> selector)
        {
            return await dbContext.Set<T>()
                .MaxAsync(selector);
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

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate,
                        Func<IQueryable<T>, IQueryable<T>> includes = null,
                        int currentPage = 0, int pageSize = 100)
        {
            IQueryable<T> query = dbContext.Set<T>();
            query = includes?.Invoke(query) ?? query;
            query = query.Where(predicate);
            if (pageSize > 0)
            {
                int skip = currentPage * pageSize;
                query = query.Skip(skip).Take(pageSize);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<int> RowCount(Expression<Func<T, bool>> predicate)
        {
            var result = await dbContext.Set<T>().Where(predicate).CountAsync();
            return result;
        }

        public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            try
            {
                IQueryable<T> query = dbContext.Set<T>();

                if (includes != null)
                {
                    query = includes(query);
                }
                var test = await query.ToListAsync();
                return test;
            }
            catch (Exception ex)
            {

                throw;
            }

            return null;
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
            IQueryable<T> query = dbContext.Set<T>();

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
