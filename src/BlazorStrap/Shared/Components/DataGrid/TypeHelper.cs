namespace BlazorStrap.Shared.Components.DataGrid;

public class TypeHelper
{
    public static bool IsNumber(object value)
    {
        return value is sbyte or byte or short or int or long or float or double or decimal;
    }
    public static bool IsEnum(object value)
    {
        return value is Enum;
    }
    public static bool IsDateTime(object value)
    {
        return value is DateTime or DateTimeOffset or DateOnly;
    }
    public static bool IsString(object value)
    {
        return value is string or Guid or char or TimeOnly ;
    }
    
}