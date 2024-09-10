using BlazorStrap.Extensions;

namespace BlazorStrap;

public abstract class ColumnFilter
{
    public string Property { get; set; }
    public Operator Operator { get; set; }
    public virtual object? Value { get; set; }
    public Type ValueTypess => Value?.GetType() ?? typeof(object);

    public ColumnFilter(string property, Operator @operator)
    {
        Property = property;
        Operator = @operator;
    }
}
public class ColumnFilter<TValue> : ColumnFilter
{
    private new TValue _value;

    public ColumnFilter(string property, Operator @operator, TValue value) : base(property, @operator)
    {
        _value = value;
    }

    public override object? Value { get => _value; set => _value = (TValue)value; }
}
