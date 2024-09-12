using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.Shared.Components.DataGrid.Columns;

public abstract class TemplateColumnBase<TGridItem> : ColumnBase<TGridItem>
{
    
    /// <summary>
    /// Gives you complete control over the column header.
    /// </summary>
    [Parameter] public override RenderFragment<IColumnHeaderAccessor>? Header { get; set; }
    
    [Parameter] public override RenderFragment<TGridItem>? Content { get; set; }
    
    /// <summary>
    /// Used to add a footer for the column.
    /// </summary>
    [Parameter] public override RenderFragment? Footer { get; set; }
    
    /// <summary>
    /// Specifies the property to be used for sorting and filtering.
    /// </summary>
    [Parameter] public Expression<Func<TGridItem, object>>? Property { get; set;}
    private Expression<Func<TGridItem, object>>? _property;

    protected override void OnParametersSet()
    {
        if (Header is not null && ColumnOptions is not null)
        {
            throw new Exception("You can't have both a Header and ColumnOptions. When using Header you have complete control over the way the header builds. You will need to implement your own ColumnOptions menu in your Header.");
        }
        
        if (!Equals(_property,Property) && Property != null)
        {   
            _property = Property;
            PropertyPath = ExpressionHelper.GetPropertyPath(_property);
        }
    }
    public override void CellContent(RenderTreeBuilder builder, TGridItem item)
    {
        if (Content is null) return;
        builder.AddContent(4, Content?.Invoke(item));
    }
    public override void BuildHeader(RenderTreeBuilder builder, IColumnHeaderAccessor columnHeaderAccessor) 
    {
        if (Header is null) return;
        builder.AddContent(1, Header?.Invoke(columnHeaderAccessor));
    }
}