using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSCarouselIndicatorsBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder("carousel-indicators")
        .AddClass(Class)
        .Build();

        [Parameter] public int NumberOfItems { get; set; }
        [Parameter] public int ActiveIndex { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChangedEvent { get; set; }

        protected void ClickEventActiveIndex(int index)
        {
            ActiveIndex = index;
            ActiveIndexChanged.InvokeAsync(index);
            ActiveIndexChangedEvent.InvokeAsync(index);
        }
    }
}
