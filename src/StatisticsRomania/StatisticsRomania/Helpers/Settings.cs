using Plugin.Settings;
using Plugin.Settings.Abstractions;

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

        public static string Chapter
        {
            get { return AppSettings.GetValueOrDefault<string>(LastSelectedChapterKey); }
            set { AppSettings.AddOrUpdateValue<string>(LastSelectedChapterKey, value); }
        }

        public static string StandingsChapter
        {
            get { return AppSettings.GetValueOrDefault<string>(LastSelectedStandingsChapterKey); }
            set { AppSettings.AddOrUpdateValue<string>(LastSelectedStandingsChapterKey, value); }
        }
    }
}