
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbContext _context;
        protected DbSet<T> Table { get; set; }

        
        public GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));  // Thêm kiểm tra null
            Table = _context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public virtual void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        //The code Teacher provided used bool and return true, despite the .Remove(entity) 
        //only return an object despite being available in the context or not
        //And SaveChange only throw 
        public virtual void Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public virtual T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual T? GetById(long code)
        {
            return _context.Set<T>().Find(code);
        }

        public virtual T? GetById(string code)
        {
            return _context.Set<T>().Find(code);
        }

        //Use guids when you have multiple independent systems or clients generating ID's that need to be unique. 
        //http://stackoverflow.com/questions/371762/ddg#371788
        //    //Still don't know why we use here?
        public virtual T? GetById(Guid code)
        {
            return _context.Set<T>().Find(code);
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }
        public virtual IQueryable<T> Include<TProperty>(Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            return _context.Set<T>().Include(navigationPropertyPath);
        }
        public virtual IQueryable<T> AsQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual IOrderedQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            return _context.Set<T>().OrderBy(keySelector);
        }
        public virtual IOrderedQueryable<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
        {
            return _context.Set<T>().OrderByDescending(keySelector);
        }
        public virtual IOrderedQueryable<T> ThenBy<TKey>(
            IOrderedQueryable<T> query,
            Expression<Func<T, TKey>> keySelector)
        {
            return query.ThenBy(keySelector);
        }
        public virtual IOrderedQueryable<T> ThenByDescending<TKey>(
            IOrderedQueryable<T> query,
            Expression<Func<T, TKey>> keySelector)
        {
            return query.ThenByDescending(keySelector);
        }
        #region Asyncronous

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<T?> GetByIdAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> GetByIdAsync(string code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        public virtual async Task<T?> GetByIdAsync(Guid code)
        {
            return await _context.Set<T>().FindAsync(code);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        #endregion


        #region Separating asigned entities and save operators        
        //
        //This will ONLY MODIFY data to the context. but now save down to the database
        public virtual void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        //This will ONLY MODIFY data to the context. but now save down to the database
        public virtual void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        //This will ONLY MODIFY data to the context. but now save down to the database
        public virtual void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        //This will save the context down to the database
        public virtual int Save()
        {
            return _context.SaveChanges();
        }

        //This will save the context down to the database
        public virtual async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion Separating asign entity and save operators
        
    }
}
