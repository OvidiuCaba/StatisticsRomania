extern alias DrawingCommon;

using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Lib;
using StatisticsRomania.Repository.Seeders;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    public class Covid19Controller : BaseController
    {
        [HttpGet("[action]")]
        public async Task<List<CountyCovid19>> GetCounties(int month)
        {
            var covidData = Covid19Seeder.GetData();
            var counties = CountiesSeeder.GetData().Select(x => new CountyCovid19 { Id = x.Id, Name = x.Name }).ToList();
            var repository = RepositoryFactory.GetRepository<Deceased>();
            var deceased = await repository.GetAll(x => x.Year == 2021 && (month == -1 ? true : x.YearFraction == month));

            var res = new List<CountyCovid19>();

            if (month == -1)
            {
                var lastMonthOfData = deceased.Max(x => x.YearFraction);

                res = GetCountyCovidDetails(covidData, counties, lastMonthOfData);
            }
            else
            {
                res = GetCountyCovidDetails(covidData, counties, month);
                var previousMonth = GetCountyCovidDetails(covidData, counties, month - 1);

                res.ForEach(x => x.Cases = x.Cases - previousMonth.First(pm => pm.Id == x.Id).Cases);
            }

            var gradients = GetGradients(Color.Green, Color.Red, 100);
            var min = res.Min(x => x.Cases);
            var max = res.Max(x => x.Cases);
            var dif = max - min;
            var difp = (max - min) / 100;
            var transparency = "30";

            if (difp != 0)
                res.ForEach(x => x.Color = DrawingCommon::System.Drawing.ColorTranslator.ToHtml(gradients[Math.Min((x.Cases - min) / difp, 99)]) + transparency);

            var nationalLevel = new CountyCovid19
            {
                Id = -1,
                Name = "Nivel national",
                Cases = res.Sum(x => x.Cases)
            };

            res.Insert(0, nationalLevel);

            return res;
        }

        [HttpGet("[action]")]
        public async Task<List<CovidDto>> GetCovidDetails(int countyId, int month)
        {
            var repository = RepositoryFactory.GetRepository<Deceased>();
            var maxMonthForLastYear = (await repository.GetAll(x => x.Year == 2021)).Max(x => x.YearFraction);
            var deceased = await repository.GetAll(x => (month == -1 ? x.YearFraction <= maxMonthForLastYear : x.YearFraction == month) && (countyId == -1 ? true : x.CountyId == countyId));

            var res = (from d in deceased
                       group d by d.Year into g
                       select new CovidDto { Year = g.Key, YearFraction = month, TotalDeaths = (int)g.Sum(x => x.Value) }).ToList();

            return res;
        }

        [HttpGet("[action]")]
        public async Task<int> GetMaxMonthForLastYear()
        {
            var repository = RepositoryFactory.GetRepository<Deceased>();
            return (await repository.GetAll(x => x.Year == 2021)).Max(x => x.YearFraction);
        }

        private List<Color> GetGradients(Color start, Color end, int size)
        {
            int rMax = end.R;
            int rMin = start.R;
            int gMax = end.G;
            int gMin = start.G;
            int bMax = end.B;
            int bMin = start.B;
            
            var colorList = new List<Color>();
            for (int i = 0; i < size; i++)
            {
                var rAverage = rMin + (rMax - rMin) * i / size;
                var gAverage = gMin + (gMax - gMin) * i / size;
                var bAverage = bMin + (bMax - bMin) * i / size;
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }

            return colorList;
        }

        private static List<CountyCovid19> GetCountyCovidDetails(List<Covid19> covidData, List<CountyCovid19> counties, int month)
        {
            return (from county in counties
                    join covid in covidData.Where(x => x.county_data != null && DateTime.Parse(x.reporting_date).Month == month).OrderByDescending(x => DateTime.Parse(x.reporting_date)).FirstOrDefault()?.county_data ?? new List<CountyData>()
                        on county.Name equals covid.county_name.Replace("Bistrita Nasaud", "Bistrita-Nasaud").Replace("Caras Severin", "Caras-Severin")
                        into leftJoin
                    from covid2 in leftJoin.DefaultIfEmpty()
                    select new CountyCovid19 { Id = county.Id, Name = county.Name, Cases = covid2?.total_cases ?? 0 }).ToList();
        }
    }

    public class CountyCovid19
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Cases { get; set; }

        public string Color { get; set; }
    }

    public class CovidDto
    {
        public int Year { get; set; }

        // Month, trimester or semester
        public int YearFraction { get; set; }

        public int TotalDeaths { get; set; }

        // Available only at national level
        public int? CovidDeaths { get; set; }
    }

    //public class CountyDetailsDto
    //{
    //    public string ValueColumnCaption { get; set; }
    //    public string Value2ColumnCaption { get; set; }
    //    public List<DataDto> Data { get; set; }
    //}
}