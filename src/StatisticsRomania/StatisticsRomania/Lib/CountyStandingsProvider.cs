using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;
using StatisticsRomania.ViewModels;
using System.Linq.Expressions;

namespace StatisticsRomania.Lib
{
    public static class CountyStandingsProvider
    {
        public static bool IsWebSite { get; set; }

        public static async Task<List<StandingItem>> GetData(Type chapter, int year, int yearFraction)
        {
            if (chapter == typeof(ExportFob))
            {
                return await GetData<ExportFob>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(ImportCif))
            {
                return await GetData<ImportCif>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(SoldFobCif))
            {
                return await GetData<SoldFobCif>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(AverageGrossSalary))
            {
                return await GetData<AverageGrossSalary>(year, yearFraction);
            }
            
            if (chapter == typeof(AverageNetSalary))
            {
                return await GetData<AverageNetSalary>(year, yearFraction);
            }

            if (chapter == typeof(NumberOfTourists))
            {
                return await GetData<NumberOfTourists>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(NumberOfNights))
            {
                return await GetData<NumberOfNights>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(NumberOfEmployees))
            {
                return await GetData<NumberOfEmployees>(year, yearFraction);
            }

            if (chapter == typeof(Unemployed))
            {
                return await GetData<Unemployed>(year, yearFraction, isAscending: true);
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

        private static async Task<List<StandingItem>> GetData<T>(int year, int yearFraction, bool isAscending = false, bool isSum = false)
            where T : Data, new()
        {
            var repo = IsWebSite ? GetWebClientRepository<T>() : GetMobileRepository<T>();

            Expression<Func<T, bool>> filter = null;
            if (yearFraction == -1)
                filter = x => x.Year == year;
            else
                filter = x => x.Year == year && x.YearFraction == yearFraction;

            var rawData = await repo.GetAll(filter);

            if (yearFraction == -1)
            {
                var firstMonth = rawData.Min(x => x.YearFraction);
                for (var countyId = 1; countyId <= 42; countyId++)
                {
                    var countyDataItem = rawData.Find(x => x.YearFraction == firstMonth && x.CountyId == countyId);
                    var countyData = rawData.Where(x => x.CountyId == countyId);
                    countyDataItem.Value = (float)Math.Round(countyData.Sum(x => x.Value) / (isSum ? 1 : countyData.Count()));
                    rawData.RemoveAll(x => x.CountyId == countyId && x.YearFraction > firstMonth);
                }
            }

            var orderedRawData = isAscending ? rawData.OrderBy(x => x.Value) : rawData.OrderByDescending(x => x.Value);

            var data = await ProcessRawData(orderedRawData, repo);

            return data;
        }

        private static async Task<List<StandingItem>> ProcessRawData<T>(IEnumerable<Data> rawData, IRepository<T> repo)
            where T : Data, new()
        {
            var data = new List<StandingItem>();
            var index = 1;

            foreach (var item in rawData)
            {
                await repo.GetChild((T) item, x => x.County);

                var standingItem = new StandingItem() {Position = index++, County = item.County.Name, Value = item.Value};
                data.Add(standingItem);
            }

            return data;
        }
    }
}