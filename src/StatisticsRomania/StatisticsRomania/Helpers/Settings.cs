namespace StatisticsRomania.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static IPreferences AppSettings
        {
            get { return Preferences.Default; }
        }

        private const string LastCounty1Key = "last_county1";
        private const string LastCounty2Key = "last_county2";
        private const string LastSelectedChapterKey = "last_selected_chapter";
        private const string LastSelectedStandingsChapterKey = "last_selected_standings_chapter";
        private const string LastSelectedYearKey = "last_year";
        private const string LastSelectedMonthKey = "last_month";

        public static int County1
        {
            get { return AppSettings.Get(LastCounty1Key, 0); }
            set { AppSettings.Set(LastCounty1Key, value); }
        }

        public static int County2
        {
            get { return AppSettings.Get(LastCounty2Key, -1); }
            set { AppSettings.Set(LastCounty2Key, value); }
        }

        public static int Chapter
        {
            get { return AppSettings.Get(LastSelectedChapterKey, 0); }
            set { AppSettings.Set(LastSelectedChapterKey, value); }
        }

        public static int StandingsChapter
        {
            get { return AppSettings.Get(LastSelectedStandingsChapterKey, 0); }
            set { AppSettings.Set(LastSelectedStandingsChapterKey, value); }
        }

        public static int Year
        {
            get { return AppSettings.Get(LastSelectedYearKey, DateTime.Now.Year); }
            set { AppSettings.Set(LastSelectedYearKey, value); }
        }

        public static int Month
        {
            get { return AppSettings.Get(LastSelectedMonthKey, DateTime.Now.Month); }
            set { AppSettings.Set(LastSelectedMonthKey, value); }
        }
    }
}