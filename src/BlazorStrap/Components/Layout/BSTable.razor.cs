using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSTable : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
        new CssBuilder("table")
            .AddClass("table-dark", IsDark)
            .AddClass("table-striped", IsStriped)
            .AddClass("table-bordered", IsBordered)
            .AddClass("table-borderless", IsBorderless)
            .AddClass("table-hover", IsHoverable)
            .AddClass("table-sm", IsSmall)
            .AddClass(Class)
        .Build();

        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsStriped { get; set; }
        [Parameter] public bool IsBordered { get; set; }
        [Parameter] public bool IsBorderless { get; set; }
        [Parameter] public bool IsHoverable { get; set; }
        [Parameter] public bool IsSmall { get; set; }
        [Parameter] public bool IsResponsive { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
