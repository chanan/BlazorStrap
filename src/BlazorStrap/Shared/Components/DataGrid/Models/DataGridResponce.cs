namespace BlazorStrap.Shared.Components.DataGrid.Models;

public struct DataGridResponce<TGridItem>
{
    public ICollection<TGridItem> Items { get; init; }
    public int TotalCount { get; init; }
}

public static class DataGridResponce
{
    public static DataGridResponce<TItem> Create<TItem>(ICollection<TItem> items, int totalCount)
    {
        return new DataGridResponce<TItem>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}