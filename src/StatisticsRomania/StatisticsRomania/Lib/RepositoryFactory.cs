﻿using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;

namespace StatisticsRomania.Lib
{
    public static class RepositoryFactory
    {
        public static bool IsWebSite { get; set; }

        public static IRepository<T> GetRepository<T>() where T : Data, new()
        {
            return IsWebSite? GetWebClientRepository<T>() : GetMobileRepository<T>();
        }

        private static IRepository<T> GetMobileRepository<T>() where T : Data, new()
        {
            return new Repository<T>(App.AsyncDb);
        }

        private static IRepository<T> GetWebClientRepository<T>() where T : Data, new()
        {
            return new InMemoryRepository<T>();
        }

        public static IRepository<County> GetCountyRepository()
        {
            return IsWebSite ? GetWebClientCountyRepository() : GetMobileCountyRepository();
        }

        private static IRepository<County> GetMobileCountyRepository()
        {
            return new Repository<County>(App.AsyncDb);
        }

        private static IRepository<County> GetWebClientCountyRepository()
        {
            return new InMemoryRepository<County>();
        }
    }
}