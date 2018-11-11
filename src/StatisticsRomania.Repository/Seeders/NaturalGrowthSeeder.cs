using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class NaturalGrowthSeeder : BaseSeeder
    {
        internal static List<NaturalGrowth> GetData()
        {
            var rawData = new List<string>
            {
            };

            List<string> dataFromResources = GetDataFromResources(CountiesData.NaturalGrowthSeeder);

            rawData.AddRange(dataFromResources);

            var items = GetItems<NaturalGrowth>(rawData);

            return items;
        }
    }
}