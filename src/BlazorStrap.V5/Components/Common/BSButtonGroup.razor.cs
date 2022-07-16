using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;

namespace BlazorStrap.V5
{
    public partial class BSButtonGroup : BSButtonGroupBase
    {
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("btn-group", !IsToolbar && !IsVertical)
                .AddClass("btn-toolbar", IsToolbar)
                .AddClass("dropup", DropdownPlacement is Placement.Top or Placement.TopEnd or Placement.TopStart)
                .AddClass("dropstart", DropdownPlacement is Placement.Left or Placement.LeftEnd or Placement.LeftStart)
                .AddClass("dropend", DropdownPlacement is Placement.Right or Placement.RightEnd or Placement.RightStart)
                .AddClass("btn-group-vertical", IsVertical && !IsToolbar)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}
