using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V4
{
    public partial class BSNavItem : BSNavItemBase
    {
        protected override Task ChildHandler(BSNavItemBase sender)
        {
            return Task.CompletedTask;
        }

        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("nav-link")
                .AddClass("active", IsActive ?? false)
                .AddClass("disabled", IsDisabled)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? ListClassBuilder => new CssBuilder()
                .AddClass("nav-item", !NoNavItem)
                .AddClass("dropdown", IsDropdown)
                .AddClass(ListItemClass)
                .Build().ToNullString();
    }
}