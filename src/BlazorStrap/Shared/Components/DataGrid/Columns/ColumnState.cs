namespace BlazorStrap.Shared.Components.DataGrid.Columns;

public class ColumnState<TGridItem>
{
    private readonly List<ColumnBase<TGridItem>> _columns = new();
    private readonly List<SortColumn<TGridItem>> _sortColumns = new();
    
    public ICollection<ColumnBase<TGridItem>> Columns => _columns;
    public ICollection<SortColumn<TGridItem>> SortColumns => _sortColumns;
    public int SortOrder(Guid id) => _sortColumns.FirstOrDefault(x => x.Id == id)?.Order ?? 0;
    public bool IsSorted(Guid id) => _sortColumns.Any(x => x.Id == id && x.Sorted);
    public bool IsSortedDescending(Guid id) => _sortColumns.Any(x => x.Id == id && x.Descending);
    
    
    public void AddColumn(ColumnBase<TGridItem> column)
    {
        if(_columns.Any(x => x.Id == column.Id)) return;
        if (column.IsSortable || column.CustomSort != null)
        {
            var sortColumn = new SortColumn<TGridItem>(column.Id, false, _sortColumns.Count, false, column.PropertyPath, column);
            _sortColumns.Add(sortColumn);
            
            // Assigns the initial sort column based on Parameter only one column can be initially sorted
            if (column.InitialSorted)
            {
                foreach (var otherColumns in (_sortColumns.Where(x => x.Column.IsSortable)))
                {
                    otherColumns.Sorted = false;
                }
                sortColumn.Sorted = true;
            }
        }
        _columns.Add(column);
    }
    
    public void RemoveColumn(ColumnBase<TGridItem> column)
    {
        _sortColumns.RemoveAll(x => x.Column.Id == column.Id);
        _columns.Remove(column);
    }

    public BSDataGridCoreBase<TGridItem>? DataGrid { get; set; }

    internal Func<Task>? OnStateChange { get; set; }
}