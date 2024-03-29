﻿using Microsoft.WindowsAPICodePack.Shell;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SeederGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var isUnpackingNeeded = true;

            var assemblyLocation = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            var driveLetter = assemblyLocation.Substring(0, 1);

            var months = new[] { "ian", "feb", "mar", "apr", "mai", "iun", "iul", "aug", "sep", "oct", "nov", "dec" };

            var downloadsFolder = KnownFolders.Downloads.Path;
            var downloadsFiles = Directory.GetFiles(downloadsFolder);
            var pattern = $@"[\w| ]*({string.Join("|", months)})[\w| |.]*(20[2-9][0-9])";
            var downloadedFile = string.Empty;
            downloadsFiles.ToList().ForEach(x =>
            {
                var downloadedFileMatch = Regex.Match(x, pattern, RegexOptions.IgnoreCase);

                if (downloadedFileMatch.Success)
                {
                    if (string.IsNullOrEmpty(downloadedFile))
                        downloadedFile = x;
                    else
                        throw new InvalidOperationException("More than one INS file was found in the Downloads directory.");
                }
            });

            var tempDir = $@"{driveLetter}:\INS\Temp";

            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);

            using (var archive = RarArchive.Open(Path.Combine(downloadsFolder, downloadedFile)))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(tempDir, new ExtractionOptions()
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    });
                }
            }

            var yearString = Regex.Match(downloadedFile, @"\d+").Value;
            var year = int.Parse(yearString);                                                           // year - 1 for international commerce
            var monthString = Regex.Match(downloadedFile, $@"({string.Join("|", months)})").Value;
            var dir = $@"{driveLetter}:\INS\Publicatie BSL Judete_ Excel_luna {monthString}. {year}\";  // add {year + 1} for international commerce;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var tempDirSubFolder = downloadedFile.Substring(downloadedFile.LastIndexOf('\\') + 1).Replace(".rar", string.Empty);

            var sourcePath = Path.Combine(tempDir, tempDirSubFolder);
            var targetPath = dir;
            //sourcePath = @"F:\INS\Temp\BSL Judete_noiembrie 2022";
            var sourceFiles = Directory.GetFiles(sourcePath);
            sourceFiles.ToList().ForEach(x =>
            {
                if (!isUnpackingNeeded)
                    return;

                var fileName = x.Substring(x.LastIndexOf('\\') + 1);
                var sourceFile = Path.Combine(sourcePath, fileName);
                var destFile = Path.Combine(targetPath, fileName);

                if (File.Exists(destFile))
                    return;

                File.Copy(sourceFile, destFile, true);
            });

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
                                      {"SatuMare","Satu-Mare.xls"},
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

            // The second parameter of the tuple is the number of the row in the table that contains the data
            var chapterMapping = new Dictionary<string, Tuple<string, int>>
            {
                {"AverageGrossSalarySeeder", new Tuple<string, int>("CÂŞTIGUL SALARIAL MEDIU BRUT", 1)},
                {"AverageNetSalarySeeder", new Tuple<string, int>("CÂŞTIGUL SALARIAL MEDIU NET", 1)},
                {"NumberOfTouristsSeeder", new Tuple<string, int>("SOSIRI ÎN PRINCIPALELE STRUCTURI DE PRIMIRE TURISTICĂ", 1)},
                {"NumberOfNightsSeeder", new Tuple<string, int>("ÎNNOPTĂRI ÎN PRINCIPALELE STRUCTURI DE PRIMIRE TURISTICĂ", 1)},
                {"NumberOfEmployeesSeeder", new Tuple<string, int>("EFECTIVUL SALARIAŢILOR", 1)},
                {"UnemployedSeeder", new Tuple<string, int>("NUMĂRUL ŞOMERILOR", 1)},
                {"ExportFobSeeder", new Tuple<string, int>("COMERŢUL INTERNAŢIONAL CU BUNURI", 1)},
                {"ImportCifSeeder", new Tuple<string, int>("COMERŢUL INTERNAŢIONAL CU BUNURI", 2)},
                {"SoldFobCifSeeder", new Tuple<string, int>("COMERŢUL INTERNAŢIONAL CU BUNURI", 3)},
                {"BornAliveSeeder", new Tuple<string, int>("MIŞCAREA NATURALĂ A POPULAŢIEI", 1) },
                {"DeceasedSeeder", new Tuple<string, int>("MIŞCAREA NATURALĂ A POPULAŢIEI", 2) },
                {"NaturalGrowthSeeder", new Tuple<string, int>("MIŞCAREA NATURALĂ A POPULAŢIEI", 3) },
                {"MarriagesSeeder", new Tuple<string, int>("MIŞCAREA NATURALĂ A POPULAŢIEI", 4) },
                {"DivorcesSeeder", new Tuple<string, int>("MIŞCAREA NATURALĂ A POPULAŢIEI", 5) },
                {"DeceasedUnderOneYearOldSeeder", new Tuple<string, int>("MIŞCAREA NATURALĂ A POPULAŢIEI", 6) },
                {"BuildingPermitsSeeder", new Tuple<string, int>("AUTORIZAŢII DE CONSTRUIRE ELIBERATE PENTRU CLĂDIRI REZIDENŢIALE", 1) },
            };

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
                {"BornAliveSeeder", string.Empty },
                {"DeceasedSeeder", string.Empty },
                {"NaturalGrowthSeeder", string.Empty },
                {"MarriagesSeeder", string.Empty },
                {"DivorcesSeeder", string.Empty },
                {"DeceasedUnderOneYearOldSeeder", string.Empty },
                {"BuildingPermitsSeeder", string.Empty },
            };

            foreach (var file in fileMapping)
            {
                foreach (var chapter in chapterMapping.Keys)
                {
                    res[chapter] += GetSeedingText(file.Key, dir + "\\" + file.Value, chapterMapping[chapter].Item1, chapterMapping[chapter].Item2, year, months) + Environment.NewLine;
                }
            }

            var targetDirectory = dir + @"\Seeders\";

            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            foreach (var chapter in chapterMapping.Keys)
            {
                File.WriteAllText(targetDirectory + year + chapter + ".txt", res[chapter]);
            }
        }

        private static string GetSeedingText(string county, string file, string chapter, int rowNumber, int year, string[] months)
        {
            var chapterRowIndex = -1;

            IWorkbook excelFile;

            if (!File.Exists(file))
                file = file.Replace("-", " ");

            // Sometimes INS provides xls, sometimes it provides xlsx; so let's check for xlsx, too
            if (!File.Exists(file))
                file = file.Replace(".xls", ".xlsx");

            // Sometimes INS provides Bucuresti, sometimes it provides București; so let's check for București, too
            if (!File.Exists(file) && county == "Bucuresti")
                file = file.Replace("Bucuresti", "București");

            if (!File.Exists(file) && county == "Salaj")
                file = file.Replace("Salaj", "Sălaj");

            // If București.xlsx is not found, let's look for București.xls
            if (!File.Exists(file))
                file = file.Replace(".xlsx", ".xls");

            // Sometimes a space is added in the name of the file ("Valcea.xls" becomes "Valcea .xls")
            if (!File.Exists(file))
                file = file.Replace(".xls", " .xls");

            using (var fileStream = new FileStream(file.TrimEnd(), FileMode.Open, FileAccess.Read))
            {
                excelFile = file.EndsWith(".xls") ? (IWorkbook)new HSSFWorkbook(fileStream) : (IWorkbook)new XSSFWorkbook(fileStream);
            }

            var sheet = excelFile.GetSheetAt(0);

            chapterRowIndex = GetChapterRowIndex(chapter, chapterRowIndex, sheet, 0);

            if (chapterRowIndex == -1)
            {
                chapterRowIndex = GetChapterRowIndex(chapter, chapterRowIndex, sheet, 1);
            }

            var rowYear = sheet.GetRow(chapterRowIndex);

            var columnYearStartIndex =
                rowYear.Cells.FirstOrDefault(x => (x.CellType == CellType.Numeric && x.NumericCellValue == year) || ((x.CellType == CellType.String && x.StringCellValue.Contains(year.ToString(CultureInfo.InvariantCulture)))))?.ColumnIndex ?? 0;

            if (columnYearStartIndex == 0)
                return string.Empty;

            var numberOfMonths =
                sheet.GetRow(chapterRowIndex + 1).Cells.Count(
                    x => x.ColumnIndex >= columnYearStartIndex && months.Any(month => x.StringCellValue.Trim().StartsWith(month)));

            if (numberOfMonths > 12)
                numberOfMonths = 12;

            Debug.WriteLine("Current county: " + county + "; chapter: " + chapter);

            var cellsContainingTheValuesForCurrentYear = sheet.GetRow(chapterRowIndex + rowNumber + 1).Cells.Where(x => x.ColumnIndex >= columnYearStartIndex && x.ColumnIndex < columnYearStartIndex + numberOfMonths);

            if (!cellsContainingTheValuesForCurrentYear.Any())
                return null;

            var text = cellsContainingTheValuesForCurrentYear
                .Select(x => x.CellType == CellType.String ? x.StringCellValue.Trim() : x.NumericCellValue.ToString(CultureInfo.InvariantCulture))
                .Select(x => x == "-" ? "0" : x)
                .Select(x => x == "_" ? "0" : x)
                .Aggregate((c, n) => c + " " + n);

            return "\"" + year.ToString(CultureInfo.InvariantCulture) + " 1 " + county + " " + text + "\",";
        }

        private static int GetChapterRowIndex(string chapter, int chapterRowIndex, ISheet sheet, int chapterColumnIndex)
        {
            for (var rowIndex = 0; rowIndex <= sheet.LastRowNum; rowIndex++)
            {
                var row = sheet.GetRow(rowIndex);

                if (row == null) // null is when the row only contains empty cells 
                    continue;

                var cell = row.GetCell(chapterColumnIndex);

                if (cell == null)
                    continue;

                if (cell.CellType != CellType.String)
                    continue;   // we're looking for chapter, so we don't care about the cell if it's not string

                if (!cell.StringCellValue.ToLower().Contains(chapter.ToLower()) && !cell.StringCellValue.ToLower().Replace('ș', 'ş').Contains(chapter.ToLower()))
                    continue;

                chapterRowIndex = rowIndex;

                break;
            }

            return chapterRowIndex;
        }
    }
}