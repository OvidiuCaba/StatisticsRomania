using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.Lib;
using StatisticsRomania.BusinessObjects;

// TODO: To be deleted

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        private const string Exporturi = "Comert international - exporturi FOB";
        private const string Importuri = "Comert international - importuri CIF";
        private const string Sold = "Comert international - sold FOB/CIF";
        private const string EfectivulSalariatilor = "Forta de munca - efectiv salariati";
        private const string CastigulSalarialMediuBrut = "Forta de munca - salariu mediu brut";
        private const string CastigulSalarialMediuNet = "Forta de munca - salariu mediu net";
        private const string NumarulSomerilor = "Forta de munca - numar someri";
        private const string InnoptariInStructurileDePrimireTuristica = "Turism - innoptari";
        private const string SosiriInStructurileDePrimireTuristica = "Turism - numar turisti";

        Dictionary<string, Type> ChapterList = new Dictionary<string, Type>()
            {
                { Exporturi, typeof(ExportFob) },
                { Importuri, typeof(ImportCif) },
                { Sold, typeof(SoldFobCif) },
                { EfectivulSalariatilor, typeof(NumberOfEmployees) },
                { CastigulSalarialMediuBrut, typeof(AverageGrossSalary) },
                { CastigulSalarialMediuNet, typeof(AverageNetSalary) },
                { NumarulSomerilor, typeof(Unemployed) },
                { InnoptariInStructurileDePrimireTuristica, typeof(NumberOfNights) },
                { SosiriInStructurileDePrimireTuristica, typeof(NumberOfTourists) },
            };

        public string ValueColumnCaption { get; set; }

        Dictionary<string, string> UnitOfMeasureList = new Dictionary<string, string>()
            {
                { Exporturi, "Mii Euro" },
                { Importuri, "Mii Euro" },
                { Sold, "Mii Euro" },
                { EfectivulSalariatilor, "Persoane" },
                { CastigulSalarialMediuBrut, "Lei" },
                { CastigulSalarialMediuNet, "Lei" },
                { NumarulSomerilor, "Persoane" },
                { InnoptariInStructurileDePrimireTuristica, "Innoptari" },
                { SosiriInStructurileDePrimireTuristica, "Persoane" },
            };

        [HttpGet("[action]")]
        public async Task<object> GetStandings(string chapter, int year, int yearFraction)
        {
            try
            {
                if (!ChapterList.ContainsKey(chapter))
                    return null;

                ValueColumnCaption = UnitOfMeasureList[chapter];

                var data = await CountyStandingsProvider.GetData(ChapterList[chapter], year, yearFraction);

                return new { ValueColumnCaption = ValueColumnCaption, Data = data };
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
