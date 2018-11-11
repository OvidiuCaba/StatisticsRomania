using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class DeceasedSeeder : BaseSeeder
    {
        internal static List<Deceased> GetData()
        {
            var rawData = new List<string>
            {
            };

            List<string> dataFromResources = GetDataFromResources(CountiesData.DeceasedSeeder);

            rawData.AddRange(dataFromResources);

            var items = GetItems<Deceased>(rawData);

            return items;
        }
    }
}
