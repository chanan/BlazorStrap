using BlazorStrap.Shared.Components.Content;
using BlazorStrap.Shared.Components.DataGrid.Models;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BlazorStrap.Shared.Components.DataGrid;

[CascadingTypeParameter(nameof(TGridItem))]
public abstract partial class BSDataGridBase<TGridItem> : BSTableBase, IBSDataGridBase<TGridItem>
{
    #region Blazor Properties
    [Parameter] public RenderFragment? Columns { get; set; }
    [Parameter] public GridItemsProvider<TGridItem>? ItemsProvider { get; set; }
    [Parameter] public bool IsVirtualized { get; set; } = false;
    [Parameter] public IQueryable<TGridItem>? Items { get; set; }
    [Parameter] public bool Paginated { get; set; } = false;
    [Parameter] public string? RowClass { get; set; }
    [Parameter] public Func<TGridItem, string>? RowClassFunc { get; set; }
    [Parameter] public string? RowStyle { get; set; }
    [Parameter] public string MultiSortClass { get; set; } = "badge bg-info text-dark";
 
    /// <summary>
    /// Set the row style based on the item.
    /// </summary>
    /// Example: <code>item => item.IsActive ? "background-color: red;" : ""</code>
    [Parameter] public Func<TGridItem, string>? RowStyleFunc { get; set; }
    
    [Parameter] public bool IsMultiSort { get; set; } = false;
    
    #endregion

    protected ColumnState<TGridItem> ColumnState;
    protected BSDataGridBase()
    {
        ColumnState = new ColumnState<TGridItem>(this);
    }
}