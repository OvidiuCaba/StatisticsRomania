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

    public class App : Application
    {
        public static string SqliteFilename = "database.db3";
        public static SQLiteAsyncConnection AsyncDb;

        // Note: change the values when new data is added [in the future we might automatically get the data from API, so no rush to optimize here]
        public static int LastYearAvailableData = 2015;
        public static int LastMonthAvailableData = 10;

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
