using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsRomania.Repository
{
    public class InMemoryRepository<T> : IRepository<T>
        where T : class, new()
    {
        public Task<int> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get(int id)
        {
            // Note: I know that I use this method only to load counties; if this changes in the future, then I need to change the implementation
            return CountiesSeeder.GetData().First(x => x.Id == id) as T;
        }

        public Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null, Expression<Func<T, TValue>> orderByDesc = null)
        {
            throw new NotImplementedException(); 
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            var data = (List<T>)Type.GetType($"StatisticsRomania.Repository.Seeders.{typeof(T).Name}Seeder").GetRuntimeMethods().First(x => x.Name == "GetData").Invoke(null, new Type[] { });
            data = data.AsQueryable().Where(predicate).ToList();
            return data;
        }

        public async Task GetChild(T entity, Expression<Func<T, object>> propertyExpression)
        {
            // Note: I know that I use this method only to load counties; if this changes in the future, then I need to change the implementation
            var dataEntity = entity as Data;
            dataEntity.County = CountiesSeeder.GetData().First(x => x.Id == dataEntity.CountyId);
        }

        public Task<int> Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> QueryAsync(string sql, params object[] args)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}