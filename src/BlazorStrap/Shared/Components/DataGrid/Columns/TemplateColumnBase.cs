using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.Shared.Components.DataGrid.Columns;

public abstract class TemplateColumnBase<TGridItem> : ColumnBase<TGridItem>
{
    
    [Parameter] public override RenderFragment? Header { get; set; }
    [Parameter] public override RenderFragment<TGridItem>? Content { get; set; }
    [Parameter] public override RenderFragment? Footer { get; set; }
    
    [Parameter]
    public Expression<Func<TGridItem, object>>? SortByField { get; set;}
    private Expression<Func<TGridItem, object>>? _sortByField;
    protected override void OnParametersSet()
    {
        if (!Equals(_sortByField,SortByField) && SortByField != null)
        {   
            _sortByField = SortByField;
            PropertyPath = ExpressionHelper.GetPropertyPath(_sortByField);
        }
    }
    public override void CellContent(RenderTreeBuilder builder, TGridItem item)
    {
        if (Content is null) return;
        builder.AddContent(4, Content?.Invoke(item));
    }
}