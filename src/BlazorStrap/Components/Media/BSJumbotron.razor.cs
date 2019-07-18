using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSJumbotron : BootstrapComponentBase
    {
        protected string classname =>
         new CssBuilder()
             .AddClass("jumbotron jumbotron-fluid", IsFluid)
             .AddClass("jumbotron", !IsFluid)
             .AddClass(Class)
         .Build();

        [Parameter] protected bool IsFluid { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
