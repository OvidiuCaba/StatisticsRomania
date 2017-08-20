using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.Lib;
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
        public async Task<object> GetIndicatorPerformers()
        {
            var months = new List<string> { "Ianuarie", "Februarie", "Martie", "Aprilie", "Mai", "Iunie", "Iulie", "August", "Septembrie", "Octombrie", "Noiembrie", "Decembrie" };
            var performers = new List<IndicatorPerformers>();
            var chapters = ChapterList.Where(x => !IgnoredChapters.Contains(x.Key)).Select(x => x.Key);
            foreach (var chapter in chapters)
            {
                var (lastYearAvailableData, lastMonthAvailableData) = (await CountyDetailsProvider.GetData(1, ChapterList[chapter])).OrderByDescending(x => x.Year).ThenByDescending(x => x.YearFraction).Take(1).Select(x => (x.Year, x.YearFraction)).First();
                var standingsCurrentYearMonth = await CountyStandingsProvider.GetData(ChapterList[chapter], lastYearAvailableData, lastMonthAvailableData);
                var standingsLastYearMonth = await CountyStandingsProvider.GetData(ChapterList[chapter], lastYearAvailableData - 1, lastMonthAvailableData);
                var chapterPerformers = standingsCurrentYearMonth.Join(standingsLastYearMonth, x => x.County, x => x.County, (currentValue, previousValue) => new
                                        {
                                            County = currentValue.County,
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

        [HttpGet("[action]")]
        public async Task<object> GetIndicatorPerformersByYear()
        {
            var months = new List<string> { "Ianuarie", "Februarie", "Martie", "Aprilie", "Mai", "Iunie", "Iulie", "August", "Septembrie", "Octombrie", "Noiembrie", "Decembrie" };
            var performers = new List<IndicatorPerformers>();
            var chapters = ChapterList.Where(x => !IgnoredChapters.Contains(x.Key)).Select(x => x.Key);
            foreach (var chapter in chapters)
            {
                var (lastYearAvailableData, lastMonthAvailableData) = (await CountyDetailsProvider.GetData(1, ChapterList[chapter])).OrderByDescending(x => x.Year).ThenByDescending(x => x.YearFraction).Take(1).Select(x => (x.Year, x.YearFraction)).First();
                var standingsLastYear = await IndicatorPerformersProvider.GetLast12MonthsData(lastYearAvailableData, lastMonthAvailableData, ChapterList[chapter]);
                var standingsPreviousYear = await IndicatorPerformersProvider.GetLast12MonthsData(lastYearAvailableData - 1, lastMonthAvailableData, ChapterList[chapter]);
                var chapterPerformers = standingsLastYear.Join(standingsPreviousYear, x => x.CountyId, x => x.CountyId, (currentValue, previousValue) => new
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