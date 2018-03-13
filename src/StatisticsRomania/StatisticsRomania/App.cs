using SQLite.Net.Async;
using StatisticsRomania.Views;
using Xamarin.Forms;

namespace StatisticsRomania
{
    // TODO: new functionalities:
    //  - La functionalitatea de "compara cu", oare se pot adauga mai multe elemente/judete de comparat, nu doar doua? Daca se poate dinamic (cate vrea utilizatorul) sau daca nu, sa zicem 7 pentru ca intr-o regiune de dezvoltare avem maxim 7 judete, si oricum 7 e "good enough". [Goada Bogdan]
    //      https://www.facebook.com/StatisticiRomaniaApp/posts/1845041702461073?comment_id=1851594968472413&notif_id=1520943160326564&notif_t=feed_comment&ref=notif
    //  - de asemenea si un titlu pe grafic nu ar strica (adica ce se prezinta/compara - turisti, someri, etc) [Goada Bogdan]
    //      https://www.facebook.com/StatisticiRomaniaApp/posts/1845041702461073?comment_id=1851594968472413&notif_id=1520943160326564&notif_t=feed_comment&ref=notif
    //  - si la grafic ar ajuta la sumele mai mari o virgula sau separare fizica la cate 3 zecimale. [Goada Bogdan]
    //      https://www.facebook.com/StatisticiRomaniaApp/posts/1845041702461073?comment_id=1851594968472413&notif_id=1520943160326564&notif_t=feed_comment&ref=notif
    /*  - de asemenea pe harta, datorita faptului ca Bucurestiul are diferente majore fata de restul judetelor culorile sunt cam toate la fel. As sugera si aici, cumva Bucurestiul sa fie "scos" din gama de culori.
     *      Argumentatie:   - Bucurestiul nu este judet, este un municipiu. Nu e corect sa calculam salariul mediu pe un judet (care are multe sate si orase mici unde salariul este mai mic, si unde multi oameni 
     *                          nici nu lucreaza traiesc din munca nefiscalizata full time) cu un oras de 6 ori mai mare ca si orasul nr 2 ca si marime si buget
                            - exporturi de asemenea, fiindca traim intr-un stat super-centralizat multe "oportunitati" sunt concentrate in Buc si de aceeasi iarasi "rupe scala"

                            fa un test .. scoate Buc din calcul (aici ma refer doar la culorile hartii) si vezi daca prin recalibrare nu se vede mai bine diferenta dintre (exemplu random) Vaslui si Alba, care altfel nu se vedea cand aveai si Buc pentru ca amandoua erau pe ceal mai deschisa culoare.
                            De obicei lumea face comparatii pe judete cu judetele din aceeasi regiune de dezvoltare, sau cu judete de acelasi "profil". Bucurestiul este un "judet" aparte, ar trebui comparat cu Budapesta, Sofia, etc. E bun sa fie la statistica ca si comparatie, dar "cam rupe scala".
            https://www.facebook.com/StatisticiRomaniaApp/posts/1845041702461073?comment_id=1851594968472413&notif_id=1520943160326564&notif_t=feed_comment&ref=notif
     */
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
