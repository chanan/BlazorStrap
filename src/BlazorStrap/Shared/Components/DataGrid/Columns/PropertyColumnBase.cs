using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.Shared.Components.DataGrid.Columns;

public abstract class PropertyColumnBase<TGridItem, TProp> : ColumnBase<TGridItem>
{
    [Parameter, EditorRequired] public Expression<Func<TGridItem, TProp>>? Property { get; set; } = default!;
    [Parameter] public override RenderFragment<IColumnHeaderAccessor>? ColumnOptions { get; set; }
    private Expression<Func<TGridItem, TProp>>? _property;
    private Func<TGridItem, string?>? _cellValueFunc;
    protected override void OnParametersSet()
    {
        if (Property is null) return;
        if (!Equals(_property,Property))
        {   
            var compiledPropertyExpression = Property.Compile();
            _property = Property;
            PropertyPath = ExpressionHelper.GetPropertyPath(_property);
            _cellValueFunc = item => compiledPropertyExpression(item)?.ToString();
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
    public override void BuildHeader(RenderTreeBuilder __builder, IColumnHeaderAccessor columnHeaderAccessor)
    {
        throw new Exception("PropertyColumn does not support custom headers. Use TemplateColumn instead.");
    }
}