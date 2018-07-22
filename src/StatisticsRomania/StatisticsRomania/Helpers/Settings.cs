using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;

namespace StatisticsRomania.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        private const string LastCounty1Key = "last_county1";
        private const string LastCounty2Key = "last_county2";
        private const string LastSelectedChapterKey = "last_selected_chapter";
        private const string LastSelectedStandingsChapterKey = "last_selected_standings_chapter";
        private const string LastSelectedYearKey = "last_year";
        private const string LastSelectedMonthKey = "last_month";

        public static int County1
        {
            get { return AppSettings.GetValueOrDefault<int>(LastCounty1Key); }
            set { AppSettings.AddOrUpdateValue<int>(LastCounty1Key, value); }
        }

        public static int County2
        {
            get { return AppSettings.GetValueOrDefault<int>(LastCounty2Key); }
            set { AppSettings.AddOrUpdateValue<int>(LastCounty2Key, value); }
        }

        public static int Chapter
        {
            get { return AppSettings.GetValueOrDefault<int>(LastSelectedChapterKey); }
            set { AppSettings.AddOrUpdateValue<int>(LastSelectedChapterKey, value); }
        }

        public static int StandingsChapter
        {
            get { return AppSettings.GetValueOrDefault<int>(LastSelectedStandingsChapterKey); }
            set { AppSettings.AddOrUpdateValue<int>(LastSelectedStandingsChapterKey, value); }
        }

        public static int Year
        {
            get { return AppSettings.GetValueOrDefault<int>(LastSelectedYearKey, DateTime.Now.Year); }
            set { AppSettings.AddOrUpdateValue<int>(LastSelectedYearKey, value); }
        }

        public static int Month
        {
            get { return AppSettings.GetValueOrDefault<int>(LastSelectedMonthKey, DateTime.Now.Month); }
            set { AppSettings.AddOrUpdateValue<int>(LastSelectedMonthKey, value); }
        }

    }
}