using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSContainer : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder()
            .AddClass("container", !IsFluid)
            .AddClass("container-fluid", IsFluid)
            .AddClass(Class)
        .Build();

        [Parameter] protected bool IsFluid { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
