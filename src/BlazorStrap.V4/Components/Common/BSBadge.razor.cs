using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSBadge : BSBadgeBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("badge")
                .AddClass("rounded-pill", IsPill)
                .AddClass($"badge-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}