using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;

namespace StatisticsRomania.ViewModels
{
    public class CountyDetailsViewModel
    {
        private readonly IRepository<County> _chapterRepository;
        readonly ObservableCollection<AverageGrossSalary> _averageGrossSalaryCollection;

        public Dictionary<string, int> CountyList { get; set; }

        public ObservableCollection<AverageGrossSalary> AverageGrossSalaryCollection
        {
            get { return _averageGrossSalaryCollection; }
        }

        public ObservableCollection<AverageGrossSalary> AverageGrossSalaryCollection2
        {
            get { return _averageGrossSalaryCollection; }
        }

        public CountyDetailsViewModel()
        {
            _chapterRepository = new Repostory<County>(App.AsyncDb);
            _averageGrossSalaryCollection = new ObservableCollection<AverageGrossSalary>();
        }

        public async Task GetCounties()
        {
            CountyList = (await _chapterRepository.Get()).ToDictionary(x => x.Name, x => x.Id);
        }

        public async Task GetAverageGrossSalaries()
        {
            await Task.Delay(1);

            var item10 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2016,
                YearFraction = 1,
                Value = 10
            };
            var item11 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2016,
                YearFraction = 2,
                Value = 11
            };
            var item12 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2016,
                YearFraction = 3,
                Value = 12
            };
            var item13 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2016,
                YearFraction = 4,
                Value = 15
            };
            var item14 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2016,
                YearFraction = 5,
                Value = 15
            };
            var item15 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2016,
                YearFraction = 6,
                Value = 150
            };
            var item20 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2015,
                YearFraction = 1,
                Value = 10
            };
            var item21 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2015,
                YearFraction = 2,
                Value = 11
            };
            var item22 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2015,
                YearFraction = 3,
                Value = 12
            };
            var item23 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2015,
                YearFraction = 4,
                Value = 15
            };
            var item24 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2015,
                YearFraction = 5,
                Value = 15
            };
            var item25 = new AverageGrossSalary()
            {
                Id = 1,
                CountyId = 1,
                Year = 2015,
                YearFraction = 6,
                Value = 150
            };

            AverageGrossSalaryCollection.Add(item10);
            AverageGrossSalaryCollection.Add(item11);
            AverageGrossSalaryCollection.Add(item12);
            AverageGrossSalaryCollection.Add(item13);
            AverageGrossSalaryCollection.Add(item14);
            AverageGrossSalaryCollection.Add(item15);
            AverageGrossSalaryCollection.Add(item20);
            AverageGrossSalaryCollection.Add(item21);
            AverageGrossSalaryCollection.Add(item22);
            AverageGrossSalaryCollection.Add(item23);
            AverageGrossSalaryCollection.Add(item24);
            AverageGrossSalaryCollection.Add(item25);
        }
    }
}