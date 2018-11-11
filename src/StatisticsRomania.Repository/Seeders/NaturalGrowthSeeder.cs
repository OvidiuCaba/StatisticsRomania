using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class NaturalGrowthSeeder : BaseSeeder
    {
        internal static List<NaturalGrowth> GetData() => GetData<NaturalGrowth>();
    }
}