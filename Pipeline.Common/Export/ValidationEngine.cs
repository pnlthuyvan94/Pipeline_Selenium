using Pipeline.Common.Utils;
using System;
using System.IO;
using System.Text;
using System.Linq;
using Pipeline.Common.BaseClass;
using System.Reflection;
using ExcelDataReader;
using System.Collections.Generic;
using NUnit.Framework;
using System.Globalization;

namespace Pipeline.Common.Enums
{
    public static class ValidationEngine
    {

        public readonly static string APP_DIR = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public readonly static string BASE_DIR = APP_DIR.Substring(0, APP_DIR.Length - 9);

        public readonly static int[] SUCCESS_CODES = { 0, 1, 2, 11, 12 };
        public readonly static int XML_IGNORE_CODE = 13;

        /// <summary>
        /// Get Export file name
        /// </summary>
        /// <param name="export"></param>
        /// <param name="format"></param>
        /// <param name="dataTitleInput">data input included community/ house/job name</param>
        /// <returns></returns>
        public static string GetFullExportFileName(string exportFileName, TableType format)
        {
            string fileExtension = format.ToString().ToLower();
            return $"{exportFileName}.{fileExtension}";
        }

        /// <summary>
        /// Generate a report of differences for the currently exported file
        /// </summary>
        /// <returns>A string representing the path to the report file</returns>
        public static string GenerateReportFile(string exportFileName, TableType format)
        {
            string file_name = GetFullExportFileName(exportFileName, format);
            string exported_file_path = CommonHelper.GetFullDownLoadFilePath(file_name);
            string baseline_file_path = BaseValues.BaselineFilesDir + $@"\{file_name}";

            try
            {
                string comparison_report = WindowsSystemHelper.GetBeyondCompareReport($"Pipeline_{exportFileName}-{format}", exported_file_path, baseline_file_path);

                if (comparison_report == string.Empty)
                    ExtentReportsHelper.LogWarning($"Unable to generate file report using Beyond Compare - Report file path returned was empty");
                else
                {
                    CommonHelper.OpenLocalFile(comparison_report);
                    System.Threading.Thread.Sleep(500);
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Generated a file report comparing the exported '{file_name}' file with the baseline '{file_name}' test file: ");
                    System.Threading.Thread.Sleep(500);
                    CommonHelper.NavigateBack();
                }
                return comparison_report;
            }
            catch (Exception ex)
            {
                ExtentReportsHelper.LogWarning($"Encountered exception while capturing comparison of Export file using Beyond Compare: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Use Beyond Compare to verify export file
        /// </summary>
        /// <param name="export"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int ValidateExportFileByBeyondCompare(string exportFileName, TableType format)
        {
            string file_name = GetFullExportFileName(exportFileName, format);
            string exported_file_path = CommonHelper.GetFullDownLoadFilePath(file_name);
            string baseline_file_path = BaseValues.BaselineFilesDir + $@"\{file_name}";

            try
            {
                string compare_result = WindowsSystemHelper.GetBeyondCompareResult(exported_file_path, baseline_file_path, true);

                int comparison_code = int.Parse(compare_result);
                if (format == TableType.XML && comparison_code == XML_IGNORE_CODE)
                    comparison_code = 1;

                bool comparison_success = SUCCESS_CODES.Contains(comparison_code);

                if (!comparison_success)
                    ExtentReportsHelper.LogFail(null, $"Beyond Compare returned unequal comparison between the exported '{file_name}' file and the baseline '{file_name}' test file - Result: Differences found (Return code = {compare_result})");
                else
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Beyond Compare returned succesfull status. The export data on '{file_name}' is same as Baseline file.</b></font>");

                return comparison_code; //1 = binary is equal, 2 = rules return equal
            }
            catch (Exception ex)
            {
                ExtentReportsHelper.LogFail($"Encountered exception while capturing comparison of Export file using Beyond Compare: {ex.Message}");
                return -1;
            }
        }

        /// <summary>
        /// Read CSV file and return total number
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="file_path"></param>
        /// <param name="expectedTitle"></param>
        /// <returns></returns>
        public static int ReadCSVFile(string file_name, string file_path, string expectedTitle)
        {
            int totalNumber = 0;
            string actualTitle = string.Empty;
            bool isheader = true;

            try
            {
                var file = new StreamReader(File.OpenRead(file_path));

                while (!file.EndOfStream)
                {
                    // Read each line on file
                    var line = file.ReadLine();

                    if (isheader)
                    {
                        // Get the title
                        isheader = false;
                        actualTitle = line;
                    }
                    else
                    {
                        if (line.StartsWith("\"") is false)
                        {
                            // Ignore these line that's to0 long and break the line by user, then validate if it starts with " , that's a item
                            // Example:
                            // Name,Description
                            // "aaa","bbb"
                            // "aaa1","bbb
                            // ccc" => ignore this one
                            continue;
                        }
                        totalNumber++;

                    }
                }

                file.Close();

                // Verify the title
                if (expectedTitle.ToLower().Equals(actualTitle.ToLower()))
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>On the <b>CSV</b> export file, The title on fist line displays same as the expected.</b>" +
                        $"<br>Title: <b>{actualTitle}</b></br></font>");
                else
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>On the <b>CSV</b> export file, the title on the fist line <b>isn't</b> same as the expected." +
                        $"<br>The expected: {expectedTitle}</br>" +
                        $"<br>The actual result: {actualTitle}</br></font>");

            }
            catch (Exception ex)
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Encountered exception while reading '{file_name}', " +
                    $"verify the file's contents - Exception: {ex.Message}</font>");
            }
            return totalNumber;
        }

        /// <summary>
        /// Read Excel file and return total number
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="file_path"></param>
        /// <param name="expectedTitle"></param>
        /// <returns></returns>
        public static int ReadExcelFile(string sheetName, string file_path, string expectedTitle)
        {
            int totalNumber = 0;
            try
            {
                var stream = File.Open(file_path, FileMode.Open, FileAccess.Read);
                var reader = ExcelReaderFactory.CreateReader(stream);

                totalNumber = reader.RowCount - 1;
                string actualTitle = string.Empty;

                // Read first sheet only
                reader.Read();

                // Read the first row only
                for (int column = 0; column < reader.FieldCount; column++)
                {
                    string cellValue = reader.GetValue(column).ToString();
                    actualTitle = string.IsNullOrEmpty(actualTitle) ? cellValue : actualTitle + "," + cellValue;
                }

                // Verify the title
                if (expectedTitle.ToLower().Equals(actualTitle.ToLower()))
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>On the <b>Excel</b> export file, The title on fist line displays same as the expected.</b>" +
                        $"<br>Title: <b>{actualTitle}</b></br></font>");
                else
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>The title on the <b>Excel</b> export file <b>isn't</b> same as the expected." +
                        $"<br>The expected: {expectedTitle}</br>" +
                        $"<br>The actual result: {actualTitle}</br></font>");
            }
            catch (Exception ex)
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Encountered exception while reading '{sheetName}' sheet on Excel file, " +
                    $"verify the file's contents - Exception: {ex.Message}</font>");
            }
            return totalNumber;
        }

        /// <summary>
        /// Compare column totals in file to given totals
        /// </summary>
        /// <param name="file_name"></param>
        /// <param name="file_path"></param>
        /// <param name="expectedColumnTotals"></param>
        public static void ReadFileAndVerifyColumnTotals(string file_name, string file_path, Dictionary<string, double> expectedColumnTotals)
        {
            var calculatedColumnTotals = new Dictionary<string, double>();
            try
            {
                // Determine the file extension
                var extension = Path.GetExtension(file_path).ToLower();

                // Process CSV file
                if (extension == ".csv")
                {
                    calculatedColumnTotals = CalculateCSVColumnTotals(file_path, expectedColumnTotals);
                }
                // Process Excel file
                else if (extension == ".xlsx")
                {
                    calculatedColumnTotals = CalculateExcelColumnTotals(file_path, expectedColumnTotals);
                }
                else
                {
                    throw new Exception("Unsupported file type.");
                }

                // Verify the calculated totals against the expected totals
                foreach (var columnName in expectedColumnTotals.Keys)
                {
                    if (Math.Abs(expectedColumnTotals[columnName] - calculatedColumnTotals[columnName]) > 0.01) // Tolerance for floating point comparison
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>Mismatch in totals for column '{columnName}'." +
                            $"<br>Expected: {expectedColumnTotals[columnName]}</br>" +
                            $"<br>Calculated: {calculatedColumnTotals[columnName]}</br></font>");
                        Assert.Fail();
                    }
                    else
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The total for column '{columnName}' matches the expected value.</font>");
                    }
                }
            }
            catch (Exception ex)
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Encountered exception while verifying totals for '{file_name}', " +
                    $"verify the file's contents - Exception: {ex.Message}</font>");
                Assert.Fail();
            }
        }

        /// <summary>
        /// Read CSV file & calculate column totals
        /// </summary>
        /// <param name="file_path"></param>
        /// <param name="expectedColumnTotals"></param>
        public static Dictionary<string, double> CalculateCSVColumnTotals(string file_path, Dictionary<string, double> expectedColumnTotals)
        {
            var calculatedColumnTotals = new Dictionary<string, double>();
            bool isHeader = true;
            string[] headers = null;

            using (var file = new StreamReader(File.OpenRead(file_path)))
            {
                while (!file.EndOfStream)
                {
                    var line = file.ReadLine();
                    var values = line.Split(',');

                    if (isHeader)
                    {
                        isHeader = false;
                        headers = values;
                        foreach (var columnName in expectedColumnTotals.Keys)
                        {
                            int columnIndex = Array.IndexOf(headers, columnName);
                            if (columnIndex == -1)
                                throw new Exception($"Column '{columnName}' not found.");
                            calculatedColumnTotals[columnName] = 0;
                        }
                    }
                    else
                    {
                        if (!line.StartsWith("\""))
                            continue;
                        foreach (var columnName in expectedColumnTotals.Keys)
                        {
                            int columnIndex = Array.IndexOf(headers, columnName);
                            string numberString = values[columnIndex].Trim(new char[] { ' ', '\"' });
                            numberString = numberString.Replace(',', '.');
                            if (double.TryParse(numberString, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                            {
                                calculatedColumnTotals[columnName] += number;
                            }
                        }
                    }
                }
            }
            return calculatedColumnTotals;
        }

        /// <summary>
        /// Read XLSX file & calculate column totals
        /// </summary>
        /// <param name="file_path"></param>
        /// <param name="expectedColumnTotals"></param>
        public static Dictionary<string, double> CalculateExcelColumnTotals(string file_path, Dictionary<string, double> expectedColumnTotals)
        {
            var calculatedColumnTotals = new Dictionary<string, double>();
            string[] headers = null;

            using (var stream = File.Open(file_path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Read the first row with headers
                    reader.Read();
                    headers = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetValue(i)?.ToString()).ToArray();

                    // Initialize the calculatedColumnTotals dictionary
                    foreach (var columnName in expectedColumnTotals.Keys)
                    {
                        int columnIndex = Array.IndexOf(headers, columnName);
                        if (columnIndex == -1)
                            throw new Exception($"Column '{columnName}' not found.");
                        calculatedColumnTotals[columnName] = 0;
                    }

                    // Read the rest of the rows and process them
                    while (reader.Read())
                    {
                        for (int i = 0; i < headers.Length; i++)
                        {
                            var columnName = headers[i];
                            if (expectedColumnTotals.ContainsKey(columnName))
                            {
                                string numberString = reader.GetValue(i)?.ToString().Trim(new char[] { ' ', '\"' });
                                numberString = numberString.Replace(',', '.');
                                if (double.TryParse(numberString, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                                {
                                    calculatedColumnTotals[columnName] += number;
                                }
                            }
                        }
                    }
                }
            }
            return calculatedColumnTotals;
        }

        /// <summary>
        /// Download basline file to compare
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        public static void DownloadBaseLineFile(string exportType, string exportFileName)
        {
            // Get file extension
            TableType format;
            if (exportType.ToLower().Contains("csv"))
                format = TableType.CSV;
            else if (exportType.ToLower().Contains("excel") || exportType.ToLower().Contains("xlsx"))
                format = TableType.XLSX;
            else
                format = TableType.XML;

            // Open Base file folder, delete file if it's existing
            string file_name = ValidationEngine.GetFullExportFileName(exportFileName, format);
            string baseLinePath = CommonHelper.GetFullBaseLineFilePath(file_name);

            try
            {

                if (Directory.Exists(CommonHelper.GetBaseLinePath()) is false)
                {
                    // Create new basline folder if it's existing
                    Directory.CreateDirectory(CommonHelper.GetBaseLinePath());
                }
                else
                {
                    // Check file is existing on baseline folder
                    if (File.Exists(baseLinePath))
                    {
                        // Delete file to export a new one
                        File.Delete(baseLinePath);
                    }
                }

                string reportPath = CommonHelper.GetFullDownLoadFilePath(file_name);

                // Wait for download file
                System.Threading.Thread.Sleep(4000);

                if (File.Exists(reportPath))
                {
                    // Move export file from report folder to Baseline folder
                    File.Move(reportPath, baseLinePath);
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>File {exportFileName} is downloaded unsuccessfully</font>");
                }
            }
            catch (Exception ex)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Encountered exception while exporting '{file_name}', verify the file's contents or update the baseline test files - Exception: {ex.Message}</font>");
            }

        }

        /// <summary>
        /// Export file from More menu
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        /// <param name="expectedTotalNumber"></param>
        public static void ValidateExportFile(string exportType, string exportFileName, string expectedTitle, int expectedTotalNumber)
        {
            System.Threading.Thread.Sleep(5000);
            TableType format;
            if (exportType.ToLower().Contains("csv"))
                format = TableType.CSV;
            else if(exportType.ToLower().Contains("excel"))
                format = TableType.XLSX;
            else
                format = TableType.XML;

            string fileName = GetFullExportFileName(exportFileName, format);
            string filePath = CommonHelper.GetFullDownLoadFilePath(fileName);
            try
            {
                if (File.Exists(filePath))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Successfully downloaded the '{fileName}' export file.</b></font>");
                    
                    // Don't verify total number and header if that's xml file
                    if (exportType.ToLower().Contains("xml"))
                        return;

                    ExportAndVerify(exportFileName, format, expectedTitle, expectedTotalNumber);
                }
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Export file '{fileName}' was not downloaded or the exported file does not match the expected file name.</font>");
            }
            catch (Exception)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Encountered exception while exporting '{fileName}'.</font>");
            }
        }

        /// <summary>
        /// Read and verify export file
        /// </summary>
        /// <param name="export"></param>
        /// <param name="format"></param>
        /// <param name="expectedItem"></param>
        /// <param name="expectedTotalItem"></param>
        public static void ExportAndVerify(string exportFileName, TableType format, string expectedItem, int expectedTotalItem)
        {
            string file_name = GetFullExportFileName(exportFileName, format);
            string file_path = CommonHelper.GetFullDownLoadFilePath(file_name);

            // Wait until file downloaded successfully
            System.Threading.Thread.Sleep(4000);

            // Verify file is existing on the report folder
            if (File.Exists(file_path) is false)
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Export file <b>'{file_name}'</b> was not downloaded or the exported file does not match the expected file name.</font>");
                return;
            }

            string content = File.ReadAllText(file_path, Encoding.UTF8);
            if (string.IsNullOrEmpty(content) is true)
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Can't read the <b>'{file_name}'</b> file on location: <b>{CommonHelper.GetFullFilePath("Download")}</b>" +
               $"<br>The export file is empty.</font>");
                return;
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Open the export {format} file to verify the header and total number items.</b></font>");
            // Open file and verify the content
            int totalRow;
            if (format.ToString().Contains("CSV"))
            {
                // CSV file
                totalRow = ReadCSVFile(file_name, file_path, expectedItem);
            }
            else
            {
                // Excel file
                totalRow = ReadExcelFile(file_name, file_path, expectedItem);
            }

            // Don't verify total Item if it's not nessary
            if (expectedTotalItem == 0)
                return;

            // Verify total number of items
            if (expectedTotalItem.Equals(totalRow))
                ExtentReportsHelper.LogPass($"<font color='green'><b>Total number of items found in {format} file is the same as UI: {totalRow - 1}.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='green'>Total number on export and UI are different." +
                    $"<br>on ui: <b>{expectedTotalItem}</b>" +
                    $"<br>on export file: <b>{totalRow}</b></br></font>");
        }

        public static bool VerifyDownloadedFileExist(string fileName)
        {
            string file_path = CommonHelper.GetFullDownLoadFilePath(fileName);

            // Wait until file downloaded successfully
            System.Threading.Thread.Sleep(1500);

            // Verify file is existing on the report folder
            if (File.Exists(file_path) is false)
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Export file <b>'{fileName}'</b> was not downloaded.</font>");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
