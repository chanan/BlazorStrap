using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSJumbotron : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
         new CssBuilder()
             .AddClass("jumbotron jumbotron-fluid", IsFluid)
             .AddClass("jumbotron", !IsFluid)
             .AddClass(Class)
         .Build();

        [Parameter] public bool IsFluid { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
