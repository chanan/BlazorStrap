using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSCarouselCaption : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
        new CssBuilder("carousel-caption d-none d-md-block")
        .AddClass(Class)
        .Build();

        [Parameter] public string Class { get; set; }
        [Parameter] public string CaptionText { get; set; }
        [Parameter] public string HeaderText { get; set; }
    }
}
