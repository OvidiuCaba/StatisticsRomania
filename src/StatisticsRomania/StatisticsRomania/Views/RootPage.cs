using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StatisticsRomania.Views
{
    public class RootPage : TabbedPage
    {
        public RootPage()
        {
            this.Title = "Romania - Statistici";
            this.Children.Add(new CountyDetailsView());
            this.Children.Add(new CountyStandingsView());
        }
    }
}