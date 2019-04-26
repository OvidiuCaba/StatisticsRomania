using System;

namespace StatisticsRomania.BusinessObjects
{
    public abstract class Data
    {
        public int Id { get; set; }

        public int? CountyId { get; set; }

        public string Subchapter { get; set; }

        public int Year { get; set; }

        // Month, trimester or semester
        public int YearFraction { get; set; }

        public float Value { get; set; }

        public float Value2 { get; set; }

        public County County { get; set; }

        public DateTime TimeStamp
        {
            get { return new DateTime(Year, YearFraction, 1); }
        }
    }
}