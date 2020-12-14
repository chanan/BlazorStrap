using BlazorStrap.Util;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSTooltip : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] public BlazorStrapInterop BlazorStrapInterop { get; set; }
        [Inject] public IPopper Popper { get; set; }

        //Didnt change this to use DynamicElement so that ref will still work

        protected ElementReference Tooltip { get; set; }
        protected ElementReference Arrow { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstrun)
        {
            if(firstrun)
            {
                await Popper.SetPopper();
            }
            if (Target != null)
            {
                var placement = Placement.ToDescriptionString();
                await BlazorStrapInterop.Tooltip(Target, Tooltip, Arrow, placement);
            }
        }

        [Parameter] public Placement Placement { get; set; } = Placement.Auto;
        [Parameter] public string Title { get; set; }
        [Parameter] public string Target { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
    }
}
