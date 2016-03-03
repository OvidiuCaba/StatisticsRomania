using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    internal abstract class BaseSeeder
    {
        protected static List<T> GetItems<T>(List<string> rawData) where T : Data, new()
        {
            var items = new List<T>();

            foreach (var rawItem in rawData)
            {
                var data = rawItem.Split(' ');

                var county = data[0];
                var year = 2014;
                var month = 10;

                for (var i = 1; i <= 13; i++)
                {
                    var item = new T()
                    {
                        Subchapter = "Total judet",
                        CountyId = CountryIds.Counties[county],
                        Year = year,
                        YearFraction = month,
                        Value = float.Parse(data[i])
                    };

                    items.Add(item);

                    month++;
                    if (month == 13)
                    {
                        year++;
                        month = 1;
                    }
                }
            }
            return items;
        }

    }
}