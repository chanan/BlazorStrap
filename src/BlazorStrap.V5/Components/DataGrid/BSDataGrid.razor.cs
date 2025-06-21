using BlazorStrap.Shared.Components.DataGrid;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5.DataGrid;
[CascadingTypeParameter(nameof(TGridItem))]
#warning "This component is not ready for use"
public partial class BSDataGrid<TGridItem> : BSDataGridBase<TGridItem>
{
    protected override string? LayoutClass { get; } 
    protected override string? ClassBuilder { get; }
    protected override string? WrapperClassBuilder { get; }
}