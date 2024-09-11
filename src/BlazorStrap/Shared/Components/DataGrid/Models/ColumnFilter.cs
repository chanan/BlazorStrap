using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.DataGrid;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap;

public interface IColumnFilterInternal<TGridItem> : IColumnFilter<TGridItem>
{
    Type ValueType { get; }
    string ValueAsString { get; set; }
}
public interface IColumnFilter<TGridItem> 
{
    public string Property { get; set; }
    public Operator Operator { get; set; }
    public dynamic? Value { get; set; }
}
public class ColumnFilter<TGridItem> : IColumnFilterInternal<TGridItem>
{
    public string Property
    {
        get => _property;
        set
        {
            if (value != _property)
            {
                _property = value;
                _value = null;
                ExpressionHelper.GetPropertyType<TGridItem>(_property);
            }
        }
    }

    public Operator Operator { get; set; }
    private dynamic? _value;
    private string _property;

    public dynamic? Value
    {
        get => _value;
        set => _value = value;
    }
    public Type ValueType { get; set; }

    public string ValueAsString
    {
        get => Convert.ToString(_value);
        set
        {
            // Check for null or empty string to reset the value
            if (string.IsNullOrEmpty(value))
            {
                _value = null;
                return;
            }

            // Convert to the specific type based on ValueType
            if (ValueType == typeof(int))
            {
                if (int.TryParse(value, out int intValue))
                    _value = intValue;
            }
            else if (ValueType == typeof(long))
            {
                if (long.TryParse(value, out long longValue))
                    _value = longValue;
            }
            else if (ValueType == typeof(float))
            {
                if (float.TryParse(value, out float floatValue))
                    _value = floatValue;
            }
            else if (ValueType == typeof(double))
            {
                if (double.TryParse(value, out double doubleValue))
                    _value = doubleValue;
            }
            else if (ValueType == typeof(decimal))
            {
                if (decimal.TryParse(value, out decimal decimalValue))
                    _value = decimalValue;
            }
            else if (ValueType == typeof(DateTime))
            {
                if (DateTime.TryParse(value, out DateTime dateTimeValue))
                    _value = dateTimeValue;
            }
            else if (ValueType == typeof(DateTimeOffset))
            {
                if(DateTimeOffset.TryParse(value, out DateTimeOffset dateTimeOffsetValue))
                    _value = dateTimeOffsetValue;
            }
#if NET6_0_OR_GREATER
            else if (ValueType == typeof(DateOnly))
            {
                if(DateOnly.TryParse(value, out DateOnly dateOnlyValue))
                    _value = dateOnlyValue;
            }
            else if (ValueType == typeof(TimeOnly))
            {
                if(TimeOnly.TryParse(value, out TimeOnly timeOnlyValue))
                    _value = timeOnlyValue;
            }
#endif
            else if (ValueType == typeof(Guid))
            {
                if (Guid.TryParse(value, out Guid guidValue))
                    _value = guidValue;
            }
            else if (ValueType == typeof(char))
            {
                if (char.TryParse(value, out char charValue))
                    _value = charValue;
            }
            else if (ValueType == typeof(string))
            {
                _value = value;
            }
            else if (ValueType == typeof(bool))
            {
                if (bool.TryParse(value, out bool boolValue))
                    _value = boolValue;
            }
            else if (ValueType == typeof(bool))
            {
                if (bool.TryParse(value, out bool boolValue))
                    _value = boolValue;
            }
            else
            {
                _value = value;
            }   
        }
    }
    
    public ColumnFilter(string property, Operator @operator, dynamic? value)
    {
        Property = property;
        Operator = @operator;
        Value = value;
        ValueType = ExpressionHelper.GetPropertyType<TGridItem>(property);
    }

}
