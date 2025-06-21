using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.Shared.Components.DataGrid.Columns;

public abstract class PropertyColumnBase<TGridItem, TProp> : ColumnBase<TGridItem>
{
    [Parameter, EditorRequired] public Expression<Func<TGridItem, TProp>>? Property { get; set; } = default!;
    [Parameter] public string? Format { get; set; }
    [Parameter] public override RenderFragment<IColumnHeaderAccessor>? ColumnOptions { get; set; }
    private Expression<Func<TGridItem, TProp>>? _property;
    private string? _format;
    private Func<TGridItem, string?>? _cellValueFunc;
    protected override void OnParametersSet()
    {
        if (Property is null) return;
        if (!Equals(_property,Property) || !Equals(_format, Format))
        {   
            var compiledPropertyExpression = Property.Compile();
            _property = Property;
            _format = Format;
            PropertyPath = ExpressionHelper.GetPropertyPath(_property);
            _cellValueFunc = item => FormatValue(compiledPropertyExpression(item), Format);
        }
        
        if(Title is null && Property.Body is MemberExpression memberExpression)
        {
            Title = memberExpression.Member.Name;
        }
    }

    public override void CellContent(RenderTreeBuilder __builder, TGridItem item)
    {
        if (_cellValueFunc is null) return;
        if(MaxTextWidth > 0)
        {
            __builder.AddContent(0, EllipsisTest(_cellValueFunc(item), MaxTextWidth));
            return;
        }
        __builder.AddContent(0, _cellValueFunc(item));
    }
    private static string EllipsisTest(string text, int max)
    {
        if (text.Length <= max) return text;
        return text.Substring(0, max) + "...";
    }
    
    private static string? FormatValue(object? value, string? format)
    {
        if (value is null) return null;
        
        if (string.IsNullOrEmpty(format))
        {
            return value.ToString();
        }
        
        try
        {
            // Handle IFormattable types (DateTime, numeric types, etc.)
            if (value is IFormattable formattable)
            {
                return formattable.ToString(format, null);
            }
            
            // Fallback to regular ToString() for non-formattable types
            return value.ToString();
        }
        catch (FormatException)
        {
            // If format string is invalid, fall back to default ToString()
            return value.ToString();
        }
    }
    public override void BuildHeader(RenderTreeBuilder __builder, IColumnHeaderAccessor columnHeaderAccessor)
    {
        throw new Exception("PropertyColumn does not support custom headers. Use TemplateColumn instead.");
    }
}