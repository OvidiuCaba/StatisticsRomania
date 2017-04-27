using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;

namespace StatisticsRomania.Lib
{
    public static class CountyDetailsProvider
    {
        public static bool IsWebSite { get; set; }

        public static async Task<List<Data>> GetData(int countyId, Type chapter)
        {
            if (chapter == typeof(ExportFob))
            {
                return await GetData<ExportFob>(countyId);
            }

            if (chapter == typeof(ImportCif))
            {
                return await GetData<ImportCif>(countyId);
            }

            if (chapter == typeof(SoldFobCif))
            {
                return await GetData<SoldFobCif>(countyId);
            }

            if (chapter == typeof(AverageGrossSalary))
            {
                return await GetData<AverageGrossSalary>(countyId);
            }

            if (chapter == typeof(AverageNetSalary))
            {
                return await GetData<AverageNetSalary>(countyId);
            }

            if (chapter == typeof(NumberOfTourists))
            {
                return await GetData<NumberOfTourists>(countyId);
            }

            if (chapter == typeof(NumberOfNights))
            {
                return await GetData<NumberOfNights>(countyId);
            }

            if (chapter == typeof(NumberOfEmployees))
            {
                return await GetData<NumberOfEmployees>(countyId);
            }

            if (chapter == typeof(Unemployed))
            {
                return await GetData<Unemployed>(countyId);
            }

            return null;
        }

        // TODO: extract this in a factory
        private static IRepository<T> GetMobileRepository<T>() where T : Data, new()
        {
            return new Repository<T>(App.AsyncDb);
        }

        private static IRepository<T> GetWebClientRepository<T>() where T : Data, new()
        {
            return new InMemoryRepository<T>();
        }

        private static async Task<List<Data>> GetData<T>(int countyId)
            where T : Data, new()
        {
            var repo = IsWebSite ? GetWebClientRepository<T>() : GetMobileRepository<T>();
            var data = (await repo.GetAll(x => x.CountyId == countyId))
                .Cast<Data>().ToList();
            return data;
        }
    }
}