using System.Globalization;
using System.Text;

namespace BlazorStrap
{
    internal static class StringExtensions
    {
        public static string ToDashSeperated(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            if (text.Length < 2)
            {
                return text;
            }
            var sb = new StringBuilder();
            sb.Append(char.ToLowerInvariant(text[0]));
            for (int i = 1; i < text.Length; ++i)
            {
                char c = text[i];
                if (char.IsUpper(c))
                {
                    sb.Append('-');
                    sb.Append(char.ToLowerInvariant(c));
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string LeftRightToStartEnd(this string value)
        {
            if (value == "left")
                return "start";
            else if (value == "right")
                return "end";
            return value;
        }

        public static string PurgeStartEnd(this string value)
        {
            value = value.Replace("start", "", true, CultureInfo.InvariantCulture);
            value = value.Replace("end", "", true, CultureInfo.InvariantCulture);
            return value;
        }
        public static string? FirstCharToUpper(this string? value)
        {
            if (string.IsNullOrEmpty(value) || char.IsUpper(value[0]))
                return value;
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static string? ToNullString(this string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
