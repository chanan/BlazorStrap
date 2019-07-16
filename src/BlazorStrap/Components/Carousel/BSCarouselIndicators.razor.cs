using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSCarouselIndicators : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder("carousel-indicators")
        .AddClass(Class)
        .Build();

        [Parameter] protected int NumberOfItems { get; set; }
        [Parameter] protected int ActiveIndex { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] protected EventCallback<int> ActiveIndexChangedEvent { get; set; }

        protected void ClickEventActiveIndex(int index)
        {
            ActiveIndex = index;
            ActiveIndexChanged.InvokeAsync(index);
            ActiveIndexChangedEvent.InvokeAsync(index);
        }
    }
}
