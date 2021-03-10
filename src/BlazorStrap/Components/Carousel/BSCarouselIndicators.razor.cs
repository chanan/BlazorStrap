﻿using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public partial class BSCarouselIndicators : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [CascadingParameter] protected BSCarousel Parent { get; set; }

        protected string Classname =>
        new CssBuilder("carousel-indicators")
        .AddClass(Class)
        .Build();

        [Parameter] public int ActiveIndex { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChanged { get; set; }
        [Parameter] public EventCallback<int> ActiveIndexChangedEvent { get; set; }
        private int _numberOfItems { get => Parent.NumberOfItems; }

        protected void ClickEventActiveIndex(int index)
        {
            ActiveIndex = index;
            ActiveIndexChanged.InvokeAsync(index);
            ActiveIndexChangedEvent.InvokeAsync(index);
        }
    }
}
