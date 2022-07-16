using BlazorStrap.Shared.Components.Content;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace BlazorStrap.Shared.Components.Datatable
{
    public abstract class BSDataTableBase<TValue> : BSTableBase
    {
        /// <summary>
        /// Items to drive the data table.
        /// </summary>
        /// <remarks>
        /// Not required when <see cref="FetchItems"/> is used.
        /// <c>StateHasChanged()</c> should be called after the Enumerable has been updated.
        /// </remarks>
        [Parameter] public IEnumerable<TValue>? Items { get; set; }

        /// <summary>
        /// Event called when table is sorted, filtered, or page has changed.
        /// </summary>
        [Parameter] public EventCallback<DataRequest> OnChange { get; set; }

        /// <summary>
        /// Data task to fetch items for table.
        /// </summary>
        /// <remarks>Not required when <see cref="Items"/> is used</remarks>
        [Parameter] public Func<DataRequest, Task<(IEnumerable<TValue>, int)>>? FetchItems { get; set; }

        /// <summary>
        /// Total number of items in <see cref="Items"/>.
        /// </summary>
        /// <remarks>
        /// <para>Only used when either <see cref="PaginationTop"/> or <see cref="PaginationBottom"/> is true.</para>
        /// Not required when <see cref="FetchItems"/> is used.
        /// <c>StateHasChanged()</c> should be called after this is updated.
        /// </remarks>
        [Parameter] public int TotalItems { get; set; }

        /// <summary>
        /// Table row content. Should be type <see cref="BSDataTableRow"/>.
        /// This will be repeated for each item in the dataset.
        /// </summary>
        [Parameter] public RenderFragment<TValue>? Body { get; set; }

        /// <summary>
        /// Table header content. Should be type <see cref="BSDataTableHead{TValue}"/>
        /// </summary>
        [Parameter] public RenderFragment? Header { get; set; }

        /// <summary>
        /// Content for table footer.
        /// </summary>
        [Parameter] public RenderFragment? Footer { get; set; }

        /// <summary>
        /// Content displayed when <see cref="Items"/> is empty.
        /// </summary>
        [Parameter, AllowNull] public RenderFragment? NoData { get; set; }

        /// <summary>
        /// Enable page navigation at the top of the table.
        /// </summary>
        [Parameter] public bool PaginationTop { get; set; }

        /// <summary>
        /// Enable page navigation at the bottom of the table.
        /// </summary>
        [Parameter] public bool PaginationBottom { get; set; } = true;

        /// <summary>
        /// Sets the number of rows per page. 
        /// </summary>
        /// <remarks>
        /// Only used when either <see cref="PaginationTop"/> or <see cref="PaginationBottom"/> is true.
        /// </remarks>
        [Parameter] public int RowsPerPage { get; set; } = 20;

        /// <summary>
        /// Page to start on.
        /// </summary>
        /// <remarks>
        /// Only used when either <see cref="PaginationTop"/> or <see cref="PaginationBottom"/> is true.
        /// </remarks>
        [Parameter] public int StartPage { get; set; } = 1;

        public Func<string, bool, Task>? OnSort { get; set; }
        public Func<string, Task>? OnFilter { get; set; }

    /// <summary>
    /// Active page number.
    /// </summary>
    /// <remarks>
    /// Only used when either <see cref="PaginationTop"/> or <see cref="PaginationBottom"/> is true.
    /// </remarks>
    public int Page { get; set; } = 1;

        private string _sortColumn = "";
        private string _filterColumn = "";
        private string _filterValue = "";
        private bool _desc;

        protected override async Task OnInitializedAsync()
        {
            Page = StartPage;
            //Might be Prerendered
            try
            {
                if (FetchItems != null)
                {
                    var data = await FetchItems.Invoke(DataRequest(Page - 1));
                    Items = data.Item1;
                    TotalItems = data.Item2;
                    await InvokeAsync(StateHasChanged);
                }
            }
            catch { }
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
        protected async Task ChangePage(int page)
        {
            if (FetchItems != null)
            {
                var data = await FetchItems.Invoke(DataRequest(page - 1));
                Items = data.Item1;
                TotalItems = data.Item2;
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                await OnChange.InvokeAsync(DataRequest(page - 1));
            }

            Page = page;
        }
        protected int GetPages()
        {
            var value = 0;
            value = (int)Math.Ceiling(((float)TotalItems / RowsPerPage));
            return value < 1 ? 1 : value;
        }

        /// <summary>
        /// Refreshes data in table and invokes <c>StateHasChanged()</c>
        /// </summary>
        /// <remarks>
        /// If a <see cref="FetchItems"/> function is passed, it will run this function.
        /// If it is not passed, the table will be rerendered with any updates from <see cref="Items"/>.
        /// </remarks>
        /// <returns>Completed task when data refresh is complete.</returns>
        public async Task Refresh()
        {
            if (FetchItems != null)
            {
                var data = await FetchItems.Invoke(DataRequest(Page - 1));
                Items = data.Item1;
                TotalItems = data.Item2;
                await InvokeAsync(StateHasChanged);
            }
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Applies a filter to the table.
        /// </summary>
        /// <param name="name">Column name to filter. Must match <see cref="BSDataTableHead{TValue}.Column"/> value.</param>
        /// <param name="value">Value to filter by.</param>
        /// <remarks>
        /// If a <see cref="FetchItems"/> function is passed, it will run this function before filtering.
        /// </remarks>
        /// <returns>Completed task once the filter is complete.</returns>
        public async Task FilterAsync(string name, string value)
        {
            Page = 1;
            _filterValue = value;
            _filterColumn = name;
            OnFilter?.Invoke(name);
            if (FetchItems != null)
            {
                var data = await FetchItems.Invoke(DataRequest(Page - 1));
                Items = data.Item1;
                TotalItems = data.Item2;
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                await OnChange.InvokeAsync(DataRequest(Page - 1));
            }
        }

        /// <summary>
        /// Sort the table.
        /// </summary>
        /// <param name="name">Column name to sort by.</param>
        /// <remarks>
        /// If a <see cref="FetchItems"/> function is passed, it will run this function before filtering.
        /// </remarks>
        /// <returns>Completed task once the filter is complete.</returns>
        public async Task SortAsync(string name)
        {
            _desc = _sortColumn != name ? false : !_desc;
            _sortColumn = name;

            if (FetchItems != null)
            {
                var data = await FetchItems.Invoke(DataRequest(Page - 1));
                Items = data.Item1;
                TotalItems = data.Item2;
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                await OnChange.InvokeAsync(DataRequest(Page - 1));
            }

            OnSort?.Invoke(name, _desc);
        }
    }
}
