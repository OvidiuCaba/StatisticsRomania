using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;
using StatisticsRomania.ViewModels;

namespace StatisticsRomania.Lib
{
    public static class CountyStandingsProvider
    {
        public static async Task<List<StandingItem>> GetData(Type chapter, int year, int yearFraction)
        {
            if (chapter == typeof(AverageGrossSalary))
            {
                var repo = new Repository<AverageGrossSalary>(App.AsyncDb);
                var rawData = (await repo.GetAll(x => x.Year == year && x.YearFraction == yearFraction))
                    .OrderByDescending(x => x.Value)
                    .Cast<Data>()
                    .ToList();

                var data = await ProcessRawData(rawData, repo);

                return data;
            }
            
            if (chapter == typeof(AverageNetSalary))
            {
                var repo = new Repository<AverageNetSalary>(App.AsyncDb);
                var rawData = (await repo.GetAll(x => x.Year == year && x.YearFraction == yearFraction))
                     .OrderByDescending(x => x.Value)
                     .Cast<Data>()
                     .ToList();

                var data = await ProcessRawData(rawData, repo);

                return data;
            }

            return null;
        }

        private static async Task<List<StandingItem>> ProcessRawData<T>(List<Data> rawData, Repository<T> repo)
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