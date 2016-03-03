using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository.Seeders;

namespace StatisticsRomania.Repository
{
    public class Database
    {
        public ISQLitePlatform SqlitePlatform { get; set; }

        public string Path { get; set; }

        SQLiteConnection _db;
        public SQLiteAsyncConnection AsyncDb { get; set; }

        public void Initialize()
        {
            InitializeDatabase();
            CreateDatabase();
            SeedDatabase();
        }

        private void InitializeDatabase()
        {
            var sqLiteConnectionWithLock = new SQLiteConnectionWithLock(SqlitePlatform, new SQLiteConnectionString(Path, true));
            AsyncDb = new SQLiteAsyncConnection(() => sqLiteConnectionWithLock);
            _db = new SQLiteConnection(SqlitePlatform, Path, true);
        }

        private void CreateDatabase()
        {
            _db.CreateTable<County>();
            _db.CreateTable<AverageNetSalary>();
            _db.CreateTable<AverageGrossSalary>();
            _db.CreateTable<NumberOfTourists>();
            _db.CreateTable<NumberOfNights>();
            _db.CreateTable<NumberOfEmployees>();
        }

        private void SeedDatabase()
        {
            var counties = CountiesSeeder.GetData();
            _db.InsertOrReplaceAll(counties);

            var averageGrossSalaries = AverageGrossSalarySeeder.GetData();
            _db.DeleteAll<AverageGrossSalary>();
            _db.InsertAll(averageGrossSalaries);

            var averageNetSalaries = AverageNetSalarySeeder.GetData();
            _db.DeleteAll<AverageNetSalary>();
            _db.InsertAll(averageNetSalaries);

            var numberOfTourists = NumberOfTouristsSeeder.GetData();
            _db.DeleteAll<NumberOfTourists>();
            _db.InsertAll(numberOfTourists);

            var numberOfNights = NumberOfNightsSeeder.GetData();
            _db.DeleteAll<NumberOfNights>();
            _db.InsertAll(numberOfNights);

            var numberOfEmployees = NumberOfEmployeesSeeder.GetData();
            _db.DeleteAll<NumberOfEmployees>();
            _db.InsertAll(numberOfEmployees);
        }
    }
}