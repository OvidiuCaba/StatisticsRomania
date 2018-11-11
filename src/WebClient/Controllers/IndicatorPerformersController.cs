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
        public Task<object> GetIndicatorPerformers(string favouriteCounties)
        {
            return GetData(false, favouriteCounties);
        }

        [HttpGet("[action]")]
        public Task<object> GetIndicatorPerformersByYear(string favouriteCounties)
        {
            return GetData(true, favouriteCounties);
        }

        private async Task<object> GetData(bool get12Months, string favouriteCounties)
        {
            favouriteCounties = favouriteCounties?.Replace("Satu Mare", "SatuMare");
            var favouriteCountiesIds = string.IsNullOrWhiteSpace(favouriteCounties) ? new List<int>() : favouriteCounties.Split(' ').Select(x => CountryIds.Counties[x.Replace("-", string.Empty)]);
            var months = GetMonths();
            var performers = new List<IndicatorPerformers>();
            var chapters = GetChapters();
            foreach (var chapter in chapters)
            {
                var (lastYearAvailableData, lastMonthAvailableData) = await GetLastMonthAndLastYear(chapter);
                Func<int, int, Type, Task<List<Data>>> getData = get12Months ? (Func<int, int, Type, Task<List<Data>>>)IndicatorPerformersProvider.GetLast12MonthsData : IndicatorPerformersProvider.GetData;
                var currentYearData = await getData(lastYearAvailableData, lastMonthAvailableData, ChapterList[chapter]);
                var previousYearData = await getData(lastYearAvailableData - 1, lastMonthAvailableData, ChapterList[chapter]);
                var chapterPerformers = GetPerformers(chapter, currentYearData, previousYearData, favouriteCountiesIds);
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

        private static IEnumerable<Performer> GetPerformers(string chapter, List<Data> currentYearData, List<Data> previousYearData, IEnumerable<int> favouriteCountiesIds)
        {
            return currentYearData.Join(previousYearData, x => x.CountyId, x => x.CountyId, (currentValue, previousValue) => new
                                                    {
                                                        County = currentValue.County,
                                                        CurrentValue = currentValue.Value,
                                                        PreviousValue = previousValue.Value
                                                    })
                                                    .OrderByWithDirection(x => x.CurrentValue - x.PreviousValue,    chapter == "Forta de munca - numar someri" || 
                                                                                                                    chapter == "Populatie - decedati" || 
                                                                                                                    chapter == "Populatie - decedati sub 1 an" ||
                                                                                                                    chapter == "Populatie - divorturi")
                                                    .Select((x, index) => new { Position = index + 1, Performer = x })
                                                    .Where(x => favouriteCountiesIds.Contains(x.Performer.County.Id) || x.Position <= 5)
                                                    .Select(x => new Performer
                                                    {
                                                        Position = x.Position,
                                                        County = x.Performer.County.Name,
                                                        OldValue = x.Performer.PreviousValue,
                                                        NewValue = x.Performer.CurrentValue,
                                                        ValueVariation = $"{(x.Performer.CurrentValue - x.Performer.PreviousValue).ToString("##,#", CultureInfo.CurrentCulture)} ({(x.Performer.CurrentValue - x.Performer.PreviousValue) * 100.0 / x.Performer.PreviousValue:F2}%)"
                                                    });
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