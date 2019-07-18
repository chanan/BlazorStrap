using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSCarouselItem : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder("carousel-item")
        .AddClass("active", IsActive)
        .AddClass(Class)
        .Build();

        protected bool AddActionLink
        {
            get
            {
                return !String.IsNullOrEmpty(ActionLink);
            }
        }

        [Parameter] protected string src { get; set; }
        [Parameter] protected string alt { get; set; }
        [Parameter] protected bool IsActive { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected string ActionLink { get; set; }
        [Parameter] protected string ActionLinkTarget { get; set; } = "_self";
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
