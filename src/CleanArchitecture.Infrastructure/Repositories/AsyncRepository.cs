using CleanArchitecture.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

/// <summary>
/// Asynchronous generic repository.
/// </summary>

namespace CleanArchitecture.Infrastructure.Data
{
    public class AsyncRepository<T> : IAsyncRepository<T> where T : class
    {
        public AsyncRepository(AppDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<T>();
        }

        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children)
        {
            IQueryable<T> query = DbSet;

            foreach (Expression<Func<T, object>> child in children)
                query = query.Include(child);

            return query.FirstOrDefault(filter);
        }

        public async Task<IReadOnlyList<T>> ListAsync(params Expression<Func<T, object>>[] children)
        {
            DbContext.Set<T>().AsNoTracking();

            IQueryable<T> query = DbSet;

            foreach (Expression<Func<T, object>> child in children)
                query = query.Include(child);

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await DbContext.Set<T>().FindAsync(id);
            DbContext.Entry(entity).State = EntityState.Deleted;
            await DbContext.SaveChangesAsync();
        }
    }
}
