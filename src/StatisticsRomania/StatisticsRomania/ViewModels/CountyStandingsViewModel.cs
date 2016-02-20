using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsRomania.ViewModels
{
    public class CountyStandingsViewModel : BaseViewModel
    {
        internal Task GetStandings(string selectedChapter)
        {
            //throw new NotImplementedException();
            return Task.FromResult(0);
        }

        public object Standings { get; set; }
    }
}