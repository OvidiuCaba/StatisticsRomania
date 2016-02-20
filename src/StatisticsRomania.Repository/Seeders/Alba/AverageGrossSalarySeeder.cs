using System.Collections.Generic;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders.Alba
{
    /// <summary>
    /// Note: formula for generating seeding:
    /// ="new AverageGrossSalary { Subchapter = ""Total judet"", CountyId = 1, Year = 2014, YearFraction = " & B2 & ", Value = " & B3 & "},"
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
                                        CountyId = 1,
                                        Year = 2014,
                                        YearFraction = 10,
                                        Value = 1989
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2014,
                                        YearFraction = 11,
                                        Value = 1981
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2014,
                                        YearFraction = 12,
                                        Value = 2155
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 1,
                                        Value = 2042
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 2,
                                        Value = 2113
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 3,
                                        Value = 2130
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 4,
                                        Value = 2124
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 5,
                                        Value = 2118
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 6,
                                        Value = 2115
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 7,
                                        Value = 2150
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 8,
                                        Value = 2133
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 9,
                                        Value = 2163
                                    },
                                new AverageGrossSalary
                                    {
                                        Subchapter = "Total judet",
                                        CountyId = 1,
                                        Year = 2015,
                                        YearFraction = 10,
                                        Value = 2237
                                    },
                            };
        return items;
        }
    }
}