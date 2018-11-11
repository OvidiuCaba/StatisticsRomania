using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class BuildingPermitsSeeder : BaseSeeder
    {
        internal static List<BuildingPermits> GetData() => GetData<BuildingPermits>();
    }
}