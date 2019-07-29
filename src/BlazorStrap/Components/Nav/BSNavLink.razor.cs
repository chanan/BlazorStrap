using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSNavLink : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder("nav-item nav-link")
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass(Class)
        .Build();

        protected string disabled => IsDisabled ? "disabled" : null;

        [Parameter] protected bool IsActive { get; set; }
        [Parameter] protected bool IsDisabled { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
