using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSPaginationItem : BSPaginationItemBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("page-item")
            .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}