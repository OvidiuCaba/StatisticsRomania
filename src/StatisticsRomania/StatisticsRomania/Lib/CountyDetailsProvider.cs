using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsRomania.Lib
{
    public static class CountyDetailsProvider
    {
        public static async Task<List<Data>> GetData(int countyId, int countyId2, string chapter)
        {
            await AzureService.Initialize();

            var data = await AzureService.Table
                                    .Where(x => x.CountyId == countyId && x.Chapter == chapter)
                                    .OrderBy(x => x.Year)
                                    .ThenBy(x => x.YearFraction)
                                    .ToListAsync();

            var data2 = await AzureService.Table
                                    .Where(x => x.CountyId == countyId2 && x.Chapter == chapter)
                                    .OrderBy(x => x.Year)
                                    .ThenBy(x => x.YearFraction)
                                    .ToListAsync();

            foreach (var item2 in data2)
            {
                var item = data.FirstOrDefault(x => x.Year == item2.Year && x.YearFraction == item2.YearFraction);
                if (item != null)
                {
                    item.Value2 = item2.Value;
                }
            }

            return data;
        }
    }
}