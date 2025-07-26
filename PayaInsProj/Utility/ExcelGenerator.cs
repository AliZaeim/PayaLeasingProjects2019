using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.Streaming;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PayaInsProj.Utility
{
    public static class ExcelGenerator
    {
        public static void GenerateExcel<T>(string filePath, List<T> data, List<string> selectedCols, string sheetName, string caption = null)
        {
            string shName = "Data";
            if (!string.IsNullOrEmpty(sheetName))
            {
                shName = sheetName;
            }
            SXSSFWorkbook workbook = new SXSSFWorkbook(300); //==> Class SXSSFWorkbook. Streaming version of XSSFWorkbook implementing the "BigGridDemo" strategy. This allows to write very large files without running out of memory as only a configurable portion of the rows are kept in memory at any one time.
            var sheet = workbook.CreateSheet(shName);
            sheet.IsRightToLeft = true;
            var formatter = new DataFormatter();

            var headerStyle = CreateCenterAlignedStyle(workbook);
            var captionStyle = CreateCaptionStyle(workbook);
            var properties = typeof(T).GetProperties();
            int totalColumns = properties.Length + 1;
            var maxWidths = new int[totalColumns];

            int currentRowIndex = 0;

            // Add caption row if provided
            if (!string.IsNullOrEmpty(caption))
            {
                CreateCaptionRow(sheet, caption, totalColumns, captionStyle);
                currentRowIndex++;
            }

            // Create header row
            CreateHeaderRow(sheet, properties, headerStyle, formatter, ref maxWidths, currentRowIndex);
            currentRowIndex++;

            // Process data rows
            ProcessDataRows(sheet, data, properties, headerStyle, formatter, ref maxWidths, currentRowIndex);

            // Set column widths
            SetColumnWidths(sheet, maxWidths);

            SaveWorkbook(workbook, filePath);
            
        }

        private static ICellStyle CreateCenterAlignedStyle(SXSSFWorkbook workbook)
        {
            var style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            return style;
        }

        private static ICellStyle CreateCaptionStyle(SXSSFWorkbook workbook)
        {
            var font = workbook.CreateFont();
            font.IsBold = true;

            var style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.SetFont(font);
            return style;
        }

        private static void CreateCaptionRow(ISheet sheet, string caption, int totalColumns, ICellStyle style)
        {
            var row = sheet.CreateRow(0);
            var cell = row.CreateCell(0);
            cell.SetCellValue(caption);
            cell.CellStyle = style;
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, totalColumns - 1));
        }

        private static void CreateHeaderRow(ISheet sheet, PropertyInfo[] properties,
            ICellStyle style, DataFormatter formatter, ref int[] maxWidths, int rowIndex)
        {
            var headerRow = sheet.CreateRow(rowIndex);
            int colIndex = 0;

            // Order column header
            UpdateMaxWidth(headerRow, colIndex, "ردیف", style, formatter, ref maxWidths[colIndex]);
            colIndex++;

            // Property headers
            foreach (var prop in properties)
            {
                DisplayAttribute displayAttribute = prop.GetCustomAttributes(typeof(DisplayAttribute), false)
                                        .FirstOrDefault() as DisplayAttribute;
                //var displayName = prop.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? prop.Name;
                UpdateMaxWidth(headerRow, colIndex, displayAttribute?.Name ?? prop.Name, style, formatter, ref maxWidths[colIndex]);
                colIndex++;
            }
        }

        private static void ProcessDataRows<T>(ISheet sheet, List<T> data, PropertyInfo[] properties,
            ICellStyle style, DataFormatter formatter, ref int[] maxWidths, int startRowIndex)
        {
            for (int i = 0; i < data.Count; i++)
            {
                var row = sheet.CreateRow(startRowIndex + i);
                var item = data[i];
                int colIndex = 0;

                // Order column
                UpdateMaxWidth(row, colIndex, (i + 1).ToString(), style, formatter, ref maxWidths[colIndex]);
                colIndex++;

                // Data columns
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item);
                    UpdateMaxWidth(row, colIndex, value, style, formatter, ref maxWidths[colIndex]);
                    colIndex++;
                }
            }
        }

        private static void UpdateMaxWidth(IRow row, int colIndex, object value,
            ICellStyle style, DataFormatter formatter, ref int maxWidth)
        {
            var cell = row.CreateCell(colIndex);
            SetCellValue(cell, value, style);

            string formattedValue = formatter.FormatCellValue(cell);
            int cellWidth = formattedValue.Length;

            if (cellWidth > maxWidth)
            {
                maxWidth = cellWidth;
            }
        }

        private static void SetCellValue(ICell cell, object value, ICellStyle style)
        {
            cell.CellStyle = style;

            switch (value)
            {
                case null:
                    cell.SetCellValue((string)null);
                    break;
                case int intVal:
                    cell.SetCellValue(intVal);
                    break;
                case double doubleVal:
                    cell.SetCellValue(doubleVal);
                    break;
                case decimal decimalVal:
                    cell.SetCellValue((double)decimalVal);
                    break;
                case float floatVal:
                    cell.SetCellValue(floatVal);
                    break;
                case long longVal:
                    cell.SetCellValue(longVal);
                    break;
                case bool boolVal:
                    cell.SetCellValue(boolVal);
                    break;
                case DateTime dateVal:
                    cell.SetCellValue(dateVal);
                    break;
                default:
                    cell.SetCellValue(value.ToString());
                    break;
            }
        }

        private static void SetColumnWidths(ISheet sheet, int[] maxWidths)
        {
            for (int col = 0; col < maxWidths.Length; col++)
            {
                int width = CalculateColumnWidth(maxWidths[col]);
                sheet.SetColumnWidth(col, width);
            }
        }

        private static int CalculateColumnWidth(int contentLength)
        {
            const int minWidth = 3;
            const int maxWidth = 255;
            const int padding = 2;

            int calculatedWidth =MathHelper.Clamp(contentLength + padding, minWidth, maxWidth);
            return calculatedWidth * 256;
        }

        private static void SaveWorkbook(SXSSFWorkbook workbook, string filePath)
        {
            FileStream fileStream = File.Create(filePath);
            workbook.Write(fileStream);
            workbook.Dispose();
        }
    }
}