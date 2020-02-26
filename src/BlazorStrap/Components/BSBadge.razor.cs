using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSBadgeBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string Classname =>
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
                Tag = value != null || OnClick.HasDelegate ? "a" : "span";
            }
        }
        private EventCallback<MouseEventArgs> _onClick { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick
        {
            get => _onClick;

            set
            {
                _onClick = value;
                Tag = value.HasDelegate || Href != null ? "a" : "span";
            }
        }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected void MyOnClick(MouseEventArgs e)
        {
            if (OnClick.HasDelegate) OnClick.InvokeAsync(e);
        }
    }
}
