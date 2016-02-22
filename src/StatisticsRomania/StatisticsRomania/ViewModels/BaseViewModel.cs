using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StatisticsRomania.BusinessObjects;
using StatisticsRomania.Repository;

namespace StatisticsRomania.ViewModels
{
    public abstract class BaseViewModel
    {
        public Dictionary<string, Type> ChapterList { get; set; }

        public void GetChapters()
        {
            ChapterList = new Dictionary<string, Type>()
                              {
                                  { "Castigul salarial mediu brut", typeof(AverageGrossSalary) },
                                  { "Castigul salarial mediu net", typeof(AverageNetSalary) },
                                  { "Sosiri in structurile de primire turistica", typeof(NumberOfTourists) },
                              };
        }
    }
}