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

        private const string EfectivulSalariatilor = "Forta de munca - efectiv salariati";
        private const string CastigulSalarialMediuBrut = "Forta de munca - salariu mediu brut";
        private const string CastigulSalarialMediuNet = "Forta de munca - salariu mediu net";
        private const string InnoptariInStructurileDePrimireTuristica = "Turism - innoptari";
        private const string SosiriInStructurileDePrimireTuristica = "Turism - numar turisti";

        public void GetChapters()
        {
            ChapterList = new Dictionary<string, Type>()
            {
                { EfectivulSalariatilor, typeof(NumberOfEmployees) },
                { CastigulSalarialMediuBrut, typeof(AverageGrossSalary) },
                { CastigulSalarialMediuNet, typeof(AverageNetSalary) },
                { InnoptariInStructurileDePrimireTuristica, typeof(NumberOfNights) },
                { SosiriInStructurileDePrimireTuristica, typeof(NumberOfTourists) },
            };

            UnitOfMeasureList = new Dictionary<string, string>()
            {
                { EfectivulSalariatilor, "Persoane" },
                { CastigulSalarialMediuBrut, "Lei" },
                { CastigulSalarialMediuNet, "Lei" },
                { InnoptariInStructurileDePrimireTuristica, "Innoptari" },
                { SosiriInStructurileDePrimireTuristica, "Persoane" },
            };
        }
    }
}