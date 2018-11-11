using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class DivorcesSeeder : BaseSeeder
    {
        internal static List<Divorces> GetData() => GetData<Divorces>();
    }
}