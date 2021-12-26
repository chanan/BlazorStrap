using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSButtonGroup : BlazorStrapBase
    {
        [Parameter] public Placement DropdownPlacement { get; set; } = Placement.BottomStart;
        [Parameter] public bool IsToolbar { get; set; }
        [Parameter] public bool IsVertical { get; set; }


        private string? ClassBuilder => new CssBuilder()
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
