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
                                  };

            var chapterMapping = new Dictionary<string, string>
                                  {
                                      {"NumberOfNightsSeeder", "SOSIRI ÎN PRINCIPALELE STRUCTURI DE PRIMIRE TURISTICĂ"},
                                      {"NumberOfTouristsSeeder", "SOSIRI ÎN PRINCIPALELE STRUCTURI DE PRIMIRE TURISTICĂ"},
                                  };

            var year = 2016;

            var res = new Dictionary<string, string>
                                  {
                                      {"NumberOfNightsSeeder", string.Empty},
                                      {"NumberOfTouristsSeeder", string.Empty},
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
                rowYear.Cells.FirstOrDefault(x => x.CellType == CellType.Numeric && x.NumericCellValue == year).ColumnIndex;

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