using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;

namespace StatisticsRomania.Lib
{
    public static class CountyDetailsProvider
    {
        public static async Task<List<Data>> GetData(int countyId, Type chapter)
        {
            if (chapter == typeof(AverageGrossSalary))
            {
                var repo = new Repository<AverageGrossSalary>(App.AsyncDb);
                var data = await repo.GetAll(x => x.CountyId == countyId);

                return data.Cast<Data>().ToList();
            }
            else if (chapter == typeof(AverageNetSalary))
            {
                var repo = new Repository<AverageNetSalary>(App.AsyncDb);
                var data = await repo.GetAll(x => x.CountyId == countyId);

                return data.Cast<Data>().ToList();
            }
            else
            {
                return null;
            }
        }
    }
}