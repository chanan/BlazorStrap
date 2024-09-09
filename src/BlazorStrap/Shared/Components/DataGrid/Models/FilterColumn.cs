namespace BlazorStrap.Shared.Components.DataGrid.Models;

public record FilterColumn<TGridItem>(Guid Id, string Value, string PropertyPath, ColumnBase<TGridItem> Column)
{
    public string Value { get; set; } = string.Empty;
}
