using Npoi.Mapper;

namespace RF.Infrastructure.Parser.ExtensionMethods.Npoi
{
    using Library.Utils;
    using System.Globalization;

    public static class MappingExtensionMethods
    {
        internal static bool ParseDateTimeValues(string value, ref string target)
        {
            var dateToParse = value;

            if (dateToParse == null)
            {
                return true;
            }

            var date = DateFunctions.ConvertStringToDateTime(dateToParse);

            if (!date.HasValue)
            {
                return false;
            }

            target = date.Value.ToString(CultureInfo.InvariantCulture);

            return true;
        }

        internal static bool ParseDoubleValues(object value, ref double? target)
        {
            if (value == null)
            {
                return true;
            }

            var numberToParse = value.ToString();

            if (string.IsNullOrEmpty(numberToParse))
            {
                return true;
            }

            var numberIsParsed = double.TryParse(numberToParse.Replace(',', '.'), NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture,
                out var amountParsed);

            if (!numberIsParsed)
            {
                return false;
            }

            target = amountParsed;

            return true;
        }

        internal static bool ConvertToAlphaNumericOnlyValues(string value, ref string target)
        {
            var stringToParse = value;

            if (stringToParse == null)
            {
                return true;
            }

            var alphaNumericString = stringToParse.ConvertToAlphaNumeric();

            if (alphaNumericString == null)
            {
                return false;
            }

            target = alphaNumericString;

            return true;
        }

        internal static string TrimCellValue(IColumnInfo column)
        {
            return column.CurrentValue?.ToString()?.Trim();
        }
    }
}