namespace BlazorStrap.Shared.Components.DataGrid;

public class ColumnState<TGridItem>
{
    private readonly List<ColumnBase<TGridItem>> _columns = new();
    private readonly List<SortColumn<TGridItem>> _sortColumns = new();
    private readonly List<FilterColumn<TGridItem>> _filterColumns = new();
    
    public ICollection<ColumnBase<TGridItem>> Columns => _columns;
    public ICollection<SortColumn<TGridItem>> SortColumns => _sortColumns;
    public ICollection<FilterColumn<TGridItem>> FilterColumns => _filterColumns;
    public int SortOrder(Guid id) => _sortColumns.FirstOrDefault(x => x.Id == id)?.Order ?? 0;
    public bool IsSorted(Guid id) => _sortColumns.Any(x => x.Id == id && x.Sorted);
    public bool IsSortedDescending(Guid id) => _sortColumns.Any(x => x.Id == id && x.Descending);
    
    
    public void AddColumn(ColumnBase<TGridItem> column)
    {
        if(_columns.Any(x => x.Id == column.Id)) return;
        if (column.IsSortable || column.CustomSort != null)
        {
            _sortColumns.Add(new SortColumn<TGridItem>(column.Id, false, _sortColumns.Count, false, column.PropertyPath, column));
        }
        _columns.Add(column);
    }
    
    public void RemoveColumn(ColumnBase<TGridItem> column)
    {
        _sortColumns.RemoveAll(x => x.Column.Id == column.Id);
        _columns.Remove(column);
    }

    public BSDataGridBase<TGridItem> DataGrid { get; set; }
    public ColumnState(BSDataGridBase<TGridItem> dataGrid)
    {
        DataGrid = dataGrid;
    }
}