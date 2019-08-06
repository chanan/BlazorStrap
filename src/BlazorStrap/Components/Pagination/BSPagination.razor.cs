using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSPagination : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
         new CssBuilder("pagination")
             .AddClass($"pagination-{Size.ToDescriptionString()}", Size != Size.None)
             .AddClass(GetAlignment())
             .AddClass(Class)
         .Build();

        [Parameter] protected Size Size { get; set; } = Size.None;
        [Parameter] protected Alignment Alignment { get; set; } = Alignment.Left;
        [Parameter] protected string Class { get; set; }
        [Parameter] protected string Style { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        private string GetAlignment()
        {
            if (Alignment == Alignment.Center) { return "justify-content-center"; }
            if (Alignment == Alignment.Right) { return "justify-content-end"; }
            return null;
        }
    }
}
