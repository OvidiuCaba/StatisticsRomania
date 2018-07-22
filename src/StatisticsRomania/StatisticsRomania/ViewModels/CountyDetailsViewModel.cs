using PropertyChanged;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Lib;
using StatisticsRomania.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StatisticsRomania.ViewModels
{
    [ImplementPropertyChanged]
    public class CountyDetailsViewModel : BaseViewModel
    {
        private readonly IRepository<County> _countyRepository;
        private readonly ObservableCollection<Data> _chapterData;
        private readonly ObservableCollection<Data> _chapterDataReversed;
        private readonly Dictionary<string, string> _countyAbbreviations;

        public Dictionary<string, int> CountyList { get; set; }

        public ObservableCollection<Data> ChapterData
        {
            get { return _chapterData; }
        }

        public Dictionary<string, string> CountyAbbreviations
        {
            get { return _countyAbbreviations; }
        }

        public string ValueColumnCaption { get; set; }

        public string Value2ColumnCaption { get; set; }

        public bool Value2ColumnVisibility { get; set; }

        public bool HasData { get; set; }

        public CountyDetailsViewModel()
        {
            _countyRepository = new Repository<County>(App.AsyncDb);
            _chapterData = new ObservableCollection<Data>();
            _chapterDataReversed = new ObservableCollection<Data>();

            _countyAbbreviations = new Dictionary<string, string>()
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
        }

        public async Task GetCounties()
        {
            CountyList = (await _countyRepository.GetAll()).ToDictionary(x => x.Name, x => x.Id);
        }

        public async Task GetChapterData(int countyId, int countyId2, string chapter)
        {
            if (!ChapterList.ContainsKey(chapter) || countyId < 1)
                return;

            ValueColumnCaption = string.Format("{0} {1}", UnitOfMeasureList[chapter], CountyAbbreviations[CountyList.First(x => x.Value == countyId).Key]);

            var internetData = await InternetDataProvider.GetCountyDetailsFromCacheOrInternet(countyId, countyId2, chapter);

            var data = internetData == null ? await CountyDetailsProvider.GetData(countyId, ChapterList[chapter]) : internetData.Data.Cast<Data>().ToList();

            HasData = data.Count > 0;

            if (countyId2 >= 1 && countyId != countyId2)
            {
                if (internetData == null)
                {
                    var data2 = await CountyDetailsProvider.GetData(countyId2, ChapterList[chapter]);
                    foreach (var item2 in data2)
                    {
                        var item = data.FirstOrDefault(x => x.Year == item2.Year && x.YearFraction == item2.YearFraction);
                        if (item != null)
                        {
                            item.Value2 = item2.Value;
                        }
                    }
                }

                Value2ColumnCaption = string.Format("{0} {1}", UnitOfMeasureList[chapter], CountyAbbreviations[CountyList.First(x => x.Value == countyId2).Key]);

                Value2ColumnVisibility = true;
            }
            else
            {
                Value2ColumnVisibility = false;
            }

            ChapterData.Clear();

            foreach (var item in data)
            {
                ChapterData.Add(item);
            }
        }
    }
}