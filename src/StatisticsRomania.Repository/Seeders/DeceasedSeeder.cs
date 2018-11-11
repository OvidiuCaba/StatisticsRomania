using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class DeceasedSeeder : BaseSeeder
    {
        internal static List<Deceased> GetData() => GetData<Deceased>();
    }
}