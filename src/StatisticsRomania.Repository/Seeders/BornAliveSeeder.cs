using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class BornAliveSeeder : BaseSeeder
    {
        internal static List<BornAlive> GetData()
        {
            var rawData = new List<string>
                              {
                              };

            List<string> dataFromResources = GetDataFromResources(CountiesData.BornAliveSeeder);

            rawData.AddRange(dataFromResources);

            var items = GetItems<BornAlive>(rawData);

            return items;
        }
    }
}