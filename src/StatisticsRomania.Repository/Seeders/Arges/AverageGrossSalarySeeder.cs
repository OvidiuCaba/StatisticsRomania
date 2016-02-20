using System.Collections.Generic;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders.Arges
{
    /// <summary>
    /// Note: formula for generating seeding:
    /// ="new AverageGrossSalary { Subchapter = ""Total judet"", CountyId = CountryIds.Arges, Year = 2014, YearFraction = " & B2 & ", Value = " & B3 & "},"
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
                                        CountyId = CountryIds.Arges,
                                        Year = 2014,
                                        YearFraction = 10,
                                        Value = 2318
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2014,
                                        YearFraction = 11,
                                        Value = 2347
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2014,
                                        YearFraction = 12,
                                        Value = 2624
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 1,
                                        Value = 2256
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 2,
                                        Value = 2275
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 3,
                                        Value = 2350
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 4,
                                        Value = 2541
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 5,
                                        Value = 2493
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 6,
                                        Value = 2713
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 7,
                                        Value = 2666
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 8,
                                        Value = 2403
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 9,
                                        Value = 2409
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = CountryIds.Arges,
                                        Year = 2015,
                                        YearFraction = 10,
                                        Value = 2436
                                    },
                            };
            return items;
        }
    }
}