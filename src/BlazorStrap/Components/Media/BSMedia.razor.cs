using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSMediaBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
         new CssBuilder()
             .AddClass(GetClass())
             .AddClass(Class)
         .Build();

        protected string Tag => MediaType switch
        {
            MediaType.Body => "div",
            MediaType.Media => "div",
            MediaType.Heading => "h5",
            MediaType.Image => "img",
            MediaType.List => "ul",
            MediaType.ListItem => "li",
            _ => "div"
        };

        [Parameter] public MediaType MediaType { get; set; } = MediaType.Media;
        [Parameter] public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.None;
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        private string GetVerticalAlignmnet()
        {
            if (VerticalAlignment == VerticalAlignment.Center) { return "align-self-center"; }
            if (VerticalAlignment == VerticalAlignment.Bottom) { return "align-self-end"; }
            if (VerticalAlignment == VerticalAlignment.Bottom) { return "align-self-start"; }
            return null;
        }

        private string GetClass() => this.MediaType switch
        {
            MediaType.Media => "media",
            MediaType.Heading => "mt-0 mb-1",
            MediaType.Image => $"{GetVerticalAlignmnet()} mr-3",
            MediaType.List => "list-unstyled",
            MediaType.ListItem => "media",
            _ => "media"
        };
    }
}
