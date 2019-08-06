using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSPaginationItem : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("page-item")
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass(Class)
        .Build();

        [Parameter] protected bool IsActive { get; set; }
        [Parameter] protected bool IsDisabled { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
