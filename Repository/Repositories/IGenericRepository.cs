using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // Basic CRUD
        List<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Remove(T entity);
        T? GetById(int id);
        T? GetById(long code);
        T? GetById(string code);
        T? GetById(Guid code);
        int Count();
        IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath);
        IQueryable<T> AsQueryable();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IOrderedQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);
        IOrderedQueryable<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector);
        IOrderedQueryable<T> ThenBy<TKey>(IOrderedQueryable<T> query, Expression<Func<T, TKey>> keySelector);
        IOrderedQueryable<T> ThenByDescending<TKey>(IOrderedQueryable<T> query, Expression<Func<T, TKey>> keySelector);

        // Async CRUD
        Task<List<T>> GetAllAsync();
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        Task<T?> GetByIdAsync(long id);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetByIdAsync(string code);
        Task<T?> GetByIdAsync(Guid code);
        Task<int> CountAsync();

        // Staged Operations (Prepare only, no DB write)
        void PrepareCreate(T entity);
        void PrepareUpdate(T entity);
        void PrepareRemove(T entity);

        // Save pending changes
        int Save();
        Task<int> SaveAsync();
    }
}
