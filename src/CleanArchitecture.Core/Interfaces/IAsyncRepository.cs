using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

/// <summary>
/// Asynchronous generic repository interface.
/// </summary>

namespace CleanArchitecture.Core.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        Task<IReadOnlyList<T>> ListAsync(params Expression<Func<T, object>>[] children);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
