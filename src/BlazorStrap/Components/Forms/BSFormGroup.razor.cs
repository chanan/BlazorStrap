using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class BSFormGroupBase  : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("form-check", IsCheck)
            .AddClass("form-group", !IsCheck)
            .AddClass("row", IsRow)
            .AddClass(Class)
        .Build();

        [Parameter] public bool IsRow { get; set; }
        [Parameter] public bool IsCheck { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
