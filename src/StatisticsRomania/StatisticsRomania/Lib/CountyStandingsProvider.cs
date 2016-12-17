using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository.Seeders;
using StatisticsRomania.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsRomania.Lib
{
    public static class CountyStandingsProvider
    {
        public static async Task<List<StandingItem>> GetData(string chapter, int year, int yearFraction, bool isAscending = false)
        {
            await AzureService.Initialize();
            await AzureService.SyncData();

            var rawData = await AzureService.Table.Where(x => x.Year == year && x.YearFraction == yearFraction && x.Chapter == chapter).ToListAsync();

            var orderedRawData = isAscending ? rawData.OrderBy(x => x.Value) : rawData.OrderByDescending(x => x.Value);

            var data = ProcessRawData(orderedRawData);

            return data;
        }

        private static List<StandingItem> ProcessRawData(IEnumerable<Data> rawData)
        {
            var counties = CountiesSeeder.GetData().ToDictionary(x => x.Id, x => x.Name);

            var data = new List<StandingItem>();
            var index = 1;

            foreach (var item in rawData)
            {
                var standingItem = new StandingItem() {Position = index++, County = counties[item.CountyId.Value], Value = item.Value};
                data.Add(standingItem);
            }

            return data;
        }
    }
}