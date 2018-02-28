using SQLite.Net.Async;
using StatisticsRomania.Views;
using Xamarin.Forms;

namespace StatisticsRomania
{
    // TODO: new functionalities:
    //  - export data
    //  - forecast: https://azure.microsoft.com/en-us/documentation/articles/machine-learning-what-is-machine-learning/
    //  - add data from 2014 - maybe display only last 24 months [or less? maybe configurable?]
    //  - maybe group indicators by category
    //  [cele de mai jos testate cu Nexus 5]
    //  - adaugă un "help" unde să ai explicații pentru toți termenii sau abrevierile folosite (eventual poți avea un buton de info "i" în colțul dreapta-sus al termenilor respectivi).
    //  - adaugă search la lista județelor, e destul de greu să scoll-ezi în 41 de județe. 
    //  - zoom-in și zoom-out la grafice. O difentă între 155.000 și 158.000 pare imensă când axa Y pleacă de la 150.000 și se termină la 160.000.
    //  - infografice, afiseaza mai putine informatii; eventual in setari userul sa-si aleaga judetele pe care le urmareste si doar alea sa apara:
    //          ex: 1. Bucuresti, 2. Cluj, 3. Ilfov (pana la 5), apoi 25. Bihor (cu 2 judete inainte si 2 dupa) + eventual o optiune de "show all" [sau highlight]

    public class App : Application
    {
        public static string SqliteFilename = "database.db3";
        public static SQLiteAsyncConnection AsyncDb;

        // Note: change the values when new data is added [in the future we might automatically get the data from API, so no rush to optimize here]
        public static int LastYearAvailableData = 2017;
        public static int LastMonthAvailableData = 12;

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
