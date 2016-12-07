using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace StatisticsRomania.BusinessObjects
{
    public class Data
    {
        //[PrimaryKey, AutoIncrement]
        public string Id { get; set; } // TODO: this should be string

        [ForeignKey(typeof(County))]
        public int? CountyId { get; set; }

        public string Chapter { get; set; }

        [MaxLength(256)]
        public string Subchapter { get; set; }

        public int Year { get; set; }

        // Month, trimester or semester
        public int YearFraction { get; set; }

        public float Value { get; set; }

        [Ignore]
        public float Value2 { get; set; }

        [ManyToOne(ReadOnly = true)]
        public County County { get; set; }

        [Ignore]
        public DateTime TimeStamp
        {
            get { return new DateTime(Year, YearFraction, 1); }
        }
    }
}