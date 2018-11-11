using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class BuildingPermitsSeeder : BaseSeeder
    {
        internal static List<BuildingPermits> GetData()
        {
            var rawData = new List<string>
            {
            };

            List<string> dataFromResources = GetDataFromResources(CountiesData.BuildingPermitsSeeder);

            rawData.AddRange(dataFromResources);

            var items = GetItems<BuildingPermits>(rawData);

            return items;
        }
    }
}