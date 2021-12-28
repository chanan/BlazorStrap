using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSTooltip : BlazorToggleStrapBase<BSTooltip>, IAsyncDisposable
    {
        [Parameter] public Placement Placement { get; set; }
        [Parameter] public string? Target { get; set; }
        private DotNetObjectReference<BSTooltip>? _objRef;

        private string? ClassBuilder => new CssBuilder("tooltip")
            .AddClass($"bs-tooltip-{Placement.NameToLower().LeftRightToStartEnd()}")
            .AddClass($"show", Shown)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private bool HasRender { get; set; }

        private ElementReference MyRef { get; set; }
        private bool Shown { get; set; }
        private string Style { get; set; } = "display:none";

        public override async Task HideAsync()
        {
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            Shown = false;
            await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "none");
            await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show");
            await Js.InvokeVoidAsync("blazorStrap.RemovePopover", MyRef, DataId);
            
            if (OnHidden.HasDelegate)
                await OnHidden.InvokeAsync(this);
        }

        public override async Task ShowAsync()
        {
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            Shown = true;
            await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "block");
            
            if (_objRef != null)
            {
                if (Target != null)
                    await Js.InvokeVoidAsync("blazorStrap.AddPopover", MyRef, _objRef,
                        Placement.NameToLower().ToDashSeperated(), Target);
                await Js.InvokeVoidAsync("blazorStrap.UpdatePopoverArrow", MyRef, _objRef, Placement.NameToLower().PurgeStartEnd(),
                    true);
            }

            await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show");
            if (OnShown.HasDelegate)
                await OnShown.InvokeAsync(this);
        }

        public override Task ToggleAsync()
        {
            return !Shown ? ShowAsync() : HideAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                HasRender = true;
                _objRef = DotNetObjectReference.Create(this);
                if (Target != null)
                {
                    
                    await Js.InvokeVoidAsync("blazorStrap.AddEvent", Target, "bsTooltip", "mouseenter");
                    await Js.InvokeVoidAsync("blazorStrap.AddEvent", Target, "bsTooltip", "mouseleave");
                    EventsSet = true;
                }
            }
        }

        protected override void OnInitialized()
        {
            JSCallback.EventHandler += OnEventHandler;
        }

        private async void OnEventHandler(string id, string name, string type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (id == Target && name == "bsTooltip" && type == "mouseenter")
            {
                await ShowAsync();
            }
            else if (id == Target && name == "bsTooltip" && type == "mouseleave")
            {
                await HideAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            JSCallback.EventHandler -= OnEventHandler;


            // Prerendering error suppression 
            if (HasRender)
                try
                {
                    await Js.InvokeVoidAsync("blazorStrap.RemovePopover", MyRef, DataId);
                    if (Target != null)
                    {
                        if (EventsSet)
                        {
                            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", Target, "bsTooltip", "mouseenter");
                            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", Target, "bsTooltip", "mouseleave");
                            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "null", "null");
                        }
                    }
                }
                catch (Exception ex) when (ex.GetType().Name == "JSDisconnectedException")
                {
                }

            GC.SuppressFinalize(this);
        }
    }
}