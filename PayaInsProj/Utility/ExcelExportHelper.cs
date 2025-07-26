using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PayaInsProj.Models;
using PayaInsProj.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PayaInsProj.Utility
{
    public static class ExcelExportHelper
    {
        private static string GetDisplayName(PropertyInfo propertyInfo)
        {
            string result = "unknown";
            try
            {
                object[] attrs = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), true);
                if (attrs.Any())
                    result = ((DisplayAttribute)attrs[0]).Name;
            }
            catch (Exception)
            {
                //eat the exception
            }
            return result;
        }
        private static ISheet GetWorksheet(IWorkbook workbook, string sheetName, int? sheetIndex)
        {
            return !string.IsNullOrEmpty(sheetName) ? workbook.GetSheet(sheetName) :
                sheetIndex.HasValue ? workbook.GetSheetAt(sheetIndex.Value) :
                   workbook.GetSheetAt(0);
        }

        private static List<string> GetHeadersFromFirstRow(ISheet worksheet)
        {
            var headers = new List<string>();
            var headerRow = worksheet.GetRow(0);
            if (headerRow == null) return headers;

            for (int cellIndex = 0; cellIndex < headerRow.LastCellNum; cellIndex++)
            {
                var cell = headerRow.GetCell(cellIndex);
                headers.Add(cell == null ? string.Empty : cell.ToString().Trim());
            }

            return headers;
        }
        public static (string Root, string FileName) WriteExcelWithNPOI<T>(T Entity, List<T> data, string Root, List<string> SelectedCols, string title, string SheetName = "Sheet1", string FileName = "ExcelFile", bool showOrder = false)
        {
            string extension = "xlsx";
            // Get DataTable
            DataTable dt = ConvertListToDataTable(data, SelectedCols);
            // Instantiate Wokrbook
            IWorkbook workbook;
            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("The format '" + extension + "' is not supported.");
            }
            //make top row
            ISheet sheet1 = workbook.CreateSheet(SheetName);
            sheet1.IsRightToLeft = true;
            IFont TopRowFont = workbook.CreateFont();
            TopRowFont.FontName = "topFont";
            TopRowFont.IsBold = true;
            TopRowFont.FontHeight = 350;

            IRow topRow = sheet1.CreateRow(0);
            var CellStyleTop = workbook.CreateCellStyle();
            CellStyleTop.Alignment = HorizontalAlignment.Center;
            CellStyleTop.VerticalAlignment = VerticalAlignment.Center;
            CellStyleTop.SetFont(TopRowFont);
            ICell cellTop = topRow.CreateCell(0);
            cellTop.CellStyle = CellStyleTop;
            cellTop.SetCellValue(title);

            var cra = new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dt.Columns.Count - 1);
            sheet1.AddMergedRegion(cra);

            //make a header row
            IFont font1 = workbook.CreateFont();
            font1.FontName = "Font1";
            font1.IsBold = true;
            font1.Color = IndexedColors.Black.Index;

            IRow row1 = sheet1.CreateRow(1);
            var CellStyleHeader = workbook.CreateCellStyle();
            CellStyleHeader.Alignment = HorizontalAlignment.Center;
            CellStyleHeader.VerticalAlignment = VerticalAlignment.Center;

            // center-align currency values
            CellStyleHeader.Alignment = HorizontalAlignment.Center;
            CellStyleHeader.VerticalAlignment = VerticalAlignment.Center;
            CellStyleHeader.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            CellStyleHeader.FillPattern = FillPattern.SolidForeground;
            CellStyleHeader.SetFont(font1);

            var CellStyleBody = workbook.CreateCellStyle();
            // center-align currency values
            CellStyleBody.Alignment = HorizontalAlignment.Center;
            CellStyleBody.VerticalAlignment = VerticalAlignment.Center;

            ICell cell0 = row1.CreateCell(0);

            string Title0 = "ردیف";
            if (!string.IsNullOrEmpty(Title0))
            {
                cell0.SetCellValue(Title0);
                cell0.CellStyle = CellStyleHeader;
            }


            PropertyInfo[] props = Entity.GetType().GetProperties();
            PropertyInfo pKey = props.SingleOrDefault(s => s.IsKey());
            props = props.Where(w => !w.IsKey()).ToArray();
            if (SelectedCols.Count != 0)
            {
                props = props.Where(w => SelectedCols.Any(a => a == w.Name)).ToArray();
            }

            int colN = 1;
            foreach (var p in props)
            {
                ICell cell = row1.CreateCell(colN);
                string Title = GetDisplayName(props[colN - 1]);
                if (!string.IsNullOrEmpty(Title))
                {
                    cell.SetCellValue(Title);
                    cell.CellStyle = CellStyleHeader;
                }
                colN++;
            }

            //loops through data

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 2);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string columnName = dt.Columns[j].ToString();
                    if (columnName != pKey?.Name)
                    {
                        ICell cell = row.CreateCell(j);
                        string columnValue = dt.Rows[i][columnName].ToString();
                        Type tpe = dt.Rows[i][columnName].GetType();

                        if (tpe.Equals(typeof(bool)))
                        {
                            if (columnValue == "True")
                            {
                                columnValue = "بله";
                            }
                            else if (columnValue == "False")
                            {
                                columnValue = "خیر";
                            }

                        }


                        //string Title = GetDisplayName(props[j]);
                        string[] cellval = columnValue.Split('|');
                        string nstr = string.Empty;
                        int loop = 1;
                        foreach (var item in cellval)
                        {
                            if (item != cellval.LastOrDefault())
                            {
                                nstr += $"{item}\n";
                            }
                            else
                            {
                                nstr += item;
                            }
                            loop++;
                        }
                        cell.SetCellValue(nstr);

                        ICellStyle cs = workbook.CreateCellStyle();
                        cs.Alignment = HorizontalAlignment.Center;
                        cs.VerticalAlignment = VerticalAlignment.Center;
                        cs.WrapText = true;
                        cs.ShrinkToFit = true;
                        cell.CellStyle = cs;
                    }
                    else
                    {

                        ICell cell = row.CreateCell(j);
                        string columnValue = (i + 1).ToString();
                        cell.SetCellValue(columnValue);
                        ICellStyle cs = workbook.CreateCellStyle();
                        cs.Alignment = HorizontalAlignment.Center;
                        cs.VerticalAlignment = VerticalAlignment.Center;
                        cs.WrapText = true;
                        cs.ShrinkToFit = true;
                        cell.CellStyle = cs;


                    }

                }
                for (int j = 0; j < row1.LastCellNum; j++)
                {
                    sheet1.AutoSizeColumn(j);
                }
            }
            string FName = FileName + "." + extension;
            //string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), root, ImageName);
            string root = Root;
            string Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), Root, FName);
            //string Path = "wwwroot/Files/" + FileName + "." + extension;
            FileStream fs = new FileStream(Path, FileMode.Create, FileAccess.Write);
            ISheet excelSheet = sheet1;//workbook.CreateSheet("Sheet1");                
            workbook.Write(fs);
            fs.Dispose();
            return (Path, FName);
        }
        public static void GenerateExcel<T>(List<T> data, string filePath)
        {
            // Create Workbook and Sheet
            IWorkbook workbook = new XSSFWorkbook(); // .xlsx
            ISheet sheet = workbook.CreateSheet("MyData");

            // Create Header Row with Bold Style
            var headerStyle = workbook.CreateCellStyle();
            IFont headerFont = workbook.CreateFont();
            headerFont.IsBold = true;
            headerStyle.SetFont(headerFont);

            IRow headerRow = sheet.CreateRow(0);
            PropertyInfo[] properties = typeof(T).GetProperties();

            // Write Headers
            for (int i = 0; i < properties.Length; i++)
            {
                ICell headerCell = headerRow.CreateCell(i);
                headerCell.SetCellValue(properties[i].Name);
                headerCell.CellStyle = headerStyle;
            }

            // Write Data Rows
            int rowIndex = 1;
            foreach (T t in data)
            {
                IRow row = sheet.CreateRow(rowIndex++);
                for (int i = 0; i < properties.Length; i++)
                {
                    object value = properties[i].GetValue(t);
                    ICell cell = row.CreateCell(i);

                    // Handle Data Types
                    switch (value)
                    {
                        case DateTime dateVal:
                            cell.SetCellValue(dateVal);
                            // Format Date Cells
                            ICellStyle dateStyle = workbook.CreateCellStyle();
                            dateStyle.DataFormat = workbook.CreateDataFormat().GetFormat("dd/MM/yyyy");
                            cell.CellStyle = dateStyle;
                            break;
                        case int intVal:
                            cell.SetCellValue(intVal);
                            break;
                        default:
                            cell.SetCellValue(value?.ToString() ?? "");
                            break;
                    }
                }
            }

            // Auto-Size Columns
            for (int i = 0; i < properties.Length; i++)
                sheet.AutoSizeColumn(i);

            // Save to File
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                workbook.Write(stream);
        }

        public static DataTable ConvertListToDataTable<T>(List<T> Data, List<string> SelectedCols)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                if (SelectedCols != null)
                {
                    if (prop.IsKey())
                    {
                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }
                    else
                    {
                        if (SelectedCols.Any(a => a == prop.Name))
                        {
                            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                        }
                    }

                }
                else
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

            }

            foreach (T item in Data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {

                    if (SelectedCols != null)
                    {
                        if (prop.IsKey())
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        else
                        {
                            if (SelectedCols.Any(a => a == prop.Name))
                            {
                                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                            }
                        }
                    }
                    else
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }

                }

                table.Rows.Add(row);
            }
            return table;
        }
        public static List<string> GetExcelHeaders(HttpPostedFileBase file, string sheetName = null, int? sheetIndex = null)
        {
            List<string> headers = new List<string>();

            // Check if file exists and has content
            if (file == null )
            {
                return headers;
            }

            // Get the file extension
            string fileExtension = Path.GetExtension(file.FileName).ToLower();

            IWorkbook workbook;

            // Create workbook based on file extension
            using (Stream stream = file.InputStream)
            {
                if (fileExtension == ".xlsx")
                {
                    workbook = new XSSFWorkbook(stream);
                }
                else if (fileExtension == ".xls")
                {
                    workbook = new HSSFWorkbook(stream);
                }
                else
                {
                    // Not an Excel file
                    return headers;
                }
                
                // Get the first worksheet
                ISheet sheet = GetWorksheet(workbook, sheetName, sheetIndex);

                // Get the first row (header row)
                IRow headerRow = sheet.GetRow(0);

                if (headerRow != null)
                {
                    // Loop through cells in the header row
                    foreach (ICell cell in headerRow.Cells)
                    {
                        string header = cell.ToString();
                        headers.Add(header);
                    }
                }
            }

            return headers;
        }
        
        public static (bool Valid, List<string> Diffences) CheckUploadedExcelFile(this HttpPostedFileBase formFile, List<string> baseColumnHeaders, string SheetName, int? SheetIndex)
        {
            List<string> fileHeaders =  GetExcelHeaders(formFile, sheetName: SheetName, sheetIndex: SheetIndex);
            if (baseColumnHeaders.SequenceEqual(fileHeaders))
            {
                return (true, null);
            }
            else
            {
                List<string> diff = baseColumnHeaders.Except(fileHeaders).ToList();
                return (false, diff);
            }
        }
        public static (bool isEqual, List<string> Differences) EvaluationFileHeaders(HttpPostedFileBase formFile, List<string> desiredHeaders, string sheetName, int? sheetIndex)
        {
            List<string> fileHeaders =  GetExcelHeaders(formFile, sheetName: sheetName, sheetIndex: sheetIndex);
            if (desiredHeaders.Count != 0)
            {
                if (desiredHeaders.SequenceEqual(fileHeaders))
                {
                    return (true, null);
                }
                else
                {
                    List<string> diff = desiredHeaders.Except(fileHeaders).ToList();
                    return (false, diff);
                }
            }
            else
            {
                return (true, null);
            }
            
        }
        /// <summary>
        /// بررسی صحت نوع فایل
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="Extensions"></param>
        /// <returns></returns>
        public static bool FileExtensionIsCorrect(this HttpPostedFileBase formFile, string[] Extensions)
        {
            bool valid = true;
            var fileExtension = Path.GetExtension(formFile.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(fileExtension) || !Extensions.Contains(fileExtension))
                valid = false;
            return valid;
        }
        /// <summary>
        /// چک حجم فایل بر اساس مگا بایت
        /// </summary>
        /// <param name="formFile">فایل آپلود شده</param>
        /// <param name="MaxVolum">حجم بر اساس مگابایت</param>
        /// <returns></returns>
        public static bool CheckFileVolume(this HttpPostedFileBase formFile, int MaxVolum)
        {
            bool res = true;
            if (formFile.ContentLength > MaxVolum * 1024 * 1024)
            {
                res = false;
            }
            return res;
        }
        public static (bool valid, List<string> validMessages) ValidationUploadedExcelFile(string SheetName, int SheetIndex, HttpPostedFileBase formFile, List<string> correctHeders, int maxVolum = 200)
        {
            //(bool valid, List<string> validMessages) Result = new (bool valid, List<string> validMessages)();
            
            ExcelValidation validation = new ExcelValidation();
            if (formFile != null)
            {
                if (CheckFileVolume(formFile, maxVolum))
                {
                    if (formFile.FileExtensionIsCorrect(new string[]{ ".xls", ".xlsx"}))
                    {
                        var evRes =  EvaluationFileHeaders(formFile, correctHeders, SheetName, SheetIndex);
                        if (evRes.isEqual)
                        {
                            List<string> fileHeaders = GetExcelHeaders(formFile, sheetName: SheetName, sheetIndex: SheetIndex);
                            (bool IsEqual,List<string> Messages) Ev =  EvaluationFileHeaders(formFile, fileHeaders, SheetName, SheetIndex);

                            if (!Ev.IsEqual)
                            {
                                validation.Valid = false;
                                validation.ValidationMessages.AddRange(Ev.Messages);
                            }
                            
                        }
                        else
                        {
                            validation.Valid = false;
                            validation.ValidationMessages.Add("عناوین ستونهای فایل با عناوین درست اختلاف دارند !");
                            string Diffs = string.Empty;
                            int loop = 1;
                            foreach (var item in evRes.Differences)
                            {
                                if (item == evRes.Differences.FirstOrDefault())
                                {
                                    Diffs = "\n\r" + loop.ToString() + "-" + item;
                                }
                                else
                                {
                                    Diffs += "\n\r" + loop.ToString() + "-" + item;
                                }
                                loop++;
                            }
                            string errMess = string.Empty;
                            if (evRes.Differences.Count != 0)
                            {
                                if (Diffs.Length == 1)
                                {
                                    errMess = "\n\r" + "عنوان مورد اختلاف " + Diffs + "\n\r می باشد";
                                }
                                else
                                {
                                    errMess = "\n\r" + "عناوین مورد اختلاف " + Diffs + "\n\r هستند !";
                                }

                            }
                            validation.ValidationMessages.Add(errMess);
                        }
                    }
                    else
                    {
                        validation.Valid = false;
                        validation.ValidationMessages.Add("نوع فایل اشتباه است !");
                    }
                }
                else
                {
                    validation.Valid = false;
                    validation.ValidationMessages.Add($"حجم فایل بیشتر از {maxVolum} مگابایت است !");
                }
            }
            else
            {
                validation.Valid = false;
                validation.ValidationMessages.Add("فایل معرفی نشده است !");
            }
            return (validation.Valid,validation.ValidationMessages);
        }


        /// <summary>
        /// خواندن داده های فایل اکسل آپلود شده به صورت 
        /// <para>رشته جیسون</para>
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string ReadUploadedExcelAsJson(HttpPostedFileBase file)
        {
            var inputstream = file.InputStream;
            XSSFWorkbook workbook = new XSSFWorkbook(inputstream);
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();

            ISheet sheet = workbook.GetSheetAt(0);
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;
            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                {
                    dtTable.Columns.Add(cell.ToString());
                }
            }
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;
                if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        string v = row.GetCell(j).ToString();
                        if (!string.IsNullOrEmpty(row.GetCell(j).ToString()))
                        {
                            rowList.Add(row.GetCell(j).ToString().Replace("ي", "ی"));
                        }
                        else
                        {
                            rowList.Add(row.GetCell(j).ToString());
                        }
                    }
                }
                if (rowList.Count > 0)
                    dtTable.Rows.Add(rowList.ToArray());
                rowList.Clear();
            }

            return JsonConvert.SerializeObject(dtTable);
        }
        /// <summary>
        /// خواندن داده های فایل اکسل آپلود شده به صورت 
        /// <para>Data Table</para>
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ReadUploadedExcelAsDataTable(HttpPostedFileBase file)
        {
            IWorkbook workbook;

            // Copy the input stream to a MemoryStream first
            using (MemoryStream ms = new MemoryStream())
            {
                file.InputStream.CopyTo(ms, bufferSize: 81920);
                ms.Position = 0; // Reset position to beginning

                if (Path.GetExtension(file.FileName).ToLower() == ".xlsx")
                    workbook = new XSSFWorkbook(ms);
                else
                    workbook = new HSSFWorkbook(ms);
            }

            ISheet sheet = workbook.GetSheetAt(0);
            DataTable dt = new DataTable();

            // Header row
            IRow headerRow = sheet.GetRow(0);
            for (int i = 0; i < headerRow.LastCellNum; i++)
            {
                dt.Columns.Add(headerRow.GetCell(i)?.ToString() ?? $"Column{i}");
            }

            // Data rows
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;

                DataRow dataRow = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dataRow[j] = row.GetCell(j)?.ToString();
                }
                dt.Rows.Add(dataRow);
            }
            
            return dt;
        }
        /// <summary>
        /// خواندن داده های فایل اکسل موجود در سرور به صورت 
        /// <para>رشته جیسون</para>
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string ReadExistExcelFileAsJson(string root)
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
            using (var stream = new FileStream(root, FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    {
                        dtTable.Columns.Add(cell.ToString());
                    }
                }
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) & !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                            {
                                rowList.Add(row.GetCell(j).ToString().Replace("ي", "ی"));
                            }
                            else
                            {
                                rowList.Add(string.Empty);
                            }
                        }
                        else
                        {
                            rowList.Add(string.Empty);
                        }
                    }
                    if (rowList.Count > 0)
                        //dtTable.Rows.Add(rowList.ToArray());
                        dtTable.Rows.Add(rowList.ToList());
                    rowList.Clear();
                }
                xssWorkbook.Close();
                stream.Close();
            }

            return JsonConvert.SerializeObject(dtTable);

        }
        /// <summary>
        /// خواندن داده های فایل اکسل موجود در سرور به صورت 
        /// <para>Data Table</para>
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DataTable ReadExistExcelFileAsDatatable(string root)
        {
            DataTable dtTable = new DataTable();
            List<string> rowList = new List<string>();
            ISheet sheet;
            using (var stream = new FileStream(root, FileMode.Open))
            {
                stream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int j = 0; j < cellCount; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                    {
                        dtTable.Columns.Add(cell.ToString());
                    }
                }
                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) & !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                            {
                                rowList.Add(row.GetCell(j).ToString().Replace("ي", "ی"));
                            }
                            else
                            {
                                rowList.Add(string.Empty);
                            }
                        }
                        else
                        {
                            rowList.Add(string.Empty);
                        }
                    }
                    if (rowList.Count > 0)
                        dtTable.Rows.Add(rowList.ToArray());
                        
                    rowList.Clear();
                }
                xssWorkbook.Close();
                stream.Close();
            }

            return dtTable;

        }
        public static List<ExcelFilePropDTo> MapExcelDataTableToList(string root)
        {
            DataTable dt = ReadExistExcelFileAsDatatable(root);
            //var props = ClassAttributesHelper.GetObjectPropertyNameAndDisplayNameAndValue<ExcelFilePropDTo>(new ExcelFilePropDTo(),false);

            List<ExcelFilePropDTo> excelFilePropDTos = new List<ExcelFilePropDTo>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                ExcelFilePropDTo excelFilePropDTo = new ExcelFilePropDTo()
                {
                    Radif = dt.Rows[i][0].ToString(),
                    IssuedDate = dt.Rows[i][1].ToString(),
                    Insurer = dt.Rows[i][2].ToString(),
                    CarType = dt.Rows[i][3].ToString(),
                    IssueNumber = dt.Rows[i][4].ToString(),
                    IssueBeginDate = dt.Rows[i][5].ToString(),
                    IssueEndDate = dt.Rows[i][6].ToString(),
                    Plaque = dt.Rows[i][7].ToString(),
                    Engine = dt.Rows[i][8].ToString(),
                    Chasis = dt.Rows[i][9].ToString(),
                    NutInsValuewithTax = dt.Rows[i][10].ToString(),
                    InsurerNC = dt.Rows[i][11].ToString(),
                    InsYear = dt.Rows[i][12].ToString(),
                };
                excelFilePropDTos.Add(excelFilePropDTo);
            }
            return excelFilePropDTos;
        }
    }
}