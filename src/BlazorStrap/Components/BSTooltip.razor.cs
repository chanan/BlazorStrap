using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Collections.Generic;

namespace BlazorStrap
{
    public class CodeBSTooltip : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] protected IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        //Didnt change this to use DynamicElement so that ref will still work

        protected ElementRef tooltip;
        protected ElementRef arrow;
        protected override void OnAfterRender()
        {
            if (Target != null)
            {
                var placement = Placement.ToDescriptionString();
                new BlazorStrapInterop(JSRuntime).Tooltip(Target, tooltip, arrow, placement);
            }
        }

        [Parameter] protected Placement Placement { get; set; } = Placement.Auto;
        [Parameter] protected string Title { get; set; }
        [Parameter] protected string Target { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
    }
}
