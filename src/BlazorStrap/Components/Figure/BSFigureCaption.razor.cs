using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSFigureCaptionBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
        new CssBuilder("figure-caption")
            .AddClass(GetAlignmentAsString())
            .AddClass(Class)
            .Build();

        [Parameter] public Alignment Alignment { get; set; } = Alignment.None;
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected string GetAlignmentAsString()
        {
            return Alignment == Alignment.Center ? "text-center" : Alignment == Alignment.Right ? "text-right" : null;
        }
    }
}
