using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using StatisticsRomania.Lib;
using System.Diagnostics;

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

        public string ValueColumnCaption { get; set; }

        public bool HasData { get; set; }
        public bool DoesNotHaveData { get; set; }

        public int LastAvailableYear { get; set; }
        public int LastAvailableYearFraction { get; set; }

        public CountyStandingsViewModel()
        {
            _standings = new ObservableCollection<StandingItem>();
        }

        public async Task GetStandings(string chapter, int year, int yearFraction)
        {
            Standings.Clear();

            if (!ChapterList.ContainsKey(chapter))
                return;

            ValueColumnCaption = UnitOfMeasureList[chapter];

            var data = await CountyStandingsProvider.GetData(ChapterList[chapter], year, yearFraction);

            //var y2015 = await CountyStandingsProvider.GetData(ChapterList[chapter], 2015, -1);
            //var y2016 = await CountyStandingsProvider.GetData(ChapterList[chapter], 2016, -1);

            //Debug.WriteLine($"Indicator: {ChapterList[chapter]}");
            //foreach(var item in y2015.OrderBy(x => x.County))
            //{
            //    var county = item.County;
            //    var value2016 = y2016.Find(x => x.County == county).Value;
            //    Debug.WriteLine($"{county}, 2015: {item.Value}, 2016: {value2016}, diferenta: {value2016 - item.Value} ({Math.Round((value2016 - item.Value) * 100.0 / item.Value, 2)}%)");
            //}

            foreach (var item in data)
            {
                Standings.Add(item);
            }

            HasData = data.Count > 0;
            DoesNotHaveData = !HasData;

            if (DoesNotHaveData)
            {
                var lastData = (await CountyDetailsProvider.GetData(1, ChapterList[chapter]))
                    .OrderByDescending(x => x.Year)
                    .ThenByDescending(x => x.YearFraction)
                    .FirstOrDefault();
                LastAvailableYear = lastData.Year;
                LastAvailableYearFraction = lastData.YearFraction;
            }
        }

        internal void GetYears()
        {
            YearList = Enumerable.Range(2014, 4).Select(x => x.ToString()).ToList();
        }

        internal void GetYearFractions()
        {
            YearFractionList = Enumerable.Range(0, 13).Select(x => x == 0 ? "──────" : x.ToString()).ToList();
        }
    }
}