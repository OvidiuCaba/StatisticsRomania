using NUnit.Framework;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Lib;
using StatisticsRomania.Repository.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AAA()
        {
            var months = new List<string> { "Ianuarie", "Februarie", "Martie", "Aprilie", "Mai", "Iunie", "Iulie", "August", "Septembrie", "Octombrie", "Noiembrie", "Decembrie" };
            var header = "Judete";
            var counties = new Dictionary<string, List<(int, int, float)>>();
            for (var year = 2015; year <= 2023; year++)
            {
                for (var month = 1; month <= 12; month++)
                {
                    try
                    {
                        header += $";{year} {months[month - 1]}";
                        //var data = await IndicatorPerformersProvider.GetLast12MonthsData(year, month, typeof(ExportFob));
                        var data = await IndicatorPerformersProvider.GetLast12MonthsData(year, month, typeof(NumberOfTourists));
                        data.ForEach(d =>
                        {
                            if (!counties.ContainsKey(d.County.Name))
                                counties.Add(d.County.Name, new List<(int, int, float)> { (year, month, d.Value) });
                            else
                                counties[d.County.Name].Add((year, month, d.Value));
                        });
                    }
                    catch { }
                }
            }

            var countiesRows = counties.Select(x => $"{x.Key};{string.Join(';', x.Value.Select(v => $"{v.Item3}"))}");
            var text = header + Environment.NewLine + string.Join(Environment.NewLine, countiesRows);
        }

//        Super graficul, felicitari!
//Oare se poate face unul la fel, dar sa fie in functie de un raport al numarului de turisti fa?? de populatia judetului vizitat?

//Nu cred ca e acelasi lucru sa ai la o populatie de 300.000 sa zicem 500.000 turisti, sau la 2 milioane locuitori sa ai 500.000 turisti.

//            Sursa: https://www.skyscrapercity.com/threads/oradea-caff%C3%A8.1724378/page-372#post-183030126

        [Test]
        public void MakeSureCovid19SeederWorks()
        {
            var covidData = Covid19Seeder.GetData();

            Assert.Greater(covidData.Count, 0);
        }

        [Test]
        public async Task AllIndicatorsShouldHave42CountiesForAllPeriod()
        {
            var internationalCommerceIndicators = new List<Type>()
            {
                typeof(ExportFob),
                typeof(ImportCif),
                typeof(SoldFobCif),
            };

            await CheckIndicators(internationalCommerceIndicators);

            var indicatorsExceptInternationalCommerce = new List<Type>()
            {
                typeof(NumberOfEmployees),
                typeof(AverageGrossSalary),
                typeof(AverageNetSalary),
                typeof(Unemployed),
                typeof(NumberOfNights),
                typeof(NumberOfTourists),
                typeof(BornAlive),
                typeof(Deceased),
                typeof(NaturalGrowth),
                typeof(Marriages),
                typeof(Divorces),
                typeof(DeceasedUnderOneYearOld),
                typeof(BuildingPermits),
            };

            await CheckIndicators(indicatorsExceptInternationalCommerce);
        }

        private static async Task CheckIndicators(List<Type> indicators)
        {
            var year = 2015;    // start from 2015 because this is the first year we have data for
            var month = 1;
            var numberOfTimesWithZeroCounties = 0;
            var indicatorIndex = 0;

            while (true)
            {
                var numberOfCounties = (await IndicatorPerformersProvider.GetData(year, month, indicators[indicatorIndex])).Count;

                if (numberOfCounties != 42)
                    TestContext.WriteLine($"Year: {year}, month: {month}, indicator: {indicators[indicatorIndex].Name}, counties: {numberOfCounties}");

                if (numberOfCounties != 0)
                {
                    if (numberOfTimesWithZeroCounties > 0)
                        Assert.Fail("Something bad happened. We have some counties for this parameters set, but we had zero counties for a previous parameters set.");
                    else
                        Assert.AreEqual(42, numberOfCounties);
                }
                else
                {
                    numberOfTimesWithZeroCounties++;
                }

                if (numberOfTimesWithZeroCounties == indicators.Count)
                    break;

                indicatorIndex++;
                if (indicatorIndex == indicators.Count)
                {
                    indicatorIndex = 0;
                    month++;
                }

                if (month == 13)
                {
                    indicatorIndex = 0;
                    month = 1;
                    year++;
                }
            }
        }
    }
}