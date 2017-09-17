using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Lib;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    public class IndicatorPerformersController : BaseController
    {
        [HttpGet("[action]")]
        public Task<object> GetIndicatorPerformers()
        {
            return GetData(false);
        }

        [HttpGet("[action]")]
        public Task<object> GetIndicatorPerformersByYear()
        {
            return GetData(true);
        }

        private static List<string> GetMonths()
        {
            return new List<string> { "Ianuarie", "Februarie", "Martie", "Aprilie", "Mai", "Iunie", "Iulie", "August", "Septembrie", "Octombrie", "Noiembrie", "Decembrie" };
        }

        private IEnumerable<string> GetChapters()
        {
            return ChapterList.Where(x => !IgnoredChapters.Contains(x.Key)).Select(x => x.Key);
        }

        private async Task<(int, int)> GetLastMonthAndLastYear(string chapter)
        {
            return (await CountyDetailsProvider.GetData(1, ChapterList[chapter])).OrderByDescending(x => x.Year).ThenByDescending(x => x.YearFraction).Take(1).Select(x => (x.Year, x.YearFraction)).First();
        }

        private static IEnumerable<Performer> GetPerformers(string chapter, List<StatisticsRomania.BusinessObjects.Data> currentYearData, List<StatisticsRomania.BusinessObjects.Data> previousYearData)
        {
            return currentYearData.Join(previousYearData, x => x.CountyId, x => x.CountyId, (currentValue, previousValue) => new
                                                    {
                                                        County = currentValue.County.Name,
                                                        CurrentValue = currentValue.Value,
                                                        PreviousValue = previousValue.Value
                                                    })
                                                    .OrderByWithDirection(x => x.CurrentValue - x.PreviousValue, chapter == "Forta de munca - numar someri")
                                                    .Take(5)
                                                    .Select((x, index) => new Performer
                                                    {
                                                        Position = index + 1,
                                                        County = x.County,
                                                        OldValue = x.PreviousValue,
                                                        NewValue = x.CurrentValue,
                                                        ValueVariation = $"{(x.CurrentValue - x.PreviousValue).ToString("##,#", CultureInfo.CurrentCulture)} ({(x.CurrentValue - x.PreviousValue) * 100.0 / x.PreviousValue:F2}%)"
                                                    });
        }

        private async Task<object> GetData(bool get12Months)
        {
            var months = GetMonths();
            var performers = new List<IndicatorPerformers>();
            var chapters = GetChapters();
            foreach (var chapter in chapters)
            {
                var (lastYearAvailableData, lastMonthAvailableData) = await GetLastMonthAndLastYear(chapter);
                Func<int, int, Type, Task<List<Data>>> getData = get12Months ? (Func<int, int, Type, Task<List<Data>>>)IndicatorPerformersProvider.GetLast12MonthsData : IndicatorPerformersProvider.GetData;
                var currentYearData = await getData(lastYearAvailableData, lastMonthAvailableData, ChapterList[chapter]);
                var previousYearData = await getData(lastYearAvailableData - 1, lastMonthAvailableData, ChapterList[chapter]);
                var chapterPerformers = GetPerformers(chapter, currentYearData, previousYearData);
                var performer = new IndicatorPerformers
                {
                    Name = chapter,
                    ComparisonPeriod = $"({ months[lastMonthAvailableData - 1] } { lastYearAvailableData } vs. { lastYearAvailableData - 1})",
                    Performers = chapterPerformers
                };
                performers.Add(performer);
            }

            return performers;
        }
    }

    class IndicatorPerformers
    {
        public string Name { get; set; }
        public string ComparisonPeriod { get; set; }
        public IEnumerable<Performer> Performers { get; set; }
    }

    class Performer
    {
        public int Position { get; set; }
        public string County { get; set; }
        public float OldValue { get; set; }
        public float NewValue { get; set; }
        public string ValueVariation { get; set; }
        public bool Favourite { get; set; }
    }
}