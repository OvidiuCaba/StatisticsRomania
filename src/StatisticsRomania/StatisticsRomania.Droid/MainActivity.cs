using System;
using System.IO;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using StatisticsRomania.Repository;

// ca-app-pub-4024802291999001~8046647573 - app ID
// ca-app-pub-4024802291999001/1426356760 - ad unit ID

namespace StatisticsRomania.Droid
{
    [Activity(Label = "Statistici Romania", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density, Theme = "@style/MainTheme")]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;

            //Android.Gms.Ads.MobileAds.Initialize(ApplicationContext, "ca-app-pub-4024802291999001~8046647573");

            //LoadApplication(new App());
        }

        protected void HandleUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            //Console.WriteLine("========= MyHandler caught : " + e.Message);
        }
    }
}