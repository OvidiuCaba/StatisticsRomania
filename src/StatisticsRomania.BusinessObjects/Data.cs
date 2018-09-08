using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using SQLite;

namespace StatisticsRomania.BusinessObjects
{
    public abstract class Data
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(County))]
        public int? CountyId { get; set; }

        [MaxLength(256)]
        public string Subchapter { get; set; }

        public int Year { get; set; }

        // Month, trimester or semester
        public int YearFraction { get; set; }

        public float Value { get; set; }

        [Ignore]
        public float Value2 { get; set; }

        [Ignore]
        public County County { get; set; }

        [Ignore]
        public DateTime TimeStamp
        {
            get { return new DateTime(Year, YearFraction, 1); }
        }
    }
}