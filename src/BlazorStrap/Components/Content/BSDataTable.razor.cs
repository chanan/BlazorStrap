using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSDataTable<TValue> : BSTable
    {
        [Parameter] public RenderFragment<TValue>? Body { get; set; }
        [Parameter] public RenderFragment? Header { get; set; }
        [Parameter] public RenderFragment? Footer { get; set; }
        [Parameter, AllowNull] public RenderFragment? NoData { get; set; }
        [Parameter] public IReadOnlyList<TValue> Items { get; set; }
        [Parameter] public bool Filter { get; set; }
        [Parameter] public bool PaginationTop { get; set; }
        [Parameter] public bool PaginationBottom { get; set; } = true;
        [Parameter] public int RowHeight { get; set; } = 20;
        [Parameter] public int RowsPerPage { get; set; }= 20;
        [Parameter] public int ItemCount { get; set; }
        [Parameter] public EventCallback<int> PageChanged { get; set; }
        [Parameter] public int StartPage { get; set; } = 1;
        private int Page { get; set; } = 1;

        protected override void OnInitialized()
        {
            Page = StartPage;
        }

        private async Task ChangePage(int page)
        {
            await PageChanged.InvokeAsync(Page);
            Page = page;
        }
        private int GetPages()
        {
            var value = ItemCount / RowsPerPage;
            
            return value < 1 ? 1: value;
        }
    }
}