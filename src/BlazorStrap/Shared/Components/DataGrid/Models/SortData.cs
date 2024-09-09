namespace BlazorStrap.Shared.Components.DataGrid.Models;

public record SortData<TGridItem>(IQueryable<TGridItem> Query, bool Descending)
{
    public bool Descending { get; set; } = Descending;
    public IOrderedQueryable<TGridItem>? Ordered { get; set; } 
}