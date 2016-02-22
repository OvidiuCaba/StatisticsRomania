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
        protected Dictionary<string, string> UnitOfMeasureList;

        public Dictionary<string, Type> ChapterList { get; set; }

        private const string CastigulSalarialMediuBrut = "Castigul salarial mediu brut";
        private const string CastigulSalarialMediuNet = "Castigul salarial mediu net";
        private const string SosiriInStructurileDePrimireTuristica = "Sosiri in structurile de primire turistica";

        public void GetChapters()
        {
            ChapterList = new Dictionary<string, Type>()
            {
                { CastigulSalarialMediuBrut, typeof(AverageGrossSalary) },
                { CastigulSalarialMediuNet, typeof(AverageNetSalary) },
                { SosiriInStructurileDePrimireTuristica, typeof(NumberOfTourists) },
            };

            UnitOfMeasureList = new Dictionary<string, string>()
            {
                { CastigulSalarialMediuBrut, "Lei" },
                { CastigulSalarialMediuNet, "Lei" },
                { SosiriInStructurileDePrimireTuristica, "Numar turisti" },
            };
        }
    }
}