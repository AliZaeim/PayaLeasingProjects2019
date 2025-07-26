using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace PayaInsProj.Utility
{
    public static class ProjUtilities
    {
        private static T GetItem<T>(DataRow dr)
        {
            try
            {
                Type temp = typeof(T);
                T obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name == column.ColumnName)
                            pro.SetValue(obj, dr[column.ColumnName]);

                        else
                            continue;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                throw;
            }
        }
        /// <summary>
        /// تغییر نام ستون های 
        /// <para>Data Table</para>
        /// <para>از فارسی به انگلیسی، بر اساس مرجع معرفی شده از نوع دیکشنری</para>
        /// </summary>
        /// <param name="dtTable"></param>
        /// <param name="Columns"></param>
        /// <returns></returns>
        public static DataTable ChangeDataTableColumnsTitles(this DataTable dtTable, Dictionary<string, string> Columns)
        {
            try
            {
                foreach (DataColumn column in dtTable.Columns)
                {
                    if (Columns.ContainsKey(column.ColumnName))
                    {
                        column.ColumnName = Columns[column.ColumnName];
                    }

                }
                return dtTable;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }

        }
        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            try
            {
                List<T> data = new List<T>();
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
                return data;
            }
            catch (Exception ex)
            {
                string me = ex.Message;
                throw;
            }

        }
        public static string ConvertDateTimeToString(this DateTime dateTime)
        {
            string strDate = dateTime.ToString("yyyy-MM-ddTHH-mm-ss");
            return strDate;
        }
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" +
                   pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }
        public static DateTime ToGregorian(this string shamsiDate)
        {
            if (string.IsNullOrEmpty(shamsiDate))
            {
                return DateTime.MinValue;
            }
            string[] parts = shamsiDate.Split('/');
            if (parts.Length != 3)
                throw new ArgumentException("Invalid date format. Use 'yyyy/mm/dd'.");

            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            PersianCalendar persianCalendar = new PersianCalendar();
            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);

        }
        public static int GetDifferenceInDays(string date1, string date2, string format = "yyyy/MM/dd")
        {
            CultureInfo persianCulture = new CultureInfo("fa-IR");
            persianCulture.DateTimeFormat.Calendar = new PersianCalendar();

            DateTime d1 = DateTime.ParseExact(date1, format, persianCulture);
            DateTime d2 = DateTime.ParseExact(date2, format, persianCulture);

            return (int)(d1 - d2).TotalDays;
        }
        public static int GetDifferenceFromToday(this string persianDate, string format = "yyyy/MM/dd")
        {
            CultureInfo persianCulture = new CultureInfo("fa-IR");
            persianCulture.DateTimeFormat.Calendar = new PersianCalendar();

            // Parse the input Persian date
            DateTime inputDate = DateTime.ParseExact(persianDate, format, persianCulture);

            // Get today's date (local time zone, without time component)
            DateTime today = DateTime.Today;

            // Calculate the difference
            TimeSpan difference = inputDate - today;
            return difference.Days;
        }

        public static string ConvertToShamsiDate(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(date);
            int month = pc.GetMonth(date);
            int day = pc.GetDayOfMonth(date);
            return $"{year}/{month:00}/{day:00}";
        }
        public static string ConvertToShamsiDateTimeWithCulture(this DateTime date)
        {
            CultureInfo persianCulture = (CultureInfo)CultureInfo.GetCultureInfo("fa-IR").Clone();
            persianCulture.DateTimeFormat.Calendar = new PersianCalendar();
            // Format string includes date (yyyy/MM/dd) and time (HH:mm:ss)
            return date.ToString("yyyy/MM/dd HH:mm:ss", persianCulture);
        }


    }
}