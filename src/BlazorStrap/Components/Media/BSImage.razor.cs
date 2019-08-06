using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSImage : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder()
            .AddClass("rounded", IsRounded)
            .AddClass("img-thumbnail", IsThumbnail)
            .AddClass("img-fluid", IsResponsive)
            .AddClass(GetAlignment())
            .AddClass(Class)
        .Build();

        [Parameter] protected bool IsThumbnail { get; set; }
        [Parameter] protected bool IsResponsive { get; set; }
        [Parameter] protected bool IsRounded { get; set; }
        [Parameter] protected Alignment Alignment { get; set; }
        [Parameter] protected string Class { get; set; }

        private string GetAlignment()
        {
            if (Alignment == Alignment.Left) { return "float-left"; }
            if (Alignment == Alignment.Center) { return "mx-auto d-block"; }
            if (Alignment == Alignment.Right) { return "float-right"; }
            return null;
        }
    }
}
