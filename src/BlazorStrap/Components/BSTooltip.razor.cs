using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BlazorStrap
{
    public abstract class BSTooltipBase : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] protected Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        //Didnt change this to use DynamicElement so that ref will still work

        protected ElementReference Tooltip { get; set; }
        protected ElementReference Arrow { get; set; }
        protected override void OnAfterRender(bool firstrun)
        {
            if (Target != null)
            {
                var placement = Placement.ToDescriptionString();
                new BlazorStrapInterop(JSRuntime).Tooltip(Target, Tooltip, Arrow, placement);
            }
        }

        [Parameter] public Placement Placement { get; set; } = Placement.Auto;
        [Parameter] public string Title { get; set; }
        [Parameter] public string Target { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
