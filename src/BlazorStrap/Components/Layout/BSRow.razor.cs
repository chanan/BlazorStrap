using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSRow : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder()
            .AddClass("row no-gutters", NoGutters)
            .AddClass("row", !NoGutters)
            .AddClass(Class)
        .Build();

        [Parameter] protected bool NoGutters { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
