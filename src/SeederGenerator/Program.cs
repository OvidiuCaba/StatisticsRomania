using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Globalization;

namespace SeederGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dir = @"f:\INS\Publicatie_BSL Judete_luna iunie 2016_Excel";

            var fileMapping = new Dictionary<string, string>
                                  {
                                      {"Alba", "Alba.xls"},
                                      {"Arad", "Arad.xls"},
                                      {"Arges", "Arges.xls"},
                                      {"Bacau","Bacau.xls"},
                                      {"Bihor","Bihor.xls"},
                                      {"BistritaNasaud","Bistrita-Nasaud.xls"},
                                      {"Botosani","Botosani.xls"},
                                      {"Brasov","Brasov.xls"},
                                      {"Braila","Braila.xls"},
                                      {"Buzau","Buzau.xls"},
                                      {"CarasSeverin","Caras-Severin.xls"},
                                      {"Calarasi","Calarasi.xls"},
                                      {"Cluj","Cluj.xls"},
                                      {"Constanta","Constanta.xls"},
                                      {"Covasna","Covasna.xls"},
                                      {"Dambovita","Dambovita.xls"},
                                      {"Dolj","Dolj.xls"},
                                      {"Galati","Galati.xls"},
                                      {"Giurgiu","Giurgiu.xls"},
                                      {"Gorj","Gorj.xls"},
                                      {"Harghita","Harghita.xls"},
                                      {"Hunedoara","Hunedoara.xls"},
                                      {"Ialomita","Ialomita.xls"},
                                      {"Iasi","Iasi.xls"},
                                      {"Ilfov","Ilfov.xls"},
                                      {"Maramures","Maramures.xls"},
                                      {"Mehedinti","Mehedinti.xls"},
                                      {"Mures","Mures.xls"},
                                      {"Neamt","Neamt.xls"},
                                      {"Olt","Olt.xls"},
                                      {"Prahova","Prahova.xls"},
                                      {"SatuMare","Satu Mare.xls"},
                                      {"Salaj","Salaj.xls"},
                                      {"Sibiu","Sibiu.xls"},
                                      {"Suceava","Suceava.xls"},
                                      {"Teleorman","Teleorman.xls"},
                                      {"Timis","Timis.xls"},
                                      {"Tulcea","Tulcea.xls"},
                                      {"Vaslui","Vaslui.xls"},
                                      {"Valcea","Valcea.xls"},
                                      {"Vrancea","Vrancea.xls"},
                                      {"Bucuresti","Bucuresti.xls"},
                                  };

            var chapterMapping = new Dictionary<string, string>
                                     {
                                         {"AverageGrossSalarySeeder", "CÂŞTIGUL SALARIAL MEDIU BRUT"},
                                         {"AverageNetSalarySeeder", "CÂŞTIGUL SALARIAL MEDIU NET"},
                                         {"NumberOfTouristsSeeder", "SOSIRI ÎN PRINCIPALELE STRUCTURI DE PRIMIRE TURISTICĂ"},
                                         {"NumberOfNightsSeeder", "ÎNNOPTĂRI ÎN PRINCIPALELE STRUCTURI DE PRIMIRE TURISTICĂ"},
                                         {"NumberOfEmployeesSeeder", "EFECTIVUL SALARIAŢILOR"},
                                         {"UnemployedSeeder", "NUMĂRUL ŞOMERILOR"},
                                         {"ExportFobSeeder", "COMERŢUL INTERNAŢIONAL CU BUNURI"},
                                         {"ImportCifSeeder", "COMERŢUL INTERNAŢIONAL CU BUNURI"},
                                         {"SoldFobCifSeeder", "COMERŢUL INTERNAŢIONAL CU BUNURI"},
                                     };

            var year = 2016;

            var res = new Dictionary<string, string>
                          {
                              {"AverageGrossSalarySeeder", string.Empty},
                              {"AverageNetSalarySeeder", string.Empty},
                              {"NumberOfTouristsSeeder", string.Empty},
                              {"NumberOfNightsSeeder", string.Empty},
                              {"NumberOfEmployeesSeeder", string.Empty},
                              {"UnemployedSeeder", string.Empty},
                              {"ExportFobSeeder", string.Empty},
                              {"ImportCifSeeder", string.Empty},
                              {"SoldFobCifSeeder", string.Empty},
                          };

            foreach (var file in fileMapping)
            {
                foreach (var chapter in chapterMapping.Keys)
                {
                    res[chapter] += GetSeedingText(file.Key, dir + "\\" + file.Value, chapterMapping[chapter], year) + Environment.NewLine;
                }
            }

            foreach (var chapter in chapterMapping.Keys)
            {
                Console.WriteLine(chapter);
                Console.WriteLine(res[chapter]);
                Console.WriteLine(Environment.NewLine + Environment.NewLine);
            }

            Console.ReadKey();
        }

        private static string GetSeedingText(string county, string file, string chapter, int year)
        {
            var chapterRowIndex = -1;

            HSSFWorkbook excelFile;

            using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                excelFile = new HSSFWorkbook(fileStream);
            }

            var sheet = excelFile.GetSheetAt(0);
            var columnYearStartIndex = -1;

            for (var rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);

                if (row == null) //null is when the row only contains empty cells 
                    continue;

                var cell = row.GetCell(0);

                if (cell == null)
                    continue;

                if (!cell.StringCellValue.ToLower().Contains(chapter.ToLower()))
                    continue;

                chapterRowIndex = rowIndex;

                break;
            }

            var rowYear = sheet.GetRow(chapterRowIndex);

            columnYearStartIndex =
                rowYear.Cells.FirstOrDefault(x => (x.CellType == CellType.Numeric && x.NumericCellValue == year) || ((x.CellType == CellType.String && x.StringCellValue.Contains(year.ToString(CultureInfo.InvariantCulture))))).ColumnIndex;

            var numberOfMonths =
                sheet.GetRow(chapterRowIndex + 1).Cells.Count(
                    x => x.ColumnIndex >= columnYearStartIndex && !string.IsNullOrWhiteSpace((x.StringCellValue)));

            var text =
                sheet.GetRow(chapterRowIndex + 2).Cells.Where(
                    x => x.ColumnIndex >= columnYearStartIndex && x.ColumnIndex < columnYearStartIndex + numberOfMonths).Select(
                        x => x.NumericCellValue.ToString(CultureInfo.InvariantCulture)).Aggregate((c, n) => c + " " + n);

            return "\"" + year.ToString(CultureInfo.InvariantCulture) + " 1 " + county + " " + text + "\",";
        }
    }
}