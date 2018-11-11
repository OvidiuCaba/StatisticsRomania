using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class DivorcesSeeder : BaseSeeder
    {
        internal static List<Divorces> GetData()
        {
            var rawData = new List<string>
            {
            };

            List<string> dataFromResources = GetDataFromResources(CountiesData.DivorcesSeeder);

            rawData.AddRange(dataFromResources);

            var items = GetItems<Divorces>(rawData);

            return items;
        }
    }
}