using PropertyChanged;
using StatisticsRomania.Helpers;
using StatisticsRomania.Lib;
using StatisticsRomania.Lib.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StatisticsRomania.ViewModels
{
    [ImplementPropertyChanged]
    public class CountyStandingsViewModel : BaseViewModel
    {
        private readonly ObservableCollection<StandingItem> _standings;

        public List<string> YearList { get; set; }
        public List<string> YearFractionList { get; set; }

        public ObservableCollection<StandingItem> Standings
        {
            get { return _standings; }
        }

        public int Year { get; set; }

        public int YearFraction { get; set; }

        public string ValueColumnCaption { get; set; }

        public bool HasData { get; set; }
        public bool DoesNotHaveData { get; set; }

        public int LastAvailableYear { get; set; }
        public int LastAvailableYearFraction { get; set; }

        public CountyStandingsViewModel()
        {
            _standings = new ObservableCollection<StandingItem>();

            MessagingCenter.Subscribe<PullCommand, string>(this, "DataPulled", async (sender, arg) => {
                if (arg == ChapterList[Chapter])
                {
                    var data = await CountyStandingsProvider.GetData(ChapterList[Chapter], Year, YearFraction, ChapterList[Chapter] == "Unemployed");

                    FillDataSource(data);
                }
            });
        }

        public async Task GetStandings(string chapter, int year, int yearFraction)
        {
            IsLoading = true;

            Standings.Clear();

            if (!ChapterList.ContainsKey(chapter))
                return;

            ValueColumnCaption = UnitOfMeasureList[chapter];

            var data = await CountyStandingsProvider.GetData(ChapterList[chapter], year, yearFraction, ChapterList[Chapter] == "Unemployed");

            FillDataSource(data);

            HasData = data.Count > 0;
            DoesNotHaveData = !HasData;

            if (DoesNotHaveData)
            {
                var lastData = (await CountyDetailsProvider.GetData(1, 0, ChapterList[chapter]))
                    .OrderByDescending(x => x.Year)
                    .ThenByDescending(x => x.YearFraction)
                    .FirstOrDefault();

                if (lastData == null)
                {
                    IsLoading = false;
                    return;
                }

                LastAvailableYear = lastData.Year;
                LastAvailableYearFraction = lastData.YearFraction;
            }

            IsLoading = false;

            await SyncService.PushCommand(new PullCommand(ChapterList[chapter], year));
        }

        internal void GetYears()
        {
            YearList = Enumerable.Range(2014, 3).Select(x => x.ToString()).ToList();
        }

        internal void GetYearFractions()
        {
            YearFractionList = Enumerable.Range(1, 12).Select(x => x.ToString()).ToList();
        }

        private void FillDataSource(List<StandingItem> data)
        {
            Standings.Clear();
            foreach (var item in data)
            {
                Standings.Add(item);
            }
        }
    }
}