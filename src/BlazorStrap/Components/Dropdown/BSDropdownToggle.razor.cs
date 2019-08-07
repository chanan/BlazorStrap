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
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
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

        [Parameter] protected Color Color { get; set; } = Color.None;
        [Parameter] protected Size Size { get; set; } = Size.None;
        [Parameter] protected bool IsLink { get; set; }
        [Parameter] protected bool IsSplit { get; set; }
        [Parameter] protected bool? IsOpen { get; set; }
        [Parameter] protected EventCallback<UIMouseEventArgs> OnClick { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
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
