using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V5_1
{
    public partial class BSPagination : BSPaginationBase<Size>
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("pagination")
                .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass($"pagination-{Size.ToDescriptionString()}", Size != Size.None)
                .AddClass($"justify-content-{Align.NameToLower()}", Align != Align.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}