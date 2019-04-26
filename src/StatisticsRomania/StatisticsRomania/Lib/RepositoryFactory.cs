using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;

namespace StatisticsRomania.Lib
{
    public static class RepositoryFactory
    {
        public static IRepository<T> GetRepository<T>() where T : Data, new()
        {
            return new InMemoryRepository<T>();
        }

        public static IRepository<County> GetCountyRepository()
        {
            return new InMemoryRepository<County>();
        }
    }
}