using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    public class CountyDetailsController : Controller
    {
        [HttpGet("[action]")]
        public List<CountyDto> GetCounties()
        {
            var counties = CountiesSeeder.GetData().Select(x => new CountyDto { Id = x.Id, Name = x.Name }).ToList();

            return counties;
        }
    }

    public class CountyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}