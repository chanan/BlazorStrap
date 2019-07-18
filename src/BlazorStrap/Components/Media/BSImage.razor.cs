using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSImage : BootstrapComponentBase
    {
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
