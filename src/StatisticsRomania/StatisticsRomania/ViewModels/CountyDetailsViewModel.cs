using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Lib;
using StatisticsRomania.Repository;

namespace StatisticsRomania.ViewModels
{
    [ImplementPropertyChanged]
    public class CountyDetailsViewModel : BaseViewModel
    {
        private readonly IRepository<County> _countyRepository;
        private readonly ObservableCollection<Data> _chapterData;
        private readonly ObservableCollection<Data> _chapterDataReversed;

        public Dictionary<string, int> CountyList { get; set; }

        public ObservableCollection<Data> ChapterData
        {
            get { return _chapterData; }
        }

        public ObservableCollection<Data> ChapterDataReversed
        {
            get { return _chapterDataReversed; }
        }

        public string ValueColumnCaption { get; set; }

        public CountyDetailsViewModel()
        {
            _countyRepository = new Repository<County>(App.AsyncDb);
            _chapterData = new ObservableCollection<Data>();
            _chapterDataReversed = new ObservableCollection<Data>();
        }

        public async Task GetCounties()
        {
            CountyList = (await _countyRepository.GetAll()).ToDictionary(x => x.Name, x => x.Id);
        }

        public async Task GetChapterData(int countyId, string chapter)
        {
            ValueColumnCaption = "Lei";

            ChapterData.Clear();

            if (!ChapterList.ContainsKey(chapter) || countyId < 1)
                return;

            var data = await CountyDetailsProvider.GetData(countyId, ChapterList[chapter]);

            foreach (var item in data)
            {
                ChapterData.Add(item);
                ChapterDataReversed.Insert(0, item);
            }
        }
    }
}