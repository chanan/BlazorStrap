namespace BlazorStrap.Shared.Components.DataGrid.Columns;

public interface IColumnBase<TGridItem>
{
    public string Filter { get; set; }
}