using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class MarriagesSeeder : BaseSeeder
    {
        internal static List<Marriages> GetData()
        {
            var rawData = new List<string>
            {
            };

            List<string> dataFromResources = GetDataFromResources(CountiesData.MarriagesSeeder);

            rawData.AddRange(dataFromResources);

            var items = GetItems<Marriages>(rawData);

            return items;
        }
    }
}