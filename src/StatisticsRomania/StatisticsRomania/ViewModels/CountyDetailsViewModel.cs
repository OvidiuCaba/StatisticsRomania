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
using StatisticsRomania.Lib.Sync;
using Xamarin.Forms;
using StatisticsRomania.Helpers;
using Plugin.Connectivity;

namespace StatisticsRomania.ViewModels
{
    [ImplementPropertyChanged]
    public class CountyDetailsViewModel : BaseViewModel
    {
        private readonly object sync = new object();

        private readonly IRepository<County> _countyRepository;
        private readonly ObservableCollection<Data> _chapterData;
        private readonly ObservableCollection<Data> _chapterDataReversed;
        private readonly Dictionary<string, string> _countyAbbreviations;

        public Dictionary<string, int> CountyList { get; set; }

        public ObservableCollection<Data> ChapterData
        {
            get { return _chapterData; }
        }

        public ObservableCollection<Data> ChapterDataReversed
        {
            get { return _chapterDataReversed; }
        }

        public Dictionary<string, string> CountyAbbreviations
        {
            get { return _countyAbbreviations; }
        }

        public string ValueColumnCaption { get; set; }

        public string Value2ColumnCaption { get; set; }

        public bool Value2ColumnVisibility { get; set; }

        public bool IsMessageVisible { get; set; }

        public bool IsDataVisible { get; set; }

        public string Message { get; set; }

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
                                           {"Bistrita Nasaud", "BN"},
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

            MessagingCenter.Subscribe<PullCommand, string>(this, "DataPulled", async (sender, arg) => {
                if (arg == ChapterList[Chapter])
                {
                    var data = await CountyDetailsProvider.GetData(Settings.County1, Settings.County2, ChapterList[Settings.Chapter]);
                    FillDataSources(data);
                    Message = string.Empty;
                    IsMessageVisible = false;
                    IsDataVisible = true;
                }
            });
        }

        public async Task GetCounties()
        {
            CountyList = (await _countyRepository.GetAll()).ToDictionary(x => x.Name, x => x.Id);
            CountyList.Add("──────", 0);
        }

        public async Task GetChapterData(int countyId, int countyId2, string chapter)
        {
            IsLoading = true;

            ChapterData.Clear();
            ChapterDataReversed.Clear();

            if (!ChapterList.ContainsKey(chapter) || countyId < 1)
                return;

            ValueColumnCaption = string.Format("{0} {1}", UnitOfMeasureList[chapter], CountyAbbreviations[CountyList.First(x => x.Value == countyId).Key]);

            var data = await CountyDetailsProvider.GetData(countyId, countyId2, ChapterList[chapter]);

            if (!data.Any())
            {
                Message = CrossConnectivity.Current.IsConnected ? "Asteptati pana cand datele sunt preluate de pe server." : "Conectati-va la Internet si incercati din nou.";
            }
            else
            {
                Message = string.Empty;
            }
            IsMessageVisible = Message.Length > 0;
            IsDataVisible = !IsMessageVisible;

            var isCounty2Set = countyId2 >= 1 && countyId != countyId2;
            Value2ColumnCaption = isCounty2Set ? string.Format("{0} {1}", UnitOfMeasureList[chapter], CountyAbbreviations[CountyList.First(x => x.Value == countyId2).Key]) : string.Empty;
            Value2ColumnVisibility = isCounty2Set;

            FillDataSources(data);

            IsLoading = false;

            await SyncService.PushCommand(new PullCommand(ChapterList[chapter], DateTime.Now.Year));
            await SyncService.PushCommand(new PullCommand(ChapterList[chapter], DateTime.Now.Year - 1));
        }

        private void FillDataSources(List<Data> data)
        {
            foreach (var item in data)
            {
                lock (sync)
                {
                    if (!ChapterData.Any(x => x.Year == item.Year && x.YearFraction == item.YearFraction))
                    {
                        var numberOfSmallerItems = ChapterData.Count(x => x.Year < item.Year || (x.Year == item.Year && x.YearFraction < item.YearFraction));
                        var numberOfBiggerItems = ChapterData.Count(x => x.Year > item.Year || (x.Year == item.Year && x.YearFraction > item.YearFraction));
                        ChapterData.Insert(numberOfSmallerItems, item);            // TODO: check this - ascending order
                        ChapterDataReversed.Insert(numberOfBiggerItems, item);    // TODO: check this - descending order
                    }
                }
            }
        }
    }
}