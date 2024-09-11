using BlazorStrap.Shared.Components.DataGrid.BSDataGirdHelpers;

namespace BlazorStrap;

public readonly struct DataGridRequest<TGridItem>
{
    public int StartIndex { get; init; }
    public int? Count { get; init; }
    public ICollection<SortColumn<TGridItem>> SortColumns { get; init; }
    public ICollection<IColumnFilter<TGridItem>> FilterColumns { get; init; }
    public CancellationToken CancellationToken { get; init; }
    internal DataGridRequest(int startIndex, int? count, ICollection<SortColumn<TGridItem>> sortColumns, ICollection<IColumnFilter<TGridItem>> filterColumns, CancellationToken cancellationToken)
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
    internal static IQueryable<TItem> ApplySort<TItem>(this IQueryable<TItem> items, ICollection<SortColumn<TItem>> columns)
    {
        return items.SortColumns(columns) ?? items;
    }
    internal static IQueryable<TItem> ApplyFilters<TItem>(this  IQueryable<TItem> items, ICollection<IColumnFilterInternal<TItem>> columns)
    {
        return items.FiltersColumns(columns.ToList<IColumnFilter<TItem>>()) ?? items;
    }
    internal static IQueryable<TItem> ApplySort<TItem>(this DataGridRequest<TItem> request, IQueryable<TItem> items, ICollection<SortColumn<TItem>> columns)
    {
        return items.SortColumns(columns) ?? items;
    }
    internal static IQueryable<TItem> ApplyFilters<TItem>(this DataGridRequest<TItem> request, IQueryable<TItem> items, ICollection<IColumnFilterInternal<TItem>> columns)
    {
        return items.FiltersColumns(columns.ToList<IColumnFilter<TItem>>()) ?? items;
    }
}