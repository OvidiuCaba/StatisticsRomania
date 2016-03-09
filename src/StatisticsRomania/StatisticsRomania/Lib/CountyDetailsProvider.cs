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
                return await GetData<AverageGrossSalary>(countyId);
            }

            if (chapter == typeof(AverageNetSalary))
            {
                return await GetData<AverageNetSalary>(countyId);
            }

            if (chapter == typeof(NumberOfTourists))
            {
                return await GetData<NumberOfTourists>(countyId);
            }

            if (chapter == typeof(NumberOfNights))
            {
                return await GetData<NumberOfNights>(countyId);
            }

            if (chapter == typeof(NumberOfEmployees))
            {
                return await GetData<NumberOfEmployees>(countyId);
            }

            if (chapter == typeof(Unemployed))
            {
                return await GetData<Unemployed>(countyId);
            }

            return null;
        }

        private static async Task<List<Data>> GetData<T>(int countyId)
            where T : Data, new()
        {
            var repo = new Repository<T>(App.AsyncDb);
            var data = (await repo.GetAll(x => x.CountyId == countyId))
                .Cast<Data>().ToList();
            return data;
        }
    }
}