using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;

namespace BlazorStrap.Util
{
    public static class TryParseString<T>
    {
        private const string _dateFormat = "yyyy-MM-dd";

        public static bool ToDateTime(string value, out T result)
        {
            var success = BindConverter.TryConvertToDateTime(value, CultureInfo.InvariantCulture, _dateFormat, out DateTime parsedValue);
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

        private static bool ToDateTimeOffset(string value, out T result)
        {
            var success = BindConverter.TryConvertToDateTimeOffset(value, CultureInfo.InvariantCulture, _dateFormat, out DateTimeOffset parsedValue);
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

        public static bool ToValue(string value, out T result, out string validationErrorMessage)
        {
            Type type = typeof(T);
            result = default;
            validationErrorMessage = string.Empty;
            if (typeof(T) == typeof(string))
            {
                result = (T)(object)value;
                validationErrorMessage = null;
                return true;
            }
            else if (value == null && (Nullable.GetUnderlyingType(type) != null))
            {
                result = (T)(object)default(T);
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
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
            {
                result = (T)(object)Convert.ToInt32(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(long) || typeof(T) == typeof(long?))
            {
                result = (T)(object)Convert.ToInt64(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(double) || typeof(T) == typeof(double?))
            {
                result = (T)(object)double.Parse(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?))
            {
                result = (T)(object)decimal.Parse(value, CultureInfo.InvariantCulture);
                validationErrorMessage = null;
                return true;
            }
            else if (typeof(T) == typeof(Guid) || typeof(T) == typeof(Guid?))
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
                        validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a bool of true or false.");
                        return false;
                    }
                }
                catch
                {
                    result = (T)(object)false;
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a bool of true or false.");
                    return false;
                }
            }
            else if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
            {
                if (TryParseString<T>.ToDateTime(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a date.", type.Name);
                    return false;
                }
            }
            else if (typeof(T) == typeof(DateTimeOffset) || typeof(T) == typeof(DateTimeOffset?))
            {
                if (TryParseString<T>.ToDateTimeOffset(value, out result))
                {
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    validationErrorMessage = string.Format(CultureInfo.InvariantCulture, "The {0} field must be a date.", type.Name);
                    return false;
                }
            }

            return false;
        }
    }
}
