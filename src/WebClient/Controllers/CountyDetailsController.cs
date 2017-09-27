using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.Lib;
using StatisticsRomania.Repository.Seeders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    public class CountyDetailsController : BaseController
    {
        [HttpGet("[action]")]
        public List<CountyDto> GetCounties()
        {
            var counties = CountiesSeeder.GetData().Select(x => new CountyDto { Id = x.Id, Name = x.Name }).ToList();

            return counties;
        }

        [HttpGet("[action]")]
        public async Task<object> GetCountyDetails(int countyId, int countyId2, string chapter, bool needToProcessAllYear)
        {
            if (!ChapterList.ContainsKey(chapter) || countyId < 1)
                return null;

            var countyAbbreviations = new Dictionary<string, string>()
                                       {
                                           {"Alba", "AB"},
                                           {"Arad", "AR"},
                                           {"Arges", "AG"},
                                           {"Bacau", "BC"},
                                           {"Bihor", "BH"},
                                           {"Bistrita-Nasaud", "BN"},
                                           {"Botosani", "BT"},
                                           {"Brasov", "BV"},
                                           {"Braila", "BR"},
                                           {"Buzau", "BZ"},
                                           {"Caras-Severin", "CS"},
                                           {"Calarasi", "CL"},
                                           {"Cluj", "CJ"},
                                           {"Constanta", "CT"},
                                           {"Covasna", "CV"},
                                           {"Dambovita", "DB"},
                                           {"Dolj", "DJ"},
                                           {"Galati", "GL"},
                                           {"Giurgiu", "GR"},
                                           {"Gorj", "GJ"},
                                           {"Harghita", "HR"},
                                           {"Hunedoara", "HD"},
                                           {"Ialomita", "IL"},
                                           {"Iasi", "IS"},
                                           {"Ilfov", "IF"},
                                           {"Maramures", "MM"},
                                           {"Mehedinti", "MH"},
                                           {"Mures", "MS"},
                                           {"Neamt", "NT"},
                                           {"Olt", "OT"},
                                           {"Prahova", "PH"},
                                           {"Satu Mare", "SM"},
                                           {"Salaj", "SJ"},
                                           {"Sibiu", "SB"},
                                           {"Suceava", "SV"},
                                           {"Teleorman", "TR"},
                                           {"Timis", "TM"},
                                           {"Tulcea", "TL"},
                                           {"Vaslui", "VS"},
                                           {"Valcea", "VL"},
                                           {"Vrancea", "VN"},
                                           {"Bucuresti", "B"},
                                       };

            var countyList = CountiesSeeder.GetData().ToDictionary(x => x.Name, x => x.Id);

            var valueColumnCaption = string.Format("{0} {1}", UnitOfMeasureList[chapter], countyAbbreviations[countyList.First(x => x.Value == countyId).Key]);

            var data = (await CountyDetailsProvider.GetData(countyId, ChapterList[chapter], needToProcessAllYear))
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.YearFraction)
                .Select(x => new DataDto
                {
                    Id = x.Id,
                    CountyId = x.CountyId,
                    Subchapter = x.Subchapter,
                    Year = x.Year,
                    YearFraction = x.YearFraction,
                    Value = x.Value,
                    Value2 = x.Value2
                })
                .ToList();

            string value2ColumnCaption = null;
            bool value2ColumnVisibility;    // kept for compatibility with the mobile VM in case I ever want to merge the duplicated logic

            if (countyId2 >= 1 && countyId != countyId2)
            {
                var data2 = await CountyDetailsProvider.GetData(countyId2, ChapterList[chapter], needToProcessAllYear);
                foreach (var item2 in data2)
                {
                    var item = data.FirstOrDefault(x => x.Year == item2.Year && x.YearFraction == item2.YearFraction);
                    if (item != null)
                    {
                        item.Value2 = item2.Value;
                    }
                }

                value2ColumnCaption = string.Format("{0} {1}", UnitOfMeasureList[chapter], countyAbbreviations[countyList.First(x => x.Value == countyId2).Key]);

                value2ColumnVisibility = true;
            }
            else
            {
                value2ColumnVisibility = false;
            }

            return new { ValueColumnCaption = valueColumnCaption, Value2ColumnCaption = value2ColumnCaption, Data = data };
        }
    }

    public class CountyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class DataDto
    {
        public int Id { get; set; }

        public int? CountyId { get; set; }

        public string Subchapter { get; set; }

        public int Year { get; set; }

        // Month, trimester or semester
        public int YearFraction { get; set; }

        public float Value { get; set; }

        public float Value2 { get; set; }
    }
}