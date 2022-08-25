using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Datatable;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5
{
    public partial class BSDataTableHead<TValue> : BSDataTableHeadBase<TValue>
    {
        /// <summary>
        /// Size of the filter input.
        /// </summary>
        [Parameter] public Size FilterSize { get; set; }

        protected override string? SortClassBuilder => new CssBuilder()
                .AddClass("sort-by", Desc == null)
                .AddClass("sort", Desc == false)
                .AddClass("sort-desc", Desc == true)
                .Build().ToNullString();

        protected override string? LayoutClass => null;

        protected override string? ClassBuilder => null;
    }
}