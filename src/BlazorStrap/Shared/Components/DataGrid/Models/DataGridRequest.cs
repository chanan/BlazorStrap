using BlazorStrap.Shared.Components.DataGrid.BSDataGirdHelpers;

namespace BlazorStrap.Shared.Components.DataGrid.Models;

public readonly struct DataGridRequest<TGridItem>
{
    public int StartIndex { get; init; }
    public int? Count { get; init; }
    public ICollection<SortColumn<TGridItem>> SortColumns { get; init; }
    public ICollection<FilterColumn<TGridItem>> FilterColumns { get; init; }
    public CancellationToken CancellationToken { get; init; }
    internal DataGridRequest(int startIndex, int? count, ICollection<SortColumn<TGridItem>> sortColumns, ICollection<FilterColumn<TGridItem>> filterColumns, CancellationToken cancellationToken)
    {
        StartIndex = startIndex;
        Count = count;
        SortColumns = sortColumns;
        FilterColumns = filterColumns;
        CancellationToken = cancellationToken;
    }
}
internal static class DataGridRequest
{
    internal static IQueryable<TItem> ApplySort<TItem>(this DataGridRequest<TItem> request, IQueryable<TItem> items, ICollection<SortColumn<TItem>> columns)
    {
        return items.SortColumns(columns) ?? items;
    }
}