using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSBadge : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder().AddClass("badge")
            .AddClass("badge-pill", IsPill)
            .AddClass($"badge-{Color.ToDescriptionString()}")
            .AddClass(Class)
        .Build();

        protected string Tag { get; set; } = "span";

        [Parameter] protected Color Color { get; set; } = Color.Primary;
        [Parameter] protected bool IsPill { get; set; }
        private string _href;
        [Parameter]
        protected string Href
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
        protected EventCallback<UIMouseEventArgs> onclick
        {
            get => _onlick;

            set
            {
                _onlick = value;
                if (value.HasDelegate || Href != null) { Tag = "a"; }
                else { Tag = "span"; }
            }
        }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected void _onclick(UIMouseEventArgs e)
        {
            if (onclick.HasDelegate) onclick.InvokeAsync(e);
        }
    }
}
