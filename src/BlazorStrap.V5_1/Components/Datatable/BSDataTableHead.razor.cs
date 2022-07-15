using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Datatable;

namespace BlazorStrap.V5_1
{
    public partial class BSDataTableHead<TValue> : BSDataTableHeadBase<TValue>
    {
        protected override string? SortClassBuilder => new CssBuilder()
                .AddClass("sort-by", Desc == null)
                .AddClass("sort", Desc == false)
                .AddClass("sort-desc", Desc == true)
                .Build().ToNullString();

        protected override string? LayoutClass => null;

        protected override string? ClassBuilder => null;
    }
}