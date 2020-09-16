using Newtonsoft.Json;
using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;
using System.Text;

namespace StatisticsRomania.Repository.Seeders
{
    public class Covid19Seeder
    {
        public static List<Covid19> GetData()
        {
            return JsonConvert.DeserializeObject<List<Covid19>>(Encoding.Default.GetString(CountiesData.covid19));
        }
    }
}