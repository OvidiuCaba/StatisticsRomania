using Microsoft.Azure.Mobile.Analytics;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StatisticsRomania.Lib.Sync
{
    public class PullCommand
    {
        private readonly string _chapter;
        private readonly int _year;

        public PullCommand(string chapter, int year)
        {
            _chapter = chapter;
            _year = year;
        }

        public async Task ExecuteAsync()
        {
            await AzureService.Initialize();
            var sw = new Stopwatch();
            sw.Start();
            await AzureService.Table.PullAsync($"{_chapter}{_year}", AzureService.Table.Where(x => x.Chapter == _chapter && x.Year == _year));
            sw.Stop();
            Analytics.TrackEvent("Pull Command Executed", new Dictionary<string, string> { { "Chapter", $"{_chapter}" }, { "Year", $"{_year}" }, { "Execution Time", $"{sw.ElapsedMilliseconds}" } });
            MessagingCenter.Send(this, "DataPulled", _chapter);
        }
    }
}