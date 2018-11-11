using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class AverageGrossSalarySeeder : BaseSeeder
    {
        internal static List<AverageGrossSalary> GetData() => GetData<AverageGrossSalary>();
    }
}