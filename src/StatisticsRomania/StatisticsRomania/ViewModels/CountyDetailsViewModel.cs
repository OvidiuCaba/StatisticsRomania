using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Lib;
using StatisticsRomania.Repository;

namespace StatisticsRomania.ViewModels
{
    public class CountyDetailsViewModel : BaseViewModel
    {
        private readonly IRepository<County> _countyRepository;
        private readonly ObservableCollection<Data> _chapterData;

        public Dictionary<string, int> CountyList { get; set; }

        public ObservableCollection<Data> ChapterData
        {
            get { return _chapterData; }
        }

        public CountyDetailsViewModel()
        {
            _countyRepository = new Repository<County>(App.AsyncDb);
            _chapterData = new ObservableCollection<Data>();
        }

        public async Task GetCounties()
        {
            CountyList = (await _countyRepository.GetAll()).ToDictionary(x => x.Name, x => x.Id);
        }

        public async Task GetChapterData(int countyId, string chapter)
        {
            ChapterData.Clear();

            if (!ChapterList.ContainsKey(chapter) || countyId < 1)
                return;

            var data = await DataProvider.GetData(countyId, ChapterList[chapter]);

            foreach (var item in data)
            {
                ChapterData.Add(item);
            }
        }
    }
}