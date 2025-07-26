using System;
using System.Globalization;

namespace WordDocumentCompleting2019.Helpers
{
    public static class Converter
    {
        public static string ToShamsi(this DateTime date)
        {
            // If the date is UTC, we don't convert because we don't know the timezone? 
            // We leave it to the caller to convert first.
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(date);
            int month = pc.GetMonth(date);
            int day = pc.GetDayOfMonth(date);
            return $"{year:0000}/{month:00}/{day:00}";
        }
    }
}