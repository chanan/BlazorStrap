using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSNavbar : BlazorStrapBase
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public Size Expand { get; set; } = Size.Medium;
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsFixedBottom { get; set; }
        [Parameter] public bool IsFixedTop { get; set; }
        [Parameter] public bool IsHeader { get; set; }
        [Parameter] public bool IsStickyTop { get; set; }

        private string? ClassBuilder => new CssBuilder("navbar")
            .AddClass("navbar-light", !IsDark)
            .AddClass("navbar-dark", IsDark)
            .AddClass("fixed-top", IsFixedTop)
            .AddClass("fixed-bottom", IsFixedBottom)
            .AddClass("sticky-top", IsStickyTop)
            .AddClass($"navbar-expand-{Expand.ToDescriptionString().ToLower()}", Expand != Size.None)
            .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}