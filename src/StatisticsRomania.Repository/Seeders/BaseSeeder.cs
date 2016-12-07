using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    public abstract class BaseSeeder
    {
        protected static List<T> GetItems<T>(List<string> rawData) where T : Data, new()
        {
            var items = new List<T>();

            foreach (var rawItem in rawData)
            {
                var data = rawItem.Split(' ');

                var county = data[2];
                var year = int.Parse(data[0]);
                var month = int.Parse(data[1]);

                for (var i = 3; i < data.Length; i++)
                {
                    var item = new T
                    {
                        Id = typeof(T).Name + "_" + CountryIds.Counties[county] + "_" + year + "_" + month,
                        Chapter = typeof(T).Name,
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