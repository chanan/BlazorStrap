using BlazorStrap.Shared.Components.Content;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Datatable
{
    public abstract class BSDataTableHeadBase<TValue> : BSTDBase, IDisposable
    {
        /// <summary>
        /// Column Name
        /// </summary>
        [Parameter] public string? Column { get; set; }

        /// <summary>
        /// Enables sorting for this column
        /// </summary>
        [Parameter] public bool Sortable { get; set; }

        /// <summary>
        /// Enables a filter for this column.
        /// </summary>
        [Parameter] public bool ColumnFilter { get; set; }

        [CascadingParameter] public BSDataTableBase<TValue>? Parent { get; set; }
        protected bool? Desc;
        protected string? Filter { get; set; }
        protected abstract string? SortClassBuilder { get; }
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

        protected async Task FilterChanged(string e)
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
            Desc = name != Column ? null : desc;
            await InvokeAsync(StateHasChanged);
        }

        protected Task SortAsync()
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
