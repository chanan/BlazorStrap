using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSFormGroup : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("form-check", IsCheck)
            .AddClass("form-group", !IsCheck)
            .AddClass("row", IsRow)
            .AddClass(Class)
        .Build();

        [Parameter] protected bool IsRow { get; set; }
        [Parameter] protected bool IsCheck { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
