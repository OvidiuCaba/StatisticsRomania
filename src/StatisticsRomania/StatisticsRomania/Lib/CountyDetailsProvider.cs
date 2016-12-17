using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StatisticsRomania.Lib
{
    public static class CountyDetailsProvider
    {
        public static async Task<List<Data>> GetData(int countyId, string chapter)
        {
            await AzureService.Initialize();
            await AzureService.SyncData();

            var data = await AzureService.Table.Where(x => x.CountyId == countyId && x.Chapter == chapter).ToListAsync();

            return data;
        }
    }
}