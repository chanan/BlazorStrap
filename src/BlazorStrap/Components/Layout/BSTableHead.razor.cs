using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class BSTableHeadBase  : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
             new CssBuilder()
                 .AddClass("thead-light", TableHeadType == TableHeadType.Light)
                 .AddClass("thead-dark", TableHeadType == TableHeadType.Dark)
                 .AddClass(Class)
             .Build();

        [Parameter] public TableHeadType TableHeadType { get; set; } = TableHeadType.None;
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
