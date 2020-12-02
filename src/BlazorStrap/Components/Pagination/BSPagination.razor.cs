using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSPagination : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
         new CssBuilder("pagination")
             .AddClass($"pagination-{Size.ToDescriptionString()}", Size != Size.None)
             .AddClass(GetAlignment())
             .AddClass(Class)
         .Build();

        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public Alignment Alignment { get; set; } = Alignment.Left;
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        private string GetAlignment()
        {
            if (Alignment == Alignment.Center) { return "justify-content-center"; }
            if (Alignment == Alignment.Right) { return "justify-content-end"; }
            return null;
        }
    }
}
