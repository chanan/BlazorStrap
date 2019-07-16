using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSDropdownToggle : BootstrapComponentBase
    {
       protected string classname =>
     new CssBuilder()
         .AddClass("btn", !IsLink)
         .AddClass($"btn-{Size.ToDescriptionString()}", !IsLink && Size != Size.None)
         .AddClass($"btn-{Color.ToDescriptionString()}", !IsLink && Color != Color.None)
         .AddClass("dropdown-toggle-split", IsSplit)
         .AddClass("dropdown-toggle")
         .AddClass("nav-link", IsLink)
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
        [Parameter] protected EventCallback<UIMouseEventArgs> onclick { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal DropDownMenuControl DropDownMenuControl { get; set; }

        protected void Escape(UIKeyboardEventArgs e)
        {
            if (e.Key.ToLower() == "escape" && (IsOpen == true || DropDownMenuControl.Handler.IsOpen(DropDownMenuControl.Id)))
            {
                DropDownMenuControl.Handler.Toggle(DropDownMenuControl.Id);
            }
        }

        protected void onClickEvent(UIMouseEventArgs e)
        {
            Console.WriteLine("dropdown-toggle onClickEvent");
            if (onclick.HasDelegate)
            {
                onclick.InvokeAsync(e);
            }
            if (DropDownMenuControl.Handler != null && IsOpen == null)
            {
                DropDownMenuControl.Handler.Toggle(DropDownMenuControl.Id);
                StateHasChanged();
            }
        }
    }
}
