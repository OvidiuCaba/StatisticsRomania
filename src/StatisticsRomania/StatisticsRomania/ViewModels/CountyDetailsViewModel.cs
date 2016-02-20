using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;

namespace StatisticsRomania.ViewModels
{
    public class CountyDetailsViewModel
    {
        private readonly IRepository<County> _chapterRepository;

        public Dictionary<string, int> CountyList { get; set; }

        public CountyDetailsViewModel()
        {
            _chapterRepository = new Repostory<County>(App.AsyncDb);
        }

        public async Task GetCounties()
        {
            CountyList = (await _chapterRepository.Get()).ToDictionary(x => x.Name, x => x.Id);
        }
    }
}