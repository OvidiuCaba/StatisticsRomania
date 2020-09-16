using System.Collections.Generic;

namespace StatisticsRomania.BusinessObjects
{
    //public class Covid19 : Data
    //{
    //}

    // Copy/paste into covid19.json the value of https://www.graphs.ro/json.php [perhaps need to use "View source" abnd copy from there]

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class CountyData
    {
        public string county_id { get; set; }
        public string county_name { get; set; }
        public int county_population { get; set; }
        public int total_cases { get; set; }
    }

    public class Covid19
    {
        public string reporting_date { get; set; }
        public int total_cases { get; set; }
        public int new_cases_today { get; set; }
        public int? total_tests { get; set; }
        public int? new_tests_today { get; set; }
        public int? total_deaths { get; set; }
        public int? new_deaths_today { get; set; }
        public int? total_recovered { get; set; }
        public int? new_recovered_today { get; set; }
        public int? intensive_care_right_now { get; set; }
        public int? emergency_calls { get; set; }
        public int? information_calls { get; set; }
        public object persons_in_quarantine { get; set; }
        public int? persons_in_home_isolation { get; set; }
        public int? tests_for_case_definition { get; set; }
        public int? tests_upon_request { get; set; }
        public int? tests_done_before_today_and_reported_today { get; set; }
        public int? infected_asymptomatic { get; set; }
        public int? infected_hospitalized { get; set; }
        public int? infected_positive_retests { get; set; }
        public int? persons_in_institutional_isolation { get; set; }
        public int? persons_in_home_quarantine { get; set; }
        public int? persons_in_institutional_quarantine { get; set; }
        public string romania_population_2020 { get; set; }
        public string sourceUrl { get; set; }
        public List<CountyData> county_data { get; set; }
    }
}