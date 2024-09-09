using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.Shared.Components.DataGrid;

public abstract class TemplateColumnBase<TGridItem> : ColumnBase<TGridItem>
{
    
    [Parameter] public override RenderFragment? Header { get; set; }
    [Parameter] public override RenderFragment<TGridItem>? Content { get; set; }
    [Parameter] public override RenderFragment? Footer { get; set; }
    
    [Parameter]
    public Expression<Func<TGridItem, object>>? SortField { get; set;}
    private Expression<Func<TGridItem, object>>? _sortField;
    protected override void OnParametersSet()
    {
        if (!Equals(_sortField,SortField) && SortField != null)
        {   
            _sortField = SortField;
            PropertyPath = ExpressionHelper.GetPropertyPath(_sortField);
        }
    }
    public override void CellContent(RenderTreeBuilder builder, TGridItem item)
    {
        if (Content is null) return;
        builder.AddContent(4, Content?.Invoke(item));
    }
}