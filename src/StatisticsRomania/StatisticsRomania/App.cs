using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsRomania.Views;
using Xamarin.Forms;
using SQLite.Net.Async;

namespace StatisticsRomania
{
    public class App : Application
    {
        public static string SqliteFilename = "database.db3";
        public static SQLiteAsyncConnection AsyncDb;

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
