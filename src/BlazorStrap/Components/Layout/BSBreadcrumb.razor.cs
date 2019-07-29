using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBsBreadcrumb : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder()
           .AddClass("breadcrumb")
           .AddClass(Class)
        .Build();

        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}