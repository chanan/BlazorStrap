using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSLabel : ColumnBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
           .AddClass("form-check-label", IsCheck)
           .AddClass("col-form-label", GetColumnClass(null) != null)
           .AddClass(GetColumnClass(null))
           .AddClass(Class)
        .Build();

        [Parameter] public bool IsCheck { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
