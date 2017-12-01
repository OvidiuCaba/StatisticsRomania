using StatisticsRomania.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsRomania.Lib
{
    public static class CountyDetailsProvider
    {
        public static async Task<List<Data>> GetData(int countyId, Type chapter, bool needToProcessAllYear = false)
        {
            if (chapter == typeof(ExportFob))
            {
                return await GetData<ExportFob>(countyId, needToProcessAllYear);
            }

            if (chapter == typeof(ImportCif))
            {
                return await GetData<ImportCif>(countyId, needToProcessAllYear);
            }

            if (chapter == typeof(SoldFobCif))
            {
                return await GetData<SoldFobCif>(countyId, needToProcessAllYear);
            }

            if (chapter == typeof(AverageGrossSalary))
            {
                return await GetData<AverageGrossSalary>(countyId, needToProcessAllYear);
            }

            if (chapter == typeof(AverageNetSalary))
            {
                return await GetData<AverageNetSalary>(countyId, needToProcessAllYear);
            }

            if (chapter == typeof(NumberOfTourists))
            {
                return await GetData<NumberOfTourists>(countyId, needToProcessAllYear);
            }

            if (chapter == typeof(NumberOfNights))
            {
                return await GetData<NumberOfNights>(countyId, needToProcessAllYear);
            }

            if (chapter == typeof(NumberOfEmployees))
            {
                return await GetData<NumberOfEmployees>(countyId, needToProcessAllYear);
            }

            if (chapter == typeof(Unemployed))
            {
                return await GetData<Unemployed>(countyId, needToProcessAllYear);
            }

            return null;
        }

        // TODO: put the functionality needToProcessAllYear on mobile, too
        private static async Task<List<Data>> GetData<T>(int countyId, bool needToProcessAllYear = false)
            where T : Data, new()
        {
            var repository = RepositoryFactory.GetRepository<T>();
            var query = (await repository.GetAll(x => x.CountyId == countyId));
            if (needToProcessAllYear)
            {
                query = query.GroupBy(x => x.Year)
                    .Select(group => new T
                    {
                        CountyId = countyId,
                        Year = group.Key,
                        Value = new[] { "ExportFob", "ImportCif", "SoldFobCif", "NumberOfTourists", "NumberOfNights" }.Contains(typeof(T).Name) ? group.Sum(x => x.Value) : group.Sum(x => x.Value) / group.Count()
                    })
                    .ToList();
            }
            var data = query.Cast<Data>().ToList();
            return data;
        }
    }
}