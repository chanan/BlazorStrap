namespace BlazorStrap;

public class ColumnHeaderAccessor : IColumnHeaderAccessor
{
    public Func<Task> RefreshDataTableAsync { get; internal set; } = () => Task.CompletedTask;
    public Func<Task> FilterButtonClickedAsync { get; internal set; } = () => Task.CompletedTask;
    public bool IsFiltered => IsFilteredFunc();
    public bool IsSorted => IsSortedFunc();
    public bool IsSortedDescending => IsSortedDescendingFunc();

    public string Filter
    {
        get => GetFilterFunc();
        set => SetFilterFunc(value);
    }

    internal Func<bool> IsFilteredFunc { get; set;} = () => false;
    internal Func<bool> IsSortedFunc { get; set;} = () => false;
    internal Func<bool> IsSortedDescendingFunc { get; set;} = () => false;
    internal Func<string> GetFilterFunc { get; set; } = () => string.Empty;
    internal Func<string,string> SetFilterFunc { get; set; } = (x) => x;
}