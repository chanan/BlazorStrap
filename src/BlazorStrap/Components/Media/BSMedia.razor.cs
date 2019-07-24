using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSMedia : BootstrapComponentBase
    {
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

        [Parameter] protected MediaType MediaType { get; set; } = MediaType.Media;
        [Parameter] protected VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.None;
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

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
