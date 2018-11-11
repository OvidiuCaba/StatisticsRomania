using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class UnemployedSeeder : BaseSeeder
    {
        internal static List<Unemployed> GetData() => GetData<Unemployed>();
    }
}