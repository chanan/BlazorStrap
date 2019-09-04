using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public abstract class BSModalHeaderBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string divClassname =>
            new CssBuilder("modal-header")
                .AddClass(Class)
            .Build();

        protected string headingClassname =>
            new CssBuilder("modal-title")
                .AddClass(HeadingClass)
            .Build();

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public string HeadingClass { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
