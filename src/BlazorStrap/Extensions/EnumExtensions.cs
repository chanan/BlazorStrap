using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public static class EnumExtensions
    {
        internal static string Name<T>(this T val) where T : Enum
        {
            return Enum.GetName(typeof(T), val) ?? "";
        }

        internal static string NameToLower<T>(this T val) where T : Enum
        {
            return Enum.GetName(typeof(T), val).ToLower() ?? "";
        }

        internal static string ToDescriptionString<T>(this T val) where T : Enum
        {
            var attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
        internal static string ToIndex(this Gutters value)
        {
            return value switch
            {
                Gutters.None => "0",
                Gutters.ExtraSmall => "1",
                Gutters.Small => "2",
                Gutters.Medium => "3",
                Gutters.Large => "4",
                Gutters.ExtraLarge => "5",
                _ => ""
            };
        }
        internal static string ToIndex(this Margins value)
        {
            return value switch
            {
                Margins.None => "0",
                Margins.ExtraSmall => "1",
                Margins.Small => "2",
                Margins.Medium => "3",
                Margins.Large => "4",
                Margins.ExtraLarge => "5",
                Margins.Auto => "auto",
                _ => ""
            };
        }

        internal static string ToIndex(this Padding value)
        {
            return value switch
            {
                Padding.None => "0",
                Padding.ExtraSmall => "1",
                Padding.Small => "2",
                Padding.Medium => "3",
                Padding.Large => "4",
                Padding.ExtraLarge => "5",
                _ => ""
            };
        }
    }
}
