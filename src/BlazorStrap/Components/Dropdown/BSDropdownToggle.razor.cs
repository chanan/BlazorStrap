using BlazorComponentUtilities;
using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSDropdownToggleBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }

        protected string Classname =>
         new CssBuilder()
             .AddClass("btn", !IsLink)
             .AddClass("dropdown-item", Dropdown?.IsSubmenu == true)
             .AddClass($"btn-{Size.ToDescriptionString()}", !IsLink && Size != Size.None)
             .AddClass($"btn-{Color.ToDescriptionString()}", !IsLink && Color != Color.None)
             .AddClass("dropdown-toggle-split", IsSplit)
             .AddClass("dropdown-toggle")
             //nav-link should only show on root drop down toggle
             .AddClass("nav-link", IsLink && Dropdown?.NavItem != null && Dropdown?.IsSubmenu == false)
             .AddClass(Class)
         .Build();

        protected string Tag => IsLink ? "a" : "button";
        protected string Type => IsLink ? null : "button";
        protected string Href => IsLink ? "javascript:void(0)" : null;

        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public bool IsLink { get; set; }

        [Obsolete("This Parameter is no longer require and will be removed soon")]
        [Parameter] public bool? IsOpen { get; set; }

        [Parameter] public bool IsSplit { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BSDropdownMenuBase Dropdown { get; set; }

        protected void Escape(KeyboardEventArgs e)
        {
            if (Dropdown == null)
                return;
            if (e?.Key.ToUpperInvariant() == "ESCAPE")
            {
                Dropdown.Hide();
            }
        }

        protected void OnClickEvent(MouseEventArgs e)
        {
            OnClick.InvokeAsync(e);
            if (Dropdown == null)
                return;
            if (!Dropdown.Manual)
            {
                Dropdown.Toggle();
            }
        }
    }
}
