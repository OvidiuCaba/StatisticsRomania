using Microsoft.AspNetCore.Mvc;
using StatisticsRomania.BusinessObjects;
using System;
using System.Collections.Generic;

namespace WebClient.Controllers
{
    public class BaseController : Controller
    {
        private const string Exporturi = "Comert international - exporturi FOB";
        private const string Importuri = "Comert international - importuri CIF";
        private const string Sold = "Comert international - sold FOB/CIF";
        private const string EfectivulSalariatilor = "Forta de munca - efectiv salariati";
        private const string CastigulSalarialMediuBrut = "Forta de munca - salariu mediu brut";
        private const string CastigulSalarialMediuNet = "Forta de munca - salariu mediu net";
        private const string NumarulSomerilor = "Forta de munca - numar someri";
        private const string InnoptariInStructurileDePrimireTuristica = "Turism - innoptari";
        private const string SosiriInStructurileDePrimireTuristica = "Turism - numar turisti";
        private const string NascutiVii = "Populatie - nascuti vii";
        private const string Decedati = "Populatie - decedati";
        private const string SporNatural = "Populatie - spor natural";
        private const string Casatorii = "Populatie - casatorii";
        private const string Divorturi = "Populatie - divorturi";
        private const string DecedatiSubUnAn = "Populatie - decedati sub 1 an";
        private const string AutorizatiiDeConstruire = "Autorizatii de construire pentru cladiri rezidentiale";

        protected Dictionary<string, Type> ChapterList = new Dictionary<string, Type>()
            {
                { Exporturi, typeof(ExportFob) },
                { Importuri, typeof(ImportCif) },
                { Sold, typeof(SoldFobCif) },
                { EfectivulSalariatilor, typeof(NumberOfEmployees) },
                { CastigulSalarialMediuBrut, typeof(AverageGrossSalary) },
                { CastigulSalarialMediuNet, typeof(AverageNetSalary) },
                { NumarulSomerilor, typeof(Unemployed) },
                { InnoptariInStructurileDePrimireTuristica, typeof(NumberOfNights) },
                { SosiriInStructurileDePrimireTuristica, typeof(NumberOfTourists) },
                { NascutiVii, typeof(BornAlive) },
                { Decedati, typeof(Deceased) },
                { SporNatural, typeof(NaturalGrowth) },
                { Casatorii, typeof(Marriages) },
                { Divorturi, typeof(Divorces) },
                { DecedatiSubUnAn, typeof(DeceasedUnderOneYearOld) },
                { AutorizatiiDeConstruire, typeof(BuildingPermits) },
            };

        protected Dictionary<string, string> UnitOfMeasureList = new Dictionary<string, string>()
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
                { NascutiVii, "Persoane" },
                { Decedati, "Persoane" },
                { SporNatural, "Persoane" },
                { Casatorii, "Numar" },
                { Divorturi, "Numar" },
                { DecedatiSubUnAn, "Persoane" },
                { AutorizatiiDeConstruire, "Numar" },
            };

        protected List<string> IgnoredChapters => new List<string> { Importuri, Sold, CastigulSalarialMediuBrut, Casatorii, Divorturi };
    }
}