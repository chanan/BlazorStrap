using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Timers;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSDropdownToggle : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
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
        protected string href => IsLink ? "javascript:void(0)" : null;

        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public bool IsLink { get; set; }
        [Parameter] public bool IsSplit { get; set; }
        [Parameter] public bool? IsOpen { get; set; }
        [Parameter] public EventCallback<UIMouseEventArgs> OnClick { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal CodeBSDropdownMenu Dropdown { get; set; }
        protected void Escape(UIKeyboardEventArgs e)
        {

            if (e.Key.ToLower() == "escape" && IsOpen == true)
            {
                Dropdown.Hide();
            }
        }

        protected void OnClickEvent()
        {
            if (IsOpen == null)
            {
                Dropdown.Toggle();
            }
        }
    }
}
