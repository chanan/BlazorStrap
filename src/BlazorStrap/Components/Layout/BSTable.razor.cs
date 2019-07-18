using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSTable : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder("table")
            .AddClass("table-dark", IsDark)
            .AddClass("table-striped", IsStriped)
            .AddClass("table-bordered", IsBordered)
            .AddClass("table-borderless", IsBorderless)
            .AddClass("table-hover", IsHovarable)
            .AddClass("table-sm", IsSmall)
            .AddClass(Class)
        .Build();

        [Parameter] protected bool IsDark { get; set; }
        [Parameter] protected bool IsStriped { get; set; }
        [Parameter] protected bool IsBordered { get; set; }
        [Parameter] protected bool IsBorderless { get; set; }
        [Parameter] protected bool IsHovarable { get; set; }
        [Parameter] protected bool IsSmall { get; set; }
        [Parameter] protected bool IsResponsive { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}