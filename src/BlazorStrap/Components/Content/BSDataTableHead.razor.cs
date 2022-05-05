using System.Collections.Immutable;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSDataTableHead<TValue> : BSTD, IDisposable
    {
        [Parameter] public string? Column { get; set; }
        [Parameter] public bool Sortable { get; set; }
        [Parameter] public bool ColumnFilter { get; set; }
        [CascadingParameter] public BSDataTable<TValue>? Parent { get; set; }
        private bool? _desc;
        private string? Filter { get; set; }
        internal string? SortClassBuilder => new CssBuilder()
            .AddClass("sort-by", _desc == null)
            .AddClass("sort", _desc == false)
            .AddClass("sort-desc", _desc == true)
            .Build().ToNullString();
        protected override void OnInitialized()
        {
            if (Parent != null)
            {
                Parent.OnSort += OnSort;
                Parent.OnFilter += OnFilter;
            }
        }

        private async Task OnFilter(string name)
        {
            if (name != Column)
                Filter = "";
            await InvokeAsync(StateHasChanged);
        }

        private async Task FilterChanged(string e)
        {
            Filter = e.ToLower();
            if (Parent != null)
                if (string.IsNullOrEmpty(Filter))
                {
                    await Parent.FilterAsync("", Filter);
                }
                else
                {
                    await Parent.FilterAsync(Column ?? "", Filter);
                }
        }
        private async Task OnSort(string name, bool desc)
        {
            _desc = name != Column ? null : desc;
            await InvokeAsync(StateHasChanged);
        }

        private Task SortAsync()
        {
            return Parent != null ? Parent.SortAsync(Column ?? "") : Task.CompletedTask;
        }

        public void Dispose()
        {
            if (Parent != null)
            {
                Parent.OnSort -= OnSort;
                Parent.OnFilter -= OnFilter;
            }
        }
    }
}