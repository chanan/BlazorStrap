using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSCarouselItem : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
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
