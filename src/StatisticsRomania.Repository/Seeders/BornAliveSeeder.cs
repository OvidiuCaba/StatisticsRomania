using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class BornAliveSeeder : BaseSeeder
    {
        internal static List<BornAlive> GetData() => GetData<BornAlive>();
    }
}