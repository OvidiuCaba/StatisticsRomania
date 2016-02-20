using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.Lib;

namespace StatisticsRomania.ViewModels
{
    public class CountyStandingsViewModel : BaseViewModel
    {
        private readonly ObservableCollection<StandingItem> _standings;

        public List<string> YearList { get; set; }
        public List<string> YearFractionList { get; set; }

        public ObservableCollection<StandingItem> Standings
        {
            get { return _standings; }
        }

        public CountyStandingsViewModel()
        {
            _standings = new ObservableCollection<StandingItem>();
        }

        public async Task GetStandings(string chapter, int year, int yearFraction)
        {
            Standings.Clear();

            if (!ChapterList.ContainsKey(chapter))
                return;

            var data = await CountyStandingsProvider.GetData(ChapterList[chapter], year, yearFraction);

            foreach (var item in data)
            {
                Standings.Add(item);
            }
        }

        internal void GetYears()
        {
            YearList = Enumerable.Range(2014, 3).Select(x => x.ToString()).ToList();
        }

        internal void GetYearFractions()
        {
            YearFractionList = Enumerable.Range(1, 12).Select(x => x.ToString()).ToList();
        }
    }

    public class StandingItem
    {
        public int Position { get; set; }
        public string County { get; set; }
        public float Value { get; set; }
    }
}