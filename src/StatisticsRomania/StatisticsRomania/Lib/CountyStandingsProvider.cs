using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;
using StatisticsRomania.ViewModels;

namespace StatisticsRomania.Lib
{
    public static class CountyStandingsProvider
    {
        public static async Task<List<StandingItem>> GetData(Type chapter, int year, int yearFraction)
        {
            if (chapter == typeof(AverageGrossSalary))
            {
                var repo = new Repository<AverageGrossSalary>(App.AsyncDb);
                var rawData = (await repo.GetAll(x => x.Year == year && x.YearFraction == yearFraction))
                    .OrderByDescending(x => x.Value)
                    .ToList();

                var data = new List<StandingItem>();
                var index = 1;
                foreach (var item in rawData)
                {
                    await repo.GetChild(item, x => x.County);

                    var standingItem = new StandingItem() { Position = index++, County = item.County.Name, Value = item.Value };
                    data.Add(standingItem);
                }

                return data;
            }
            else if (chapter == typeof(AverageNetSalary))
            {
                var repo = new Repository<AverageNetSalary>(App.AsyncDb);
                var data = (await repo.GetAll(x => x.Year == year && x.YearFraction == yearFraction))
                    .OrderByDescending(x => x.Value)
                    .Select(x => new StandingItem() { County = x.County.Name, Value = x.Value })
                    .ToList();

                var index = 1;
                foreach (var item in data)
                {
                    item.Position = index++;
                }

                return data;
            }
            else
            {
                return null;
            }
        }
    }
}