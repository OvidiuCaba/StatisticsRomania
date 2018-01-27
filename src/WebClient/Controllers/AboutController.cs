using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.Repository.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class AboutController : Controller
    {
        private readonly IServiceProvider _serviceProvider;

        public AboutController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> Index()
        {
            var url = this.HttpContext.Request.GetDisplayUrl();
            var title = url.Substring(url.LastIndexOf('/') + 1, (url.Contains('?') ? url.IndexOf('?') : url.Length) - url.LastIndexOf('/') - 1).Replace('-', ' ').ToUpper();
            ViewData["Title"] = title;

            var description = "Vizualizare tabelara (evolutie si clasamente) si grafica a unor indicatori statistici la nivelul judetelor Romaniei. Indicatori disponibili: exporturi FOB, importuri CIF, sold FOB/CIF, efectiv salariati, salariu mediu brut, salariu mediu net, numar someri, innoptari, numar turisti.Datele sunt preluate de la Insistitutul National de Statistica.";
            var image = "";

            var query = this.HttpContext.Request.Query;
            var isUrlShared = query.ContainsKey("share") && query["share"] == "true";

            if(isUrlShared)
            {
                description = await GetDescription() ?? description;
                image = GetImage();
            }

            ViewData["Description"] = description;
            ViewData["Image"] = image;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private Task<string> GetDescription()
        {
            var page = this.HttpContext.Request.Path.ToString().Substring(1);

            switch(page)
            {
                case "performerii-lunii":
                    return GetDescriptionForIndicatorPerformers();
                case "statistici-judetene":
                    return GetDescriptionForCountyDetails();
                case "clasamente":
                    return GetDescriptionForStandings();
            }

            return null;
        }

        private async Task<string> GetDescriptionForIndicatorPerformers()
        {
            var query = this.HttpContext.Request.Query;

            var dataProviderController = _serviceProvider.GetService(typeof(IndicatorPerformersController)) as IndicatorPerformersController;

            Func<string, Task<object>> getData = favouriteCountiesParam => (query["analysis"] == "monthly") ? dataProviderController.GetIndicatorPerformers(favouriteCountiesParam) : dataProviderController.GetIndicatorPerformersByYear(favouriteCountiesParam);

            var data = await getData(query["favouriteCounties"]) as List<IndicatorPerformers>;

            var favouriteCounties = query.ContainsKey("favouriteCounties") && !string.IsNullOrWhiteSpace(query["favouriteCounties"].ToString()) ? query["favouriteCounties"].ToString().Split(' ').ToList() : new List<string>();

            var pointOfInterests = data
                                        .Select(x => new { x.Name, Performers = x.Performers.Where(p => favouriteCounties.Any() ? favouriteCounties.Contains(p.County) : true) })
                                        .Select(x => new { x.Name, Performer = x.Performers.OrderBy(p => p.Position).First() })
                                        .OrderBy(x => x.Performer.Position)
                                        .Take(2);

            var description = pointOfInterests.Select(x => $"Pentru indicatorul {x.Name}, judetul {x.Performer.County} a avut in ultimul an o evolutie de {x.Performer.ValueVariation} fata de anul precedent, plasandu-se pe locul {x.Performer.Position} in topul judetelor.")
                                              .Aggregate((c, n) => c + Environment.NewLine + n);

            return description;
        }

        private async Task<string> GetDescriptionForCountyDetails()
        {
            var query = this.HttpContext.Request.Query;

            var dataProviderController = _serviceProvider.GetService(typeof(CountyDetailsController)) as CountyDetailsController;

            Func<int, int, string, bool, Task<object>> getData = (countyId1Param, countyId2Param, chapterParam, needToProcessAllYearParam) => dataProviderController.GetCountyDetails(countyId1Param, countyId2Param, chapterParam, needToProcessAllYearParam);

            var needToProcessAllYear = bool.Parse(query["needToProcessAllYear"]);
            var countyId1 = int.Parse(query["countyId1"]);
            var countyId2 = int.Parse(query["countyId2"]);
            var data = await getData(countyId1, countyId2, query["chapter"], needToProcessAllYear) as CountyDetailsDto;

            var pointOfInterests = data.Data.OrderByDescending(x => x.Year)
                                            .ThenByDescending(x => x.YearFraction)
                                            .First();

            var countyList = CountiesSeeder.GetData().ToDictionary(x => x.Name, x => x.Id);

            var description = $"In anul {pointOfInterests.Year}{(needToProcessAllYear ? string.Empty : $", luna {pointOfInterests.YearFraction}")}, pentru judetul {countyList.First(x => x.Value == countyId1).Key}, indicatorul statistic '{query["chapter"]}' a avut valoarea {pointOfInterests.Value}.";
            if (countyId2 > 0)
                description += $" Pentru judetul {countyList.First(x => x.Value == countyId2).Key}, acelasi indicator a avut valoarea {pointOfInterests.Value2}.";

            return description;
        }

        private async Task<string> GetDescriptionForStandings()
        {
            var query = this.HttpContext.Request.Query;

            var dataProviderController = _serviceProvider.GetService(typeof(StandingsController)) as StandingsController;

            Func<string, int, int, Task<object>> getData = (chapter, yearParam, yearFractionParam) => dataProviderController.GetStandings(chapter, yearParam, yearFractionParam);

            var year = int.Parse(query["year"]);
            var yearFraction = int.Parse(query["yearFraction"]);
            var data = await getData(query["chapter"], year, yearFraction) as StandingsDto;

            var favouriteCounties = query.ContainsKey("favouriteCounties") ? query["favouriteCounties"].ToString().Split(' ').ToList() : new List<string>();

            var pointOfInterests = data.Data
                                            .Where((x, index) => favouriteCounties.Contains(x.County) || index < 3)
                                            .Take(5);

            var partialStandings = pointOfInterests.Select(x => $"{x.Position}. {x.County}: {x.Value}")
                                                   .Aggregate((c, n) => c + "; " + n);

            var description = $"Pentru indicatorul statistic '{query["chapter"]}', pentru anul {year}{(yearFraction == -1 ? string.Empty : $", luna {yearFraction}")}, clasamentul pe judete arata astfel: {partialStandings}.";

            return description;
        }

        private string GetImage()
        {
            var page = this.HttpContext.Request.Path.ToString().Substring(1);

            switch (page)
            {
                case "performerii-lunii":
                case "statistici-judetene":
                    return string.Empty;
                case "clasamente":
                    return GetImageForStandings();
            }

            return string.Empty;
        }

        // TODO: set dimensions and set it in the image meta tag to display the image correctly at the 1st share
        private string GetImageForStandings()
        {
            var query = this.HttpContext.Request.Query;

            return $"{this.Request.Scheme}://{this.HttpContext.Request.Host}//maps//{query["chapter"].ToString().Replace(" ", string.Empty)}-{query["year"]}{query["yearFraction"]}.jpg";
        }
    }
}