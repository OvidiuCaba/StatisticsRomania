using System.Collections.Generic;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    internal class ImportCifSeeder : BaseSeeder
    {
        internal static List<ImportCif> GetData()
        {
            var rawData = new List<string>()
                              {
                                  "Alba ",
                                  "Arad ",
                                  "Arges ",
                                  "Bacau ",
                                  "Bihor ",
                                  "BistritaNasaud ",
                                  "Botosani ",
                                  "Brasov ",
                                  "Braila ",
                                  "Buzau ",
                                  "CarasSeverin ",
                                  "Calarasi ",
                                  "Cluj ",
                                  "Constanta ",
                                  "Covasna ",
                                  "Dambovita ",
                                  "Dolj ",
                                  "Galati ",
                                  "Giurgiu ",
                                  "Gorj ",
                                  "Harghita ",
                                  "Hunedoara ",
                                  "Ialomita ",
                                  "Iasi ",
                                  "Ilfov ",
                                  "Maramures ",
                                  "Mehedinti ",
                                  "Mures ",
                                  "Neamt ",
                                  "Olt ",
                                  "Prahova ",
                                  "SatuMare ",
                                  "Salaj ",
                                  "Sibiu ",
                                  "Suceava ",
                                  "Teleorman ",
                                  "Timis ",
                                  "Tulcea ",
                                  "Vaslui ",
                                  "Valcea ",
                                  "Vrancea ",
                                  "Bucuresti ",
                              };

            var items = GetItems<ImportCif>(rawData);

            return items;
        }
    }
}
