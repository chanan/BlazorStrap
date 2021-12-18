using BlazorStrap.Util;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSTooltip : ComponentBase, IDisposable
    {
        [Inject] internal IJSRuntime JS { get; set; }
        private bool _hasRender { get; set; }
        internal DotNetObjectReference<BSTooltip> ObjRef { get; set; }
        internal bool Shown { get; set; }
        internal ElementReference MyRef { get; set; }
        internal string Style { get; set; } = "display:none";
        internal string Id { get; set; } = Guid.NewGuid().ToString();

        internal string? Class => new CssBuilder("tooltip")
            .AddClass($"bs-tooltip-{Placement.ToDescriptionString()}")
            .AddClass($"show", Shown)
            .Build().ToNullString();

        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] public BlazorStrapInterop BlazorStrapInterop { get; set; }
        [Inject] public IPopper Popper { get; set; }


        //Didnt change this to use DynamicElement so that ref will still work

        protected ElementReference Tooltip { get; set; }
        protected ElementReference Arrow { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Console.WriteLine("Render");
                _hasRender = true;
                ObjRef = DotNetObjectReference.Create(this);
                await JS.InvokeVoidAsync("blazorStrap.AddPopover", MyRef, ObjRef, Placement.ToDescriptionString(), true, true, Target).ConfigureAwait(false);                
            }
        }

        [Parameter] public Placement Placement { get; set; } = Placement.Auto;
        [Parameter] public string Title { get; set; }
        [Parameter] public string Target { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        [JSInvokable]
        public async Task JSToggle()
        {
            if (!Shown)
            {
                await JS.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "block").ConfigureAwait(false);
                await JS.InvokeVoidAsync("blazorStrap.UpdatePopover", MyRef, ObjRef, Placement.ToDescriptionString(), true).ConfigureAwait(false);
                await JS.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show").ConfigureAwait(false);
            }
            else
            {
                await JS.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "none").ConfigureAwait(false);
                await JS.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show").ConfigureAwait(false);
            }
            Shown = !Shown;
        }

        [JSInvokable]
        public async Task SetStyle(string? style)
        {
            //Style = style;
        }
        public void Dispose()
        {
            if (_hasRender == true)
                try
                {
                    _ = JS.InvokeVoidAsync("blazorStrap.RemovePopover", MyRef, Target);
                }
                catch (Exception ex) when (ex.GetType().Name == "JSDisconnectedException") { }
        }
    }
}
