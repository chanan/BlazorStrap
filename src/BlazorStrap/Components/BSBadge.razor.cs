using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSBadgeBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
        new CssBuilder().AddClass("badge")
            .AddClass("badge-pill", IsPill)
            .AddClass($"badge-{Color.ToDescriptionString()}")
            .AddClass(Class)
        .Build();

        protected string Tag { get; set; } = "span";

        [Parameter] public Color Color { get; set; } = Color.Primary;
        [Parameter] public bool IsPill { get; set; }
        private string _href;
        [Parameter]
        public string Href
        {
            get => _href;
            set
            {
                _href = value;
                if (value != null || onclick.HasDelegate) { Tag = "a"; }
                else { Tag = "span"; }
            }
        }
        private EventCallback<UIMouseEventArgs> _onlick { get; set; }
        [Parameter]
        public EventCallback<UIMouseEventArgs> onclick
        {
            get => _onlick;

            set
            {
                _onlick = value;
                if (value.HasDelegate || Href != null) { Tag = "a"; }
                else { Tag = "span"; }
            }
        }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected void _onclick(UIMouseEventArgs e)
        {
            if (onclick.HasDelegate) onclick.InvokeAsync(e);
        }
    }
}
