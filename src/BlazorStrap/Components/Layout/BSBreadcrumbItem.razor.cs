using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBsBreadcrumbItem : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder("breadcrumb-item")
            .AddClass("active", IsActive)
            .AddClass(Class)
        .Build();

        protected string aria => IsActive ? "page" : null;

        [Parameter] protected bool IsActive { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}