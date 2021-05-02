using System.Globalization;

namespace RF.Library.Utils
{
    using System;

    public static class DateFunctions
    {
        private static readonly string[] Formats =
        {
            "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
            "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy",
            "yyyyMM", "yyyyMMdd"
        };

        public static DateTime? ConvertStringToDateTime(string date)
        {
            var dateIsParsed = DateTime.TryParseExact(date, Formats,
                 System.Globalization.CultureInfo.InvariantCulture,
                 System.Globalization.DateTimeStyles.None, out var resultDate);

            if (dateIsParsed)
            {
                return resultDate;
            }

            return null;
        }

        public static string ConvertDateToStringFormat(DateTime date, string format)
        {
            return date.ToString(format, DateTimeFormatInfo.InvariantInfo);
        }
    }
}