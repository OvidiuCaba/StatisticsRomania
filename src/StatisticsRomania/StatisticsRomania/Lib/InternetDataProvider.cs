using Akavache;
using Newtonsoft.Json;
using Plugin.Connectivity;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;   // IMPORTANT - this makes await work!
using System.Threading.Tasks;

namespace StatisticsRomania.Lib
{
    public static class InternetDataProvider
    {
        public static async Task<CountyDetailsDto> GetCountyDetailsFromCacheOrInternet(int countyId, int countyId2, string chapter)
        {
            var key = $"CountyDetails_{chapter}_{countyId}_{countyId2}";

            if (CrossConnectivity.Current.IsConnected == false)
            {
                try
                {
                    return await BlobCache.LocalMachine.GetObject<CountyDetailsDto>(key);
                }
                catch
                {
                    return null;
                }
            }

            try
            {
                await BlobCache.LocalMachine.Invalidate(key);
            }
            catch
            {
                // I don't care if I didn't find the key
            }

            return await BlobCache.LocalMachine.GetOrFetchObject(
                key,
                async () => await GetCountyDetailsFromInternet(countyId, countyId2, chapter),
                DateTimeOffset.Now.AddMonths(6)
                );
        }

        public static async Task<StandingsDto> GetStandingsFromCacheOrInternet(string chapter, int year, int yearFraction)
        {
            var key = $"Standings_{chapter}_{year}_{yearFraction}";

            if (CrossConnectivity.Current.IsConnected == false)
            {
                try
                {
                    return await BlobCache.LocalMachine.GetObject<StandingsDto>(key);
                }
                catch
                {
                    return null;
                }
            }

            try
            {
                await BlobCache.LocalMachine.Invalidate(key);
            }
            catch
            {
                // I don't care if I didn't find the key
            }

            return await BlobCache.LocalMachine.GetOrFetchObject(
                key,
                async () => await GetStandingsFromInternet(chapter, year, yearFraction),
                DateTimeOffset.Now.AddMonths(6)
                );
        }

        private static async Task<StandingsDto> GetStandingsFromInternet(string chapter, int year, int yearFraction)
        {
            var client = new HttpClient();

            var uri = new Uri(string.Format("http://statisticiromania.ro/api/Standings/GetStandings?chapter={0}&year={1}&yearFraction={2}", chapter, year, yearFraction));

            StandingsDto internetData = null;

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                internetData = JsonConvert.DeserializeObject<StandingsDto>(content);
            }

            return internetData;
        }

        private static async Task<CountyDetailsDto> GetCountyDetailsFromInternet(int countyId, int countyId2, string chapter)
        {
            var client = new HttpClient();

            var uri = new Uri(string.Format("http://statisticiromania.ro/api/CountyDetails/GetCountyDetails?countyId={0}&countyId2={1}&chapter={2}&needToProcessAllYear=false", countyId, countyId2, chapter));

            CountyDetailsDto internetData = null;

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                internetData = JsonConvert.DeserializeObject<CountyDetailsDto>(content);
            }

            return internetData;
        }

        public class DataDto : Data
        {
        }

        public class CountyDetailsDto
        {
            public string ValueColumnCaption { get; set; }
            public string Value2ColumnCaption { get; set; }
            public List<DataDto> Data { get; set; }
        }

        public class StandingsDto
        {
            public string ValueColumnCaption { get; set; }
            public List<StandingItem> Data { get; set; }
        }
    }
}