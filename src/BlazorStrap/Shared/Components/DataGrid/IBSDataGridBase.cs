using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.DataGrid;

public interface IBSDataGridBase<TGridItem>
{
    RenderFragment? Columns { get; set; }
    GridItemsProvider<TGridItem>? ItemsProvider { get; set; }
    bool IsVirtualized { get; set; }
    IQueryable<TGridItem>? Items { get; set; }
    string? RowClass { get; set; }
    Func<TGridItem, string>? RowClassFunc { get; set; }
    string? RowStyle { get; set; }
    Func<TGridItem, string>? RowStyleFunc { get; set; }
    bool IsMultiSort { get; set; }
    string? MultiSortClass { get; set; }
    string? FilterClass { get; set; }
    string? MenuClass { get; set; }
    PaginationState? Pagination { get; set; }
    string? DataGridClass { get; set; }
    float VirtualItemHeight { get; set; }
    int VirtualOverscanCount { get; set; }
}