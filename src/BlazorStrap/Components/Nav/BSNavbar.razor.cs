using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSNavbar : BootstrapComponentBase
    {
        protected private string classname =>
        new CssBuilder("navbar")
            .AddClass("fixed-top", IsFixedTop)
            .AddClass("fixed-bottom", IsFixedBottom)
            .AddClass("sticky-top", IsStickyTop)
            .AddClass("navbar-dark", IsDark)
            .AddClass("navbar-light", !IsDark)
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("navbar-expand-lg", IsExpand)
            .AddClass(Class)
        .Build();

        [Parameter] protected Color Color { get; set; } = Color.None;
        [Parameter] protected bool IsDark { get; set; }
        [Parameter] protected bool IsExpand { get; set; }
        [Parameter] protected bool IsFixedTop { get; set; }
        [Parameter] protected bool IsFixedBottom { get; set; }
        [Parameter] protected bool IsStickyTop { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
