using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSTableHead : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
             new CssBuilder()
                 .AddClass("thead-light", TableHeadType == TableHeadType.Light)
                 .AddClass("thead-dark", TableHeadType == TableHeadType.Dark)
                 .AddClass(Class)
             .Build();

        [Parameter] protected TableHeadType TableHeadType { get; set; } = TableHeadType.None;
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
