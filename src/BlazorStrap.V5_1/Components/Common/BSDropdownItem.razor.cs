using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V5_1
{
    public partial class BSDropdownItem : BSDropdownItemBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("dropdown-item-text", IsText)
                .AddClass("dropdown-item", !IsText && !IsSubmenu)
                .AddClass("dropdown-header", Header != 0)
                .AddClass("active", IsActive ?? false)
                .AddClass(SubmenuClass, IsSubmenu)
                .AddClass("disabled", IsDisabled)
                .AddClass("dropup", IsSubmenu && Parent?.Placement is Placement.Top or Placement.TopEnd or Placement.TopStart)
                .AddClass("dropstart", IsSubmenu && Parent?.Placement is Placement.Left or Placement.LeftEnd or Placement.LeftStart)
                .AddClass("dropend", IsSubmenu && Parent?.Placement is Placement.Right or Placement.RightEnd or Placement.RightStart)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}