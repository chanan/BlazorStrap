using System.Text.Json.Serialization;
using BlazorStrap.JsonConverters;

namespace BlazorStrap;

[JsonConverter(typeof(CallerNameJsonConverter))]
public class CallerName
{
    private readonly string? _name;
    
    public CallerName(string name)
    {
        _name = name;
    }

    public string? GetName()
    {
        return _name;
    }
    public static implicit operator CallerName?(string? name)
    {
        return name == null ? null : new CallerName(name);
    }

    public override string? ToString()
    {
        return _name;
    }

    public bool Equals<T>(T obj) where T: class
    {
        if (obj == null) throw new ArgumentNullException(nameof(obj));
        return typeof(T).Name.ToLower() == _name;
    }
    public bool Equals(Type obj)
    {
        return obj.Name.ToLower() == _name;
    }

    public bool Equals<T>() where T: class
    {
        return typeof(T).Name.ToLower() == _name;
    }
}

