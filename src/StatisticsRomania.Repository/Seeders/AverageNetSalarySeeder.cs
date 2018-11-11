using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class AverageNetSalarySeeder : BaseSeeder
    {
        internal static List<AverageNetSalary> GetData() => GetData<AverageNetSalary>();
    }
}