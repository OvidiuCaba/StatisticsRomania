using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    internal abstract class BaseSeeder
    {
        internal static List<T> GetData<T>() where T : Data, new()
        {
            var rawData = GetDataFromResources<T>();

            var items = GetItems<T>(rawData);

            return items;
        }

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

        private static List<string> GetDataFromResources<T>()
        {
            var seederTypeName = typeof(T).Name;

            var resourceKeys = typeof(CountiesData).GetProperties(BindingFlags.NonPublic | BindingFlags.Static).Where(x => x.Name.EndsWith($"{seederTypeName}Seeder")).Select(x => (string)x.GetValue(null)).ToList();
            resourceKeys.Sort();
            // TODO: if mobile, we would like to take only last 3 years; but for web we want everything
            //resourceKeys = resourceKeys.Skip(resourceKeys.Count - 3).ToList();

            var res = new List<string>();

            resourceKeys.ForEach(resourceKey => res.AddRange(GetDataFromResources(resourceKey)));

            return res;
        }

        private static List<string> GetDataFromResources(string resourceKey)
        {
            var dataFromResources = resourceKey.Split(',').Select(x => x.Replace(Environment.NewLine, string.Empty).Replace("\r", string.Empty).Replace("\"", string.Empty)).ToList();
            dataFromResources.RemoveAll(x => x == string.Empty);
            dataFromResources.RemoveAll(x => x == Environment.NewLine);
            dataFromResources.RemoveAll(x => x == "\r");
            return dataFromResources;
        }
    }
}