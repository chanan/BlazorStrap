using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSFormFeedback : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("valid-tooltip", IsValid && IsTooltip)
            .AddClass("valid-feedback", IsValid && !IsTooltip)
            .AddClass("invalid-tooltip", IsInvalid && IsTooltip)
            .AddClass("invalid-feedback", IsInvalid && !IsTooltip)
            .AddClass(Class)
        .Build();

        [Parameter] protected bool IsValid { get; set; }
        [Parameter] protected bool IsInvalid { get; set; }
        [Parameter] protected bool IsTooltip { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
