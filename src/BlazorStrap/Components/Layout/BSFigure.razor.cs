using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSFigure : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder()
            .AddClass("figure")
            .AddClass(Class)
        .Build();

        [Parameter] protected string Class { get; set; }
        [Parameter] protected string Style { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
