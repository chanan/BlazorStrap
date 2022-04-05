using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSDataTable<TValue> : BSTable
    {
        
        [Parameter] public IEnumerable<TValue>? Items { get; set; }
      
        [Parameter] public EventCallback<DataRequest> OnChange { get; set; }
        [Parameter] public Func<DataRequest, Task<IEnumerable<TValue>>>? FetchItems { get; set; }
        [Parameter] public int TotalItems { get; set; }

        [Parameter] public string Name { get; set; }

        [Obsolete]
        [Parameter] public Func<int, string, bool, string, string, Task<IEnumerable<TValue>>>? DataSet { get; set; }
        [Obsolete]
        [Parameter] public Func<int>? TotalRecords { get; set; }

        [Parameter] public RenderFragment<TValue>? Body { get; set; }
        [Parameter] public RenderFragment? Header { get; set; }
        [Parameter] public RenderFragment? Footer { get; set; }
        [Parameter, AllowNull] public RenderFragment? NoData { get; set; }
        [Parameter] public bool PaginationTop { get; set; }
        [Parameter] public bool PaginationBottom { get; set; } = true;
        [Parameter] public int RowsPerPage { get; set; } = 20;

        [Parameter] public int StartPage { get; set; } = 1;
        internal Func<string, bool, Task>? OnSort;
        internal Func<string, Task>? OnFilter;
        private int Page { get; set; } = 1;
        private int _itemCount;
        private string _sortColumn = "";
        private string _filterColumn = "";
        private string _filterValue = "";
        private bool _desc;

        protected override async Task OnInitializedAsync()
        {
            Page = StartPage;
          
        }

        private DataRequest DataRequest(int page)
        {
            return new DataRequest
            {
                Page = page,
                Descending = _desc,
                Filter = _filterValue,
                FilterColumn = _filterColumn,
                FilterColumnProperty = TypeDescriptor.GetProperties(typeof(TValue)).Find(_filterColumn, false),
                SortColumn = _sortColumn,
                SortColumnProperty = TypeDescriptor.GetProperties(typeof(TValue)).Find(_sortColumn, false)
            };
        }
        private async Task ChangePage(int page)
        {
           
            if (TotalRecords != null && DataSet != null)
            {
                Items = (await DataSet.Invoke(page - 1, _sortColumn, _desc, _filterColumn, _filterValue)).ToList();
                await InvokeAsync(StateHasChanged);
            }
            else if(FetchItems != null)
            {
                Items = await FetchItems.Invoke(DataRequest(page - 1));
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                await OnChange.InvokeAsync(DataRequest(page - 1));
            }
            
            Page = page;
        }
        private int GetPages()
        {
            var value = 0;
            if (TotalRecords != null && DataSet != null)
            {
                value = (int)Math.Ceiling(((float)_itemCount / (float)RowsPerPage));
            }
            else
            {
                value = (int)Math.Ceiling(((float)TotalItems / (float)RowsPerPage));
            }

            return value < 1 ? 1 : value;
        }

        public async Task Refresh()
        {
            if (TotalRecords != null && DataSet != null)
            {
                _itemCount = TotalRecords.Invoke();
                Items = (await DataSet.Invoke(Page - 1, _sortColumn, _desc, _filterColumn, _filterValue)).ToList();
            }
            else if (FetchItems != null)
            {
                Items = await FetchItems.Invoke(DataRequest(Page - 1));
                await InvokeAsync(StateHasChanged);
            }
            await InvokeAsync(StateHasChanged);
        }
        public async Task FilterAsync(string name, string value)
        {
            Page = 1;
            _filterValue = value;
            _filterColumn = name;
            OnFilter?.Invoke(name);
            if (TotalRecords != null && DataSet != null)
            {
                Items = (await DataSet.Invoke(Page - 1, _sortColumn, _desc, _filterColumn, _filterValue)).ToList();
                _itemCount = TotalRecords.Invoke();
                await InvokeAsync(StateHasChanged);
            }
            else if (FetchItems != null)
            {
                Items = await FetchItems.Invoke(DataRequest(Page -1));
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                await OnChange.InvokeAsync(DataRequest(Page -1));
            }
        }
        public async Task SortAsync(string name)
        {
            Console.WriteLine(".." + name);
            if (_sortColumn != name)
                _desc = false;
            else
                _desc = !_desc;
            _sortColumn = name;

            if (TotalRecords != null && DataSet != null)
            {
                Items = (await DataSet.Invoke(Page - 1, _sortColumn, _desc, _filterColumn, _filterValue)).ToList();
                await InvokeAsync(StateHasChanged);
            }
            else if (FetchItems != null)
            {
                Items = await FetchItems.Invoke(DataRequest(Page - 1));
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                await OnChange.InvokeAsync(DataRequest(Page - 1));
            }
            
            OnSort?.Invoke(name, _desc);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                if (TotalRecords != null && DataSet != null)
                {
                    _itemCount = TotalRecords.Invoke();
                    Items = (await DataSet.Invoke(Page - 1, _sortColumn, false, _filterColumn, _filterValue)).ToList();
                    await InvokeAsync(StateHasChanged);
                }
                else if (FetchItems != null)
                {
                    Items = await FetchItems.Invoke(DataRequest(Page - 1));
                    await InvokeAsync(StateHasChanged);
                }
            }
        }

    }
}