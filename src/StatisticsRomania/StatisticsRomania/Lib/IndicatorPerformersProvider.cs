using StatisticsRomania.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsRomania.Lib
{
    public static class IndicatorPerformersProvider
    {
        public static async Task<List<Data>> GetLast12MonthsData(int year, int yearFraction, Type chapter)
        {
            if (chapter == typeof(ExportFob))
            {
                return await GetData<ExportFob>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(ImportCif))
            {
                return await GetData<ImportCif>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(SoldFobCif))
            {
                return await GetData<SoldFobCif>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(AverageGrossSalary))
            {
                return await GetData<AverageGrossSalary>(year, yearFraction);
            }

            if (chapter == typeof(AverageNetSalary))
            {
                return await GetData<AverageNetSalary>(year, yearFraction);
            }

            if (chapter == typeof(NumberOfTourists))
            {
                return await GetData<NumberOfTourists>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(NumberOfNights))
            {
                return await GetData<NumberOfNights>(year, yearFraction, isSum: true);
            }

            if (chapter == typeof(NumberOfEmployees))
            {
                return await GetData<NumberOfEmployees>(year, yearFraction);
            }

            if (chapter == typeof(Unemployed))
            {
                return await GetData<Unemployed>(year, yearFraction);
            }

            return null;
        }

        private static async Task<List<Data>> GetData<T>(int year, int yearFraction, bool isSum = false)
            where T : Data, new()
        {
            var repository = RepositoryFactory.GetRepository<T>();
            var query = (await repository.GetAll(x => (x.Year == year && x.YearFraction <= yearFraction) || (x.Year == year - 1 && x.YearFraction > yearFraction)));
            var data = query.Cast<Data>().ToList();

            foreach (var item in data)
            {
                await repository.GetChild((T)item, x => x.County);
            }

            data = data.GroupBy(x => x.CountyId)
                .Select(x => new T
                {
                    CountyId = x.Key,
                    County = data.First(y => y.CountyId == x.Key).County,
                    Value = (float)Math.Round(x.Sum(y => y.Value) / (isSum ? 1 : x.Count()))
                })
                .Cast<Data>()
                .ToList();

            return data;
        }
    }
}