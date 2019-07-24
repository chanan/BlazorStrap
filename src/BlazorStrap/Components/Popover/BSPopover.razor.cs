using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSPopover : ToggleableComponentBase
    {
        [Inject] Microsoft.JSInterop.IJSRuntime JSRuntime { get; set; }
        protected string classname =>
        new CssBuilder("popover")
            .AddClass($"bs-popover-{Placement.ToDescriptionString()}")
            .AddClass("show", _isOpen)
            .AddClass(Class)
        .Build();

        protected ElementRef arrow;

        protected override void OnAfterRender()
        {
            if (_isOpen)
            {
                var placement = Placement.ToDescriptionString();
                new BlazorStrapInterop(JSRuntime).Popper(Target, Id, arrow, placement);
            }
        }
        protected string Id => Target + "-popover";

        [Parameter] protected Placement Placement { get; set; } = Placement.Auto;
        [Parameter] protected string Target { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected string Style { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected void onclick()
        {
            Hide();
        }
    }
}
