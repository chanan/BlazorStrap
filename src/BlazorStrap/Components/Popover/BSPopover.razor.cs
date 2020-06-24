using BlazorComponentUtilities;
using BlazorStrap.Util;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public abstract class BSPopoverBase : ToggleableComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] public BlazorStrapInterop BlazorStrapInterop { get; set; }
        [Inject] public IPopper Popper { get; set; }

        protected string Classname =>
        new CssBuilder("popover")
            .AddClass($"bs-popover-{Placement.ToDescriptionString()}")
            .AddClass("show", (IsOpen ?? false))
            .AddClass(Class)
        .Build();

        protected ElementReference Arrow { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstrun)
        {
            if (firstrun)
            {
                await Popper.SetPopper();
            }
            if (IsOpen ?? false)
            {
                var placement = Placement.ToDescriptionString();
                await BlazorStrapInterop.Popper(Target, Id, Arrow, placement);
            }
        }
        protected string Id => Target + "-popover";

        [Parameter] public Placement Placement { get; set; } = Placement.Auto;
        [Parameter] public string Target { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public string Style { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected void OnClick()
        {
            Hide();
        }
    }
}
