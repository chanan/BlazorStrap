using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Reflection;
using static System.Nullable;

// Not messing with this class it does a lot of goofy things to make inputs work with all types of values
// ReSharper disable ConditionIsAlwaysTrueOrFalse
#pragma warning disable CS8604
#pragma warning disable CS8625
#pragma warning disable CS8600
#pragma warning disable CS8602
#pragma warning disable CS8601

namespace BlazorStrap.Utilities
{
    public static class TryParseString<T>
    {
        private static string GetDateTimeFormatString(string value)
        {
            const string dateFormat = "yyyy-MM-dd";
            const string dateTimeLocalFormat = "yyyy-MM-ddTHH:mm:ss";
            const string monthFormat = "yyyy-MM";   
            const string timeFormat = "HH:mm";
            const string timeFormatWithSeconds = "HH:mm:ss";
            const string timeFormatWithMilliSeconds = "HH:mm:ss.fff";

            var formatString = value.Count(c => c == ':') switch
            {
                0 when value.Count(c => c == '-') is 1 => monthFormat,
                1 => timeFormat,
                2 when value.Contains('.') => timeFormatWithMilliSeconds,
                2 when value.Count(c => c == '-') is 2 => dateTimeLocalFormat,
                2 => timeFormatWithSeconds,
                _ => dateFormat
            };
            return formatString;
        }

        #if NET6_0_OR_GREATER
        private static bool ToDateOnly(string value, out T result)
        {
            var formatString = GetDateTimeFormatString(value);
            var success = BindConverter.TryConvertToDateOnly(value, CultureInfo.InvariantCulture, formatString, out DateOnly parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            
            result = default;
            return false;
        }
      
        private static bool ToTimeOnly(string value, out T result)
        {
            var formatString = GetDateTimeFormatString(value);
            var success = BindConverter.TryConvertToTimeOnly(value, CultureInfo.InvariantCulture, formatString, out TimeOnly parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            
            result = default;
            return false;
        }
        #endif
        private static bool ToDateTime(string value, out T result)
        {
            var formatString = GetDateTimeFormatString(value);
            var success = BindConverter.TryConvertToDateTime(value, CultureInfo.InvariantCulture, formatString, out DateTime parsedValue);
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            
            result = default;
            return false;
        }

        private static bool ToDateTimeOffset(string value, out T result)
        {
            var formatString = GetDateTimeFormatString(value);
            var success = BindConverter.TryConvertToDateTimeOffset(value, CultureInfo.InvariantCulture, formatString, out DateTimeOffset parsedValue);
            
            if (success)
            {
                result = (T)(object)parsedValue;
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public static bool ToValue(string? value, out T result, out string? validationErrorMessage)
        {
            Type type = typeof(T);
            result = default;
            validationErrorMessage = string.Empty;
            if (Nullable.GetUnderlyingType(type) != null)
            {
                if (string.IsNullOrEmpty(value))
                {
                    validationErrorMessage = null;
                    return true;
                }

                return NotNullValueToNullableType(value, out result, out validationErrorMessage);
            }
            else if (typeof(T) == typeof(string))
            {
                result = (T)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(CultureInfo))
            {
                result = (T) (object) CultureInfo.CreateSpecificCulture(value);
                validationErrorMessage = null;
                return true;
            }
            else if (value?.Length == 0 && typeof(DateTime) != typeof(T) && typeof(DateTimeOffset) != typeof(T))
            {
                result = (T)(object)default(T);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T).IsEnum)
            {
                // There's no non-generic Enum.TryParse (https://github.com/dotnet/corefx/issues/692)
                try
                {
                    result = (T)Enum.Parse(typeof(T), value);
                    validationErrorMessage = null;
                    return true;
                }
                catch (ArgumentException)
                {
                    result = default;
                    validationErrorMessage = $"The {type.Name} field is not valid.";
                    return false;
                }
            }
            else if (typeof(T) == typeof(int))
            {
                result = (T)(object)Convert.ToInt32(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(long))
            {
                result = (T)(object)Convert.ToInt64(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(double))
            {
                result = (T)(object)double.Parse(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(decimal))
            {
                result = (T)(object)decimal.Parse(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(float))
            {
                result = (T)(object)float.Parse(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(Guid))
            {
                try
                {
                    result = (T)(object)Guid.Parse(value);
                    validationErrorMessage = null;
                }
                catch
                {
                    result = (T)(object)new Guid();
                    validationErrorMessage = "Invalid Guid format";
                }

                return true;
            }
            else if (typeof(T) == typeof(bool))
            {
                try
                {
                    if (value.ToString().ToLowerInvariant() == "false")
                    {
                        result = (T)(object)false;
                        validationErrorMessage = null;
                        return true;
                    }
                    else if (value.ToString().ToLowerInvariant() == "true")
                    {
                        result = (T)(object)true;
                        validationErrorMessage = null;
                        return true;
                    }
                    else
                    {
                        result = (T)(object)false;
                        validationErrorMessage = string.Format(CultureInfo.InvariantCulture,
                            "The {0} field must be a bool of true or false.");
                        return false;
                    }
                }
                catch
                {
                    result = (T)(object)false;
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture,
                        "The {0} field must be a bool of true or false.");
                    return false;
                }
            }
            else if (typeof(T) == typeof(DateTime))
            {
                if (TryParseString<T>.ToDateTime(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture,
                        "The {0} field must be a date.", type.Name);
                    return false;
                }
            }
        
            #if NET6_0_OR_GREATER
            else if (typeof(T) == typeof(DateOnly))
            {
                if (TryParseString<T>.ToDateOnly(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture,
                        "The {0} field must be a date.", type.Name);
                    return false;
                }
            }
            else if (typeof(T) == typeof(TimeOnly))
            {
                if (TryParseString<T>.ToTimeOnly(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture,
                        "The {0} field must be a time.", type.Name);
                    return false;
                }
            }
            #endif
            
            else if (typeof(T) == typeof(DateTimeOffset))
            {
                if (ToDateTimeOffset(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture,
                        "The {0} field must be a date.", type.Name);
                    return false;
                }
            }
            return false;
        }

        private static bool NotNullValueToNullableType(string value, out T result, out string? validationErrorMessage)
        {
            var method = typeof(TryParseString<>).MakeGenericType(Nullable.GetUnderlyingType(typeof(T))).GetMethod(nameof(ToValue));
            var parameters = new object[] { value, null, null };
            var success = (bool)method.Invoke(null, parameters)!;
            result = (T)parameters[1];
            validationErrorMessage = (string?)parameters[2];
            return success;
        }
    }
}