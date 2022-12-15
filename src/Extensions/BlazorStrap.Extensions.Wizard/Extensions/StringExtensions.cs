using System.Globalization;
using System.Text;

namespace BlazorStrap.Extensions.Wizard.Extensions
{
    public static class StringExtensions
    {
        public static string? ToNullString(this string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
