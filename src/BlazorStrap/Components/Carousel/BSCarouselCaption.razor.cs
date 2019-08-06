using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSCarouselCaption : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("carousel-caption d-none d-md-block")
        .AddClass(Class)
        .Build();

        [Parameter] protected string Class { get; set; }
        [Parameter] protected string CaptionText { get; set; }
        [Parameter] protected string HeaderText { get; set; }
    }
}
