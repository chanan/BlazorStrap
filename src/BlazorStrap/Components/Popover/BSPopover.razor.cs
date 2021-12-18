using BlazorComponentUtilities;
using BlazorStrap.Util;
using BlazorStrap.Util.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public partial class BSPopover : ToggleableComponentBase, IDisposable
    {
        internal string Id { get; set; } = Guid.NewGuid().ToString();
        [Inject] internal IJSRuntime JS { get; set; }
        private bool _hasRender { get; set; }
        internal DotNetObjectReference<BSPopover> ObjRef { get; set; }
        internal bool Shown { get; set; }
        internal ElementReference MyRef { get; set; }
        internal string Style { get; set; } = "display:none";
        [Parameter(CaptureUnmatchedValues = true)] public IDictionary<string, object> UnknownParameters { get; set; }
        [Inject] public BlazorStrapInterop BlazorStrapInterop { get; set; }
        [Inject] public IPopper Popper { get; set; }

        protected string Classname =>
        new CssBuilder("popover")
            .AddClass($"bs-popover-{Placement.ToDescriptionString()}")
            .AddClass($"show", Shown)
            .AddClass(Class)
        .Build();

        protected ElementReference Arrow { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _hasRender = true;
                ObjRef = DotNetObjectReference.Create(this);
                await JS.InvokeVoidAsync("blazorStrap.AddPopover", MyRef, ObjRef, Placement.ToDescriptionString(), MouseOver, false, Target).ConfigureAwait(false);
            }
        }
        protected string ElId => Target + "-popover";

        [Parameter] public Placement Placement { get; set; } = Placement.Auto;
        [Parameter] public string Target { get; set; }
        [Parameter] public string Class { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool MouseOver { get; set; }

        public override void Show()
        {
            _ = ShowAsync();
        }
        public override void Hide()
        {
            _ = HideAsync();
        }
        public override void Toggle()
        {
            _ = JSToggle();
        }
        internal async override Task Changed(bool e)
        {
            e = !e;
            if (!e)
            {
                Style = "display:block";
                await JS.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "block").ConfigureAwait(false);
                await JS.InvokeVoidAsync("blazorStrap.UpdatePopover", MyRef, ObjRef, Placement.ToDescriptionString(), true).ConfigureAwait(false);
                await JS.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show").ConfigureAwait(false);
            }
            else
            {
                Style = "display:none";
                await JS.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "none").ConfigureAwait(false);
                await JS.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show").ConfigureAwait(false);
            }
            Shown = e;
        }
        public async Task ShowAsync()
        {
            Shown = true;
            await JS.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show").ConfigureAwait(false);
        }
        public async Task HideAsync()
        {
            Shown = false;
            await JS.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "none").ConfigureAwait(false);
            await JS.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show").ConfigureAwait(false);
        }
        [JSInvokable]
        public async Task JSToggle()
        {
            if (!Shown)
            {
                await JS.InvokeVoidAsync("blazorStrap.UpdatePopover", MyRef, ObjRef, Placement.ToDescriptionString(), true).ConfigureAwait(false);
                await JS.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show").ConfigureAwait(false);
            }
            else
            {
                await JS.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show").ConfigureAwait(false);
            }
            Shown = !Shown;
            base.IsOpen = Shown;
        }
        [JSInvokable]
        public async Task SetStyle(string? style)
        {
            Style = style;
        }
        protected void OnClick()
        {
            Hide();
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
