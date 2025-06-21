using BlazorStrap.Shared.Components.Content;
using BlazorStrap.Shared.Components.DataGrid.Columns;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.DataGrid;

[CascadingTypeParameter(nameof(TGridItem))]
public abstract partial class BSDataGridBase<TGridItem> : BSTableBase, IBSDataGridBase<TGridItem>
{
    #region Blazor Properties
    [Parameter] public RenderFragment? Columns { get; set; }
    [Parameter] public GridItemsProvider<TGridItem>? ItemsProvider { get; set; }
    [Parameter] public bool IsVirtualized { get; set; } = false;
    [Parameter] public IQueryable<TGridItem>? Items { get; set; }
    [Parameter] public string? RowClass { get; set; }
    [Parameter] public Func<TGridItem, string>? RowClassFunc { get; set; }
    [Parameter] public string? RowStyle { get; set; }
    [Parameter] public string MultiSortClass { get; set; } = "badge bg-info text-dark";
    [Parameter] public IAsyncProvider AsyncProvider { get; set; } = new FakeAsyncProvider();
    [Parameter] public PaginationStateBase? Pagination { get; set; }
    [Parameter] public string? FilterClass { get; set; }
    [Parameter] public string? MenuClass { get; set; } 
    /// <summary>
    /// Set the row style based on the item.
    /// </summary>
    /// Example: <code>item => item.IsActive ? "background-color: red;" : ""</code>
    [Parameter] public Func<TGridItem, string>? RowStyleFunc { get; set; }
    
    [Parameter] public bool IsMultiSort { get; set; } = false;
    [Parameter] public bool IsFilterable { get; set; } = false;
    [Parameter] public string? DataGridClass { get; set; } = "bs-datagrid";
    [Parameter] public float VirtualItemHeight { get; set; } = 30;
    [Parameter] public int VirtualOverscanCount { get; set; } = 5;
    
    internal Func<Task>? RefreshItemsAsyncFunc { get; set; }
    #endregion

    public Task RefreshItemsAsync()
    {
        return RefreshItemsAsyncFunc?.Invoke() ?? Task.CompletedTask;
    }
    protected ColumnState<TGridItem> ColumnState;
    protected BSDataGridBase()
    {
        ColumnState = new ColumnState<TGridItem>();
    }
}
