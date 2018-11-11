using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class MarriagesSeeder : BaseSeeder
    {
        internal static List<Marriages> GetData() => GetData<Marriages>();
    }
}