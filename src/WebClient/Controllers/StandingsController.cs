using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.Lib;
using StatisticsRomania.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    public class StandingsController : BaseController
    {
        [HttpGet("[action]")]
        public async Task<object> GetStandings(string chapter, int year, int yearFraction)
        {
            if (!ChapterList.ContainsKey(chapter))
                return null;

            var valueColumnCaption = UnitOfMeasureList[chapter];

            var data = await CountyStandingsProvider.GetData(ChapterList[chapter], year, yearFraction);

            return new StandingsDto { ValueColumnCaption = valueColumnCaption, Data = data };
        }
    }

    public class StandingsDto
    {
        public string ValueColumnCaption { get; set; }
        public List<StandingItem> Data { get; set; }
    }
}