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
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        //Didnt change this to use DynamicElement so that ref will still work

        protected ElementReference tooltip;
        protected ElementReference arrow;
        protected override void OnAfterRender()
        {
            if (Target != null)
            {
                var placement = Placement.ToDescriptionString();
                new BlazorStrapInterop(JSRuntime).Tooltip(Target, tooltip, arrow, placement);
            }
        }

        [Parameter] public Placement Placement { get; set; } = Placement.Auto;
        [Parameter] public string Title { get; set; }
        [Parameter] public string Target { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
