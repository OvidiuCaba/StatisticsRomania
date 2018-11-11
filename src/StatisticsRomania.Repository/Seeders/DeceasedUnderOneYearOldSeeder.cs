using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class DeceasedUnderOneYearOldSeeder : BaseSeeder
    {
        internal static List<DeceasedUnderOneYearOld> GetData()
        {
            var rawData = new List<string>
            {
            };

            List<string> dataFromResources = GetDataFromResources(CountiesData.DeceasedUnderOneYearOldSeeder);

            rawData.AddRange(dataFromResources);

            var items = GetItems<DeceasedUnderOneYearOld>(rawData);

            return items;
        }
    }
}
