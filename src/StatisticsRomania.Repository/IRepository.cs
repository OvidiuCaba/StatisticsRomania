using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsRomania.Repository
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);

        Task<List<T>> GetAll<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null, Expression<Func<T, TValue>> orderByDesc = null);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> QueryAsync(string sql, params object[] args);
        AsyncTableQuery<T> AsQueryable();
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
        Task GetChild(T entity, Expression<Func<T, object>> propertyExpression);
    }
}