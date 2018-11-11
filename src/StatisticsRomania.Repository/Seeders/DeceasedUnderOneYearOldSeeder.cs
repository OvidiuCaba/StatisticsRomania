using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class DeceasedUnderOneYearOldSeeder : BaseSeeder
    {
        internal static List<DeceasedUnderOneYearOld> GetData() => GetData<DeceasedUnderOneYearOld>();
    }
}