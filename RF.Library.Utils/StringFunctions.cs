using System;
using System.Text;

namespace RF.Library.Utils
{
    public static class StringFunctions
    {
        public static StringBuilder AppendIf(this StringBuilder @this, bool condition, string str)
        {
            if (@this == null)
            {
                throw new ArgumentNullException();
            }

            if (condition)
            {
                @this.Append(str);
            }

            return @this;
        }

        public static string ConvertToAlphaNumeric(this string value)
        {
            return string.IsNullOrEmpty(value) ? null : new string(Array.FindAll(value.ToCharArray(), char.IsLetterOrDigit));
        }
    }
}