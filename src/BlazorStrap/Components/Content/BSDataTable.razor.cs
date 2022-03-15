using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSDataTable<TValue> : BSTable
    {
        [Parameter] public Func<int, string, bool, string, string, Task<IEnumerable<TValue>>> DataSet { get; set; }
        [Parameter] public Func<int> TotalRecords { get; set; }
        [Parameter] public RenderFragment<TValue>? Body { get; set; }
        [Parameter] public RenderFragment? Header { get; set; }
        [Parameter] public RenderFragment? Footer { get; set; }
        [Parameter, AllowNull] public RenderFragment? NoData { get; set; }
        [Parameter] public bool PaginationTop { get; set; }
        [Parameter] public bool PaginationBottom { get; set; } = true;
        [Parameter] public int RowsPerPage { get; set; }= 20;
        
        [Parameter] public int StartPage { get; set; } = 1;
        internal Func<string, bool, Task>? OnSort;
        internal Func<string, Task>? OnFilter;
        private IList<TValue> Items { get; set; }
        private int Page { get; set; } = 1;
        private int _itemCount;
        private string _sortColumn = "";
        private string _filterColumn = "";
        private string _filterValue = "";
        private bool _desc;
        protected override async Task OnInitializedAsync()
        {
            Page = StartPage;
            _itemCount = TotalRecords.Invoke();
            Items = (await DataSet.Invoke(Page -1, _sortColumn , false, _filterColumn, _filterValue)).ToList();
        }
        
        private async Task ChangePage(int page)
        {
            Items = (await DataSet.Invoke(page -1, _sortColumn, _desc, _filterColumn, _filterValue)).ToList();
            Page = page;
        }
        private int GetPages()
        {
            var value = _itemCount / RowsPerPage;
            
            return value < 1 ? 1: value;
        }

        public async Task Refresh()
        {
            _itemCount = TotalRecords.Invoke();
            Items = (await DataSet.Invoke(Page -1, _sortColumn, _desc, _filterColumn, _filterValue)).ToList();
            await InvokeAsync(StateHasChanged);
        }
        public async Task FilterAsync(string name, string value)
        {
            Page = 1;
            _filterValue = value;
            _filterColumn = name;
            OnFilter?.Invoke(name);
            Items = (await DataSet.Invoke(Page -1, _sortColumn, _desc, _filterColumn, _filterValue)).ToList();
            _itemCount = TotalRecords.Invoke();
            await InvokeAsync(StateHasChanged);
        }
        public async Task SortAsync(string name)
        {
            Console.WriteLine(".." + name);
            if (_sortColumn != name)
                _desc = false;
            else
                _desc = !_desc;
            _sortColumn = name;
            
            Items = (await DataSet.Invoke(Page -1, _sortColumn, _desc, _filterColumn, _filterValue)).ToList();
            OnSort?.Invoke(name, _desc);
            await InvokeAsync(StateHasChanged);
        }
    }
}