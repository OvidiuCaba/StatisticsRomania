using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class NumberOfEmployeesSeeder : BaseSeeder
    {
        internal static List<NumberOfEmployees> GetData() => GetData<NumberOfEmployees>();
    }
}