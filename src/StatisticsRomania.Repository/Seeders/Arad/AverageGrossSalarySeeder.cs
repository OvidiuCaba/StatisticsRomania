using System.Collections.Generic;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders.Arad
{
    /// <summary>
    /// Note: formula for generating seeding:
    /// ="new AverageGrossSalary { Subchapter = ""Total judet"", CountyId = CountryIds.Arad, Year = 2014, YearFraction = " & B2 & ", Value = " & B3 & "},"
    /// </summary>
    internal static class AverageGrossSalarySeeder
    {
        internal static List<AverageGrossSalary> GetData()
        {
            var items = new List<AverageGrossSalary>
                            {
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2014,
                                        YearFraction = 10,
                                        Value = 2128
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2014,
                                        YearFraction = 11,
                                        Value = 2202
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2014,
                                        YearFraction = 12,
                                        Value = 2275
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 1,
                                        Value = 2138
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 2,
                                        Value = 2152
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 3,
                                        Value = 2217
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 4,
                                        Value = 2201
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 5,
                                        Value = 2162
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 6,
                                        Value = 2256
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 7,
                                        Value = 2337
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 8,
                                        Value = 2210
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 9,
                                        Value = 2290
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arad,
                                        Year = 2015,
                                        YearFraction = 10,
                                        Value = 2328
                                    },
                            };
            return items;
        }
    }
}