using System;
using System.Globalization;
using System.Linq;

namespace BlazorStrap.Util
{
    internal static class StringExtensions
    {
        public static string? ToNullString(this string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }
        public static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Properties.Resources.cannot_be_empty, nameof(input)), nameof(input)),
                _ => input.First().ToString(CultureInfo.InvariantCulture).ToUpperInvariant() + input.Substring(1),
            };
        }
    }
}