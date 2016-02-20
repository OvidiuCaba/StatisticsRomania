using SQLite.Net.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsRomania.Repository
{
    public class Repostory<T> : IRepository<T> where T : class, new()
    {
        private SQLiteAsyncConnection db;

        public Repostory(SQLiteAsyncConnection db)
        {
            this.db = db;
        }

        public Task<List<T>> QueryAsync(string sql, params object[] args)
        {
            return db.QueryAsync<T>(sql, args);
        }

        public AsyncTableQuery<T> AsQueryable()
        {
            return db.Table<T>();
        }

        public async Task<List<T>> Get()
        {
            return await db.Table<T>().ToListAsync();
        }

        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null,
                                               Expression<Func<T, TValue>> orderBy = null,
                                               Expression<Func<T, TValue>> orderByDesc = null)
        {
            var query = db.Table<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                query = query.OrderBy<TValue>(orderBy);
            }
            if (orderByDesc != null)
            {
                query = query.OrderByDescending(orderByDesc);
            }

            return await query.ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await db.FindAsync<T>(id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await db.FindAsync<T>(predicate);
        }

        public async Task<int> Insert(T entity)
        {
            return await db.InsertAsync(entity);
            //await db.InsertWithChildrenAsync(entity);
            return 1;
        }

        public async Task<int> Update(T entity)
        {
            return await db.UpdateAsync(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await db.DeleteAsync(entity);
        }
    }
}