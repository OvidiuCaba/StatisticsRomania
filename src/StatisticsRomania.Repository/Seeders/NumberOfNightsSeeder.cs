using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class NumberOfNightsSeeder : BaseSeeder
    {
        internal static List<NumberOfNights> GetData() => GetData<NumberOfNights>();
    }
}