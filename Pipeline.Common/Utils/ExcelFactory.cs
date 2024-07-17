using System.Collections.Generic;
using System.Linq;
using LinqToExcel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;

namespace Pipeline.Common.Utils
{
    public class ExcelFactory
    {
        private ExcelQueryFactory excelQueryFactory;
        private XSSFWorkbook excelWriter;
        private string filePath;

        public ExcelFactory(string excelFilePath)
        {
            filePath = excelFilePath;

            excelQueryFactory = new ExcelQueryFactory(filePath);
            excelQueryFactory.ReadOnly = true;
            excelQueryFactory.UsePersistentConnection = false;
            excelQueryFactory.TrimSpaces = LinqToExcel.Query.TrimSpacesType.Both;
            excelQueryFactory.DatabaseEngine = LinqToExcel.Domain.DatabaseEngine.Ace;

            excelWriter = new XSSFWorkbook();
        }

        public void Dispose()
        {
            excelQueryFactory.Dispose();
            excelWriter.Dispose();
        }

        #region "Query/Read Excel"
        public IEnumerable<string> GetColumnNames(string worksheetName)
        {
            return excelQueryFactory.GetColumnNames(worksheetName);
        }

        public static IList<Row> GetListRowsByValue(IQueryable<Row> rows, string columnName, string value)
        {
            var items = from r in rows
                        where r[columnName] == value
                        select r;

            return items.ToList();
        }

        public IEnumerable<string> GetWorkSheetNames()
        {
            return excelQueryFactory.GetWorksheetNames();
        }

        public IQueryable<Row> GetAllRows(string worksheetName)
        {
            var rows = from c in excelQueryFactory.Worksheet(worksheetName)
                       select c;
            return rows;
        }

        public Row GetRow(string worksheetName, int rowIndex)
        {
            var rows = from r in excelQueryFactory.Worksheet(worksheetName)
                       where r["Row Index"].Cast<int>() == rowIndex
                       select r;

            return rows.FirstOrDefault();
        }


        public static Row GetRow(IQueryable<Row> rows, int rowIndex)
        {
            var items = from r in rows
                        where r["Row Index"].Cast<int>() == rowIndex
                        select r;

            return items.FirstOrDefault();
        }

        public static IList<string> GetListByColumn(IQueryable<Row> rows, string columnName)
        {
            var items = from r in rows
                        select r[columnName].ToString();
            return items.ToList();
        }

        public static string GetCellValueByColumnName(Row row, string columnName)
        {
            return row[columnName];
        }

        public static string GetCellValueByColumnIndex(Row row, string columnIndex)
        {
            return row[columnIndex];
        }
        #endregion

        #region "Wrire Excel"
        public void AddRow(string worksheetName, IList<object> cellValues)
        {
            ISheet sheet = excelWriter.GetSheet(worksheetName);
            if (sheet == null)
            {
                var columnNames = new List<string>() { "Care Activity", "Transaction", "Response Time (in sec)" };
                sheet = CreateSheet(worksheetName, columnNames);
            }
            CreateRow(sheet, cellValues);
            WriteToFile();
        }

        public void MergeRows(string worksheetName, int columnIndex)
        {
            IRow row = null;
            ICell cell = null;
            CellRangeAddress region = null;
            IList<int> rows = null;

            ISheet sheet = excelWriter.GetSheet(worksheetName);
            if (sheet == null) return;

            Dictionary<string, IList<int>> dic = new Dictionary<string, IList<int>>();

            for (int i = 0; i < sheet.PhysicalNumberOfRows; i++)
            {
                row = sheet.GetRow(i);
                cell = row.Cells[columnIndex];

                if (!dic.ContainsKey(cell.StringCellValue))
                    dic.Add(cell.StringCellValue, new List<int> { i });
                else dic[cell.StringCellValue].Add(i);
            }

            foreach (var key in dic.Keys)
            {
                rows = dic[key];
                if (rows.Count > 1)
                {
                    region = new CellRangeAddress(rows[0], rows[rows.Count - 1], columnIndex, columnIndex);
                    sheet.AddMergedRegion(region);
                }
            }

            WriteToFile();
        }

        private ISheet CreateSheet(string worksheetName, IList<string> columnNames)
        {
            var sheet = excelWriter.CreateSheet(worksheetName);
            sheet.DisplayGridlines = true;
            CreateColumnHeaders(sheet, columnNames);
            return sheet;
        }

        private void CreateRow(ISheet sheet, IList<object> cellValues)
        {
            IRow row = sheet.CreateRow(sheet.PhysicalNumberOfRows);

            for (int i = 0; i < cellValues.Count; i++)
            {
                CreateCell(row, i, cellValues[i]);
            }
        }

        private void WriteToFile()
        {
            var folder = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                excelWriter.Write(fileStream);
                fileStream.Close();
            }
        }

        private void CreateColumnHeaders(ISheet sheet, IList<string> columnNames)
        {
            IRow row = sheet.CreateRow(0);

            for (int i = 0; i < columnNames.Count; i++)
            {
                CreateCell(row, i, columnNames[i], true);
                sheet.SetColumnWidth(i, 10000);
            }
        }

        private void CreateCell(IRow row, int columnIndex, object value, bool isHeaderCell = false)
        {
            XSSFCellStyle style = excelWriter.CreateCellStyle() as XSSFCellStyle;

            style.BorderBottom = style.BorderRight = style.BorderLeft = style.BorderTop = BorderStyle.Thin;
            style.BottomBorderColor = style.RightBorderColor = style.LeftBorderColor = style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

            if (isHeaderCell)
            {
                style.FillPattern = FillPattern.SolidForeground;
                style.FillForegroundColor = IndexedColors.Green.Index;

                IFont font = excelWriter.CreateFont();
                font.Color = IndexedColors.White.Index;
                font.FontHeightInPoints = 15;
                font.IsBold = true;
                style.SetFont(font);
            }

            ICell cell = row.CreateCell(columnIndex);

            System.Type type = value.GetType();
            if (type == typeof(int)) cell.SetCellValue(System.Convert.ToDouble(value));
            else if (type == typeof(double)) cell.SetCellValue((double)value);
            else if (type == typeof(string)) cell.SetCellValue((string)value);

            cell.CellStyle = style;
        }
        #endregion
    }
}
