using NUnit.Framework;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Lib;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        // TODO: International commerce should be refined because they are late with 3 months
        [Test]
        public async Task AllIndicatorsHave42CountiesForAllPeriod()
        {
            var year = 2015;    // start from 2015 because this is the first year we have data for
            var month = 1;
            var indicators = new List<Type>()
            {
                typeof(ExportFob),
                typeof(ImportCif),
                typeof(SoldFobCif),
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
            var numberOfTimesWithZeroCounties = 0;
            var indicatorIndex = 0;

            while (true)
            {
                var numberOfCounties = (await IndicatorPerformersProvider.GetData(year, month, indicators[indicatorIndex])).Count;

                TestContext.WriteLine($"Year: {year}, month: {month}, indicator: {indicators[indicatorIndex].Name}, counties: {numberOfCounties}");

                if (numberOfCounties != 0)
                {
                    if (numberOfTimesWithZeroCounties > 0)
                        Assert.Fail("Something bad happened. We have some counties for this parameters set, but we had zero counties for a previous parameters set.");
                    else
                        Assert.AreEqual(42, numberOfCounties);
                }
                else
                    numberOfTimesWithZeroCounties++;

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