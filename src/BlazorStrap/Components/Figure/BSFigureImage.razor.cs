using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSFigureImageBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
        new CssBuilder("figure-img img-fluid rounded")
            .AddClass(Class)
            .Build();

        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
    }
}
