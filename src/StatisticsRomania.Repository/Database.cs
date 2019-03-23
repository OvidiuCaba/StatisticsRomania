using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository.Seeders;

namespace StatisticsRomania.Repository
{
    public class Database
    {
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
            AsyncDb = new SQLiteAsyncConnection(Path, true);
            _db = new SQLiteConnection(Path, true);
        }

        private void CreateDatabase()
        {
            _db.CreateTable<County>();
            _db.CreateTable<AverageNetSalary>();
            _db.CreateTable<AverageGrossSalary>();
            _db.CreateTable<NumberOfTourists>();
            _db.CreateTable<NumberOfNights>();
            _db.CreateTable<NumberOfEmployees>();
            _db.CreateTable<Unemployed>();
            _db.CreateTable<ExportFob>();
            _db.CreateTable<ImportCif>();
            _db.CreateTable<SoldFobCif>();
        }

        private void SeedDatabase()
        {
            var numberOfCountiesInDb = _db.Table<County>().Count();

            var tooManyCountiesInDb = numberOfCountiesInDb > 42;    // for some reasons, on some devices, SQLite lets the code to insert more PKs with the same values
            if (tooManyCountiesInDb)
                _db.DeleteAll<County>();

            if (numberOfCountiesInDb == 0)
            {
                var counties = CountiesSeeder.GetData();
                try
                {
                    _db.InsertAll(counties);
                }
                catch
                {
                    // If the counties are already there, I'll get a constraint exception;
                    // I don't care
                }
            }

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

            var numberOfUnemployed = UnemployedSeeder.GetData();
            _db.DeleteAll<Unemployed>();
            _db.InsertAll(numberOfUnemployed);

            var export = ExportFobSeeder.GetData();
            _db.DeleteAll<ExportFob>();
            _db.InsertAll(export);

            var import = ImportCifSeeder.GetData();
            _db.DeleteAll<ImportCif>();
            _db.InsertAll(import);

            var sold = SoldFobCifSeeder.GetData();
            _db.DeleteAll<SoldFobCif>();
            _db.InsertAll(sold);
        }
    }
}