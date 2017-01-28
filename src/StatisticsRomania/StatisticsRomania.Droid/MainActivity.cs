using System;
using System.IO;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using StatisticsRomania.Repository;
using Microsoft.Azure.Mobile;

namespace StatisticsRomania.Droid
{
    [Activity(Label = "Statistici Romania", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            MobileCenter.Configure("34727667-015a-4fd6-ae0b-99a522146681");
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            DevExpress.Mobile.Forms.Init();
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();

            var database = new Database
            {
                SqlitePlatform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(),
                Path = GetPath()
            };
            database.Initialize();

            App.AsyncDb = database.AsyncDb;

            LoadApplication(new App());
        }

        private static string GetPath()
        {
            var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(libraryPath, App.SqliteFilename);
        }
    }
}