//using SQLite.Net.Attributes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsRomania.BusinessObjects
{
    public class County
    {
        [PrimaryKey]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }
    }
}