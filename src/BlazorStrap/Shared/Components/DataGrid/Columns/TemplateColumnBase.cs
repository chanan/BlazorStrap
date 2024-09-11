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
    public Expression<Func<TGridItem, object>>? FilterBy { get; set; }
    private Expression<Func<TGridItem, object>>? _filterBy;
    protected override void OnParametersSet()
    {
        if (!Equals(_sortByField,SortByField) && SortByField != null)
        {   
            _sortByField = SortByField;
            PropertyPath = ExpressionHelper.GetPropertyPath(_sortByField);
        }
        if (!Equals(_filterBy,FilterBy) && FilterBy != null)
        {
            _filterBy = FilterBy;
            PropertyPath = ExpressionHelper.GetPropertyPath(_filterBy);
        }
    }
    public override void CellContent(RenderTreeBuilder builder, TGridItem item)
    {
        if (Content is null) return;
        builder.AddContent(4, Content?.Invoke(item));
    }
}