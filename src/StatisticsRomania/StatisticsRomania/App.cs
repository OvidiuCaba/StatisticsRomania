using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsRomania.Views;
using Xamarin.Forms;
using SQLite.Net.Async;

namespace StatisticsRomania
{
    // TODO: new functionalities:
    //  - export data
    //  - fix issues on rotating
    //  - forecast: https://azure.microsoft.com/en-us/documentation/articles/machine-learning-what-is-machine-learning/
    //  - add 3 pixels padding at the edge of the screen
    //  - add data from 2014 - maybe display only last 24 months [or less? maybe configurable?]
    //  - maybe group indicators by category
    //  - adaugă un "help" unde să ai explicații pentru toți termenii sau abrevierile folosite (eventual poți avea un buton de info "i" în colțul dreapta-sus al termenilor respectivi).
    //  - adaugă search la lsita județelor, e destul de greu să scoll-ezi în 41 de județe. 
    //  - zoom-in și zoom-out la grafice. O difentă între 155.000 și 158.000 pare imensă când axa Y pleacă de la 150.000 și se termină la 160.000.

    public class App : Application
    {
        public static string SqliteFilename = "database.db3";
        public static SQLiteAsyncConnection AsyncDb;

        // Note: change the values when new data is added [in the future we might automatically get the data from API, so no rush to optimize here]
        public static int LastYearAvailableData = 2016;
        public static int LastMonthAvailableData = 1;

        public App()
        {
            // The root page of your application
            MainPage = new RootPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
