using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSLabel : ColumnBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
           .AddClass("form-check-label", IsCheck)
           .AddClass("col-form-label", GetColumnClass(null) != null)
           .AddClass(GetColumnClass(null))
           .AddClass(Class)
        .Build();

        [Parameter] protected bool IsCheck { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
