using System.ComponentModel;
using BlazorStrap.Properties;

namespace BlazorStrap
{
    public static class EnumExtensions
    {
        internal static bool VaildGridSize(this string? value)
        {
            if (value is "auto" or "1" or "2" or "3" or "4" or "5" or "6" or "7" or "8" or "9" or "10" or "11" or "12")
            {
                return true;
            }

            if (!string.IsNullOrEmpty(value))
                
                Console.WriteLine(Resources.Could_not_parse_input__Invalid_Guid_format,value);
            return false;
        }
        internal static string Name<T>(this T val) where T : Enum?
        {
            if (val == null) throw new NullReferenceException("Enum values should not be null");
            return Enum.GetName(typeof(T), val) ?? "";
        }

        internal static string NameToLower<T>(this T val) where T : Enum?
        {
            if (val == null) throw new NullReferenceException("Enum values should not be null");
            return Enum.GetName(typeof(T), val)?.ToLower() ?? "";
        }

        internal static string ToDescriptionString<T>(this T val) where T : Enum?
        {
            if (val == null) throw new NullReferenceException("Enum values should not be null");
            var field = val.GetType().GetField(val.ToString());
            
            if(field == null) throw new NullReferenceException("Field names should not be null");
            
            var attributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof(DescriptionAttribute), false);
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
