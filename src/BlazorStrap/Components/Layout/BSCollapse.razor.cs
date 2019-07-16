using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSCollapse : ToggleableComponentBase
    {
        protected string classname =>
         new CssBuilder("collapse")
             .AddClass("navbar-collapse", IsNavbar)
             .AddClass("show", IsOpen.HasValue && IsOpen.Value)
             .AddClass(Class)
         .Build();

        [Parameter] protected bool IsNavbar { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}