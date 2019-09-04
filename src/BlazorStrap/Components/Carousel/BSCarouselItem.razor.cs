using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSCarouselItemBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
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

        [Parameter] public string src { get; set; }
        [Parameter] public string alt { get; set; }
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public string ActionLink { get; set; }
        [Parameter] public string ActionLinkTarget { get; set; } = "_self";
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
