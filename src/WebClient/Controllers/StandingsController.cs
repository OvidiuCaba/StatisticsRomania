using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.Lib;
using StatisticsRomania.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        [HttpPost("upload")]
        public async Task Upload(string base64, string filename)
        {
            // TODO: don't make the request if the file exists [how do we handle data update/refresh from INS? maybe we just remove all the maps or all the maps for a specific indicator]
            var needle = base64.IndexOf(',');

            if (needle > 0)
            {
                base64 = base64.Substring(needle + 1);
                var fileContent = Convert.FromBase64String(base64);
                var mapsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "maps");
                if (!Directory.Exists(mapsPath))
                {
                    Directory.CreateDirectory(mapsPath);
                }
                var mapPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "maps", filename);

                if (System.IO.File.Exists(mapPath))
                {
                    using (var img = Image.FromFile(mapPath))
                    {
                        var fileInfo = new FileInfo(mapPath);
                        // the file must be bigger than 50 kb, otherwise something might be wrong and the initial file was not saved properly
                        if (img.Width > 1000 && fileInfo.Length > 50000)
                            return;
                    }
                }

                using (var stream = new FileStream(mapPath, FileMode.Create))
                {
                    await stream.WriteAsync(fileContent, 0, fileContent.Length);
                }
            }
        }
    }

    public class StandingsDto
    {
        public string ValueColumnCaption { get; set; }
        public List<StandingItem> Data { get; set; }
    }
}