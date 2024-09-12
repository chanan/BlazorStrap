namespace BlazorStrap;

public interface IColumnHeaderAccessor
{
    Func<Task> RefreshDataTableAsync { get;  }
    Func<Task> FilterButtonClickedAsync { get; }
    bool IsFiltered { get; } 
    bool IsSorted { get; }
    bool IsSortedDescending { get; }
    string Filter { get; set; }
}