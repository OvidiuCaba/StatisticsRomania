using System.Collections.Generic;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    internal class NumberOfTouristsSeeder : BaseSeeder
    {
        internal static List<NumberOfTourists> GetData() => GetData<NumberOfTourists>();
    }
}