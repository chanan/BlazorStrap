using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSFigureImage : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("figure-img img-fluid rounded")
            .AddClass(Class)
            .Build();

        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
    }
}
