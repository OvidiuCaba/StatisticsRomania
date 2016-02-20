using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
        }

        public Task GetStandings(string selectedChapter)
        {
            return Task.FromResult(0);
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