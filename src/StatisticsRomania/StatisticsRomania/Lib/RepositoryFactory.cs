using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;

namespace StatisticsRomania.Lib
{
    public static class RepositoryFactory
    {
        public static IRepository<T> GetRepository<T>() where T : Data, new()
        {
            return GetWebClientRepository<T>();
        }

        private static IRepository<T> GetWebClientRepository<T>() where T : Data, new()
        {
            return new InMemoryRepository<T>();
        }

        public static IRepository<County> GetCountyRepository()
        {
            return GetWebClientCountyRepository();
        }

        private static IRepository<County> GetWebClientCountyRepository()
        {
            return new InMemoryRepository<County>();
        }
    }
}