using System.Collections.Generic;

//TODO: nu arata bine pe Nexus S, probabil trebuie sa abreviez
namespace StatisticsRomania.ViewModels
{
    public abstract class BaseViewModel
    {
        protected Dictionary<string, string> UnitOfMeasureList;

        public bool IsLoading { get; set; }

        public Dictionary<string, string> ChapterList { get; set; }

        private const string Exporturi = "Comert international - exporturi FOB";
        private const string Importuri = "Comert international - importuri CIF";
        private const string Sold = "Comert international - sold FOB/CIF";
        private const string EfectivulSalariatilor = "Forta de munca - efectiv salariati";
        private const string CastigulSalarialMediuBrut = "Forta de munca - salariu mediu brut";
        private const string CastigulSalarialMediuNet = "Forta de munca - salariu mediu net";
        private const string NumarulSomerilor = "Forta de munca - numar someri";
        private const string InnoptariInStructurileDePrimireTuristica = "Turism - innoptari";
        private const string SosiriInStructurileDePrimireTuristica = "Turism - numar turisti";

        public void GetChapters()
        {
            ChapterList = new Dictionary<string, string>()
            {
                [Exporturi] = "ExportFob",
                [Importuri] = "ImportCif",
                [Sold] = "SoldFobCif",
                [EfectivulSalariatilor] = "NumberOfEmployees",
                [CastigulSalarialMediuBrut] = "AverageGrossSalary",
                [CastigulSalarialMediuNet] = "AverageNetSalary",
                [NumarulSomerilor] = "Unemployed",
                [InnoptariInStructurileDePrimireTuristica] = "NumberOfNights",
                [SosiriInStructurileDePrimireTuristica] = "NumberOfTourists",
            };

            UnitOfMeasureList = new Dictionary<string, string>()
            {
                { Exporturi, "Mii Euro" },
                { Importuri, "Mii Euro" },
                { Sold, "Mii Euro" },
                { EfectivulSalariatilor, "Persoane" },
                { CastigulSalarialMediuBrut, "Lei" },
                { CastigulSalarialMediuNet, "Lei" },
                { NumarulSomerilor, "Persoane" },
                { InnoptariInStructurileDePrimireTuristica, "Innoptari" },
                { SosiriInStructurileDePrimireTuristica, "Persoane" },
            };
        }
    }
}