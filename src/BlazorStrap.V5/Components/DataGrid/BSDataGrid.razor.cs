using BlazorStrap.Shared.Components.DataGrid;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5;
[CascadingTypeParameter(nameof(TGridItem))]
public partial class BSDataGrid<TGridItem> : BSDataGridBase<TGridItem>
{
    protected override string? LayoutClass { get; } 
    protected override string? ClassBuilder { get; }
    protected override string? WrapperClassBuilder { get; }
}