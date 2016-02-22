using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;
using StatisticsRomania.BusinessObjects;
using Seeders = StatisticsRomania.Repository.Seeders;

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
        }

        private void SeedDatabase()
        {
            var counties = Seeders.CountiesSeeder.GetData();

            _db.InsertOrReplaceAll(counties);

            var averageGrossSalaries = Seeders.Alba.AverageGrossSalarySeeder.GetData();
            averageGrossSalaries.AddRange(Seeders.Arad.AverageGrossSalarySeeder.GetData());
            averageGrossSalaries.AddRange(Seeders.Arges.AverageGrossSalarySeeder.GetData());
            averageGrossSalaries.AddRange(Seeders.AverageGrossSalarySeeder.GetData());

            _db.DeleteAll<AverageGrossSalary>();
            _db.InsertAll(averageGrossSalaries);

            var averageNetSalaries = Seeders.AverageNetSalarySeeder.GetData();
            _db.DeleteAll<AverageNetSalary>();
            _db.InsertAll(averageNetSalaries);

            var numberOfTourists = Seeders.NumberOfTouristsSeeder.GetData();
            _db.DeleteAll<NumberOfTourists>();
            _db.InsertAll(numberOfTourists);
        }
    }
}