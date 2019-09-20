using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSImageBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
        new CssBuilder()
            .AddClass("rounded", IsRounded)
            .AddClass("img-thumbnail", IsThumbnail)
            .AddClass("img-fluid", IsResponsive)
            .AddClass(GetAlignment())
            .AddClass(Class)
        .Build();

        [Parameter] public bool IsThumbnail { get; set; }
        [Parameter] public bool IsResponsive { get; set; }
        [Parameter] public bool IsRounded { get; set; }
        [Parameter] public Alignment Alignment { get; set; }
        [Parameter] public string Class { get; set; }

        private string GetAlignment()
        {
            if (Alignment == Alignment.Left) { return "float-left"; }
            if (Alignment == Alignment.Center) { return "mx-auto d-block"; }
            if (Alignment == Alignment.Right) { return "float-right"; }
            return null;
        }
    }
}
