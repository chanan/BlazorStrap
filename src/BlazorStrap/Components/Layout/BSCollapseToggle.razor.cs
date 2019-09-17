using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Timers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Routing;

namespace BlazorStrap
{
    public abstract class BSCollapseToggleBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        protected string classname =>
         new CssBuilder()
             .AddClass("btn", !IsLink)
             .AddClass($"btn-{Size.ToDescriptionString()}", !IsLink && Size != Size.None)
             .AddClass($"btn-{Color.ToDescriptionString()}", !IsLink && Color != Color.None)
             .AddClass(HiddenClass ?? "" , CollapseItem?.Collapse?.IsOpen ?? false && HiddenClass != null)
             .AddClass(ShownClass ?? "", CollapseItem?.Collapse?.IsOpen ?? false && ShownClass != null)
             .AddClass("active", CollapseItem?.Active ?? false)
             .AddClass(Class)
         .Build();
        
        protected string Tag => IsLink ? "a" : "button";
        protected string Type => IsLink ? null : "button";
        protected string href => IsLink ? "javascript:void(0)" : null;

        [Parameter] public Color Color { get; set; } = Color.None;
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public bool IsLink { get; set; }
        [Parameter] public string HiddenClass { get; set; }
        [Parameter] public string ShownClass { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [CascadingParameter] internal BSCollapseItem CollapseItem { get; set; }
        [CascadingParameter] internal BSCollapseGroup CollapseGroup { get; set; }

        protected void OnClickEvent()
        {
            if(CollapseGroup != null)
            {
                if(CollapseGroup.Selected != CollapseItem)
                {
                    CollapseGroup.Selected = CollapseItem;
                }
                else
                {
                    CollapseGroup.Selected.Collapse.Toggle();
                }
                return;
            }
            CollapseItem.Collapse.Toggle();
        }

       
    }
}
