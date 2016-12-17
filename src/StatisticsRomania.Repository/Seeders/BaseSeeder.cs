using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    public abstract class BaseSeeder
    {
        protected static List<Data> GetItems(string chapter, List<string> rawData)
        {
            var items = new List<Data>();

            foreach (var rawItem in rawData)
            {
                var data = rawItem.Split(' ');

                var county = data[2];
                var year = int.Parse(data[0]);
                var month = int.Parse(data[1]);

                for (var i = 3; i < data.Length; i++)
                {
                    var item = new Data
                    {
                        Id = chapter + "_" + CountyIds.Counties[county] + "_" + year + "_" + month,
                        Chapter = chapter,
                        Subchapter = "Total judet",
                        CountyId = CountyIds.Counties[county],
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