using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Interfaces
{
    public interface IBSDataTable<TValue>
    {
        RenderFragment<TValue>? Body { get; set; }
        Func<DataRequest, Task<(IEnumerable<TValue>, int)>>? FetchItems { get; set; }
        RenderFragment? Footer { get; set; }
        RenderFragment? Header { get; set; }
        IEnumerable<TValue>? Items { get; set; }
        RenderFragment? NoData { get; set; }
        EventCallback<DataRequest> OnChange { get; set; }
        int Page { get; set; }
        bool PaginationBottom { get; set; }
        bool PaginationTop { get; set; }
        int RowsPerPage { get; set; }
        int StartPage { get; set; }
        int TotalItems { get; set; }
        Func<string, bool, Task>? OnSort { get; set; }
        Func<string, Task>? OnFilter { get; set; }
        Task FilterAsync(string name, string value);
        Task Refresh();
        Task SortAsync(string name);
    }
}