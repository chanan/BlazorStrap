using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSTooltip : BlazorStrapToggleBase<BSTooltip>, IAsyncDisposable
    {
        private Func<Task>? _callback;
        [Parameter] public Placement Placement { get; set; }
        [Parameter] public string? Target { get; set; }

        private string? ClassBuilder => new CssBuilder("tooltip")
            .AddClass($"bs-tooltip-{Placement.NameToLower().LeftRightToStartEnd()}")
            .AddClass($"show", Shown)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private bool HasRender { get; set; }

        private ElementReference? MyRef { get; set; }
        public bool Shown { get; private set; }
        private string Style { get; set; } = "display:none";
        private async Task TryCallback(bool renderOnFail = true)
        {
            try
            {
                // If anything fails callback will will be handled after render.
                if (_callback != null)
                {
                    await _callback();
                    _callback = null;
                }
            }
            catch
            {
                if (renderOnFail)
                    await InvokeAsync(StateHasChanged);
            }
        }
        public override Task HideAsync()
        {
            if (!Shown) return Task.CompletedTask;
            _callback = async () =>
            {
                await HideActionsAsync();
            };
            return TryCallback();
        }
        private async Task HideActionsAsync()
        { 
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            Shown = false;
            await BlazorStrap.Interop.SetStyleAsync(MyRef, "display", "name");
            await BlazorStrap.Interop.RemoveClassAsync(MyRef, "show");
            await BlazorStrap.Interop.RemovePopoverAsync(MyRef, DataId);

            if (OnHidden.HasDelegate)
                _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
        }

        public override Task ShowAsync()
        {
            if (Shown) return Task.CompletedTask;
            _callback = async () =>
            {
                await ShowActionsAsync();
            };
            return TryCallback();
        }
        private async Task ShowActionsAsync()
        { 
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            Shown = true;
            await BlazorStrap.Interop.SetStyleAsync(MyRef, "display", "block");

            if (Target != null)
                await BlazorStrap.Interop.AddPopoverAsync(MyRef, Placement, Target);
            await BlazorStrap.Interop.UpdatePopoverArrowAsync(MyRef, Placement, true);

            await BlazorStrap.Interop.AddClassAsync(MyRef, "show");
            if (OnShown.HasDelegate)
                _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });
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
                if (Target != null)
                {
                    await BlazorStrap.Interop.AddEventAsync(DotNetObjectReference.Create(this), Target,
                        EventType.Mouseenter);
                    await BlazorStrap.Interop.AddEventAsync(DotNetObjectReference.Create(this), Target,
                        EventType.Mouseleave);
                    EventsSet = true;
                }
            }
            if (_callback != null)
            {
                await _callback.Invoke();
                _callback = null;
            }
        }

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type,
            Dictionary<string, string>? classList = null, JavascriptEvent? e = null)
        {
            if (id == Target && name.Equals(this) && type == EventType.Mouseenter)
            {
                await ShowAsync();
            }
            else if (id == Target && name.Equals(this) && type == EventType.Mouseleave)
            {
                await HideAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            // Prerendering error suppression 
            try
            {
                if (HasRender)
                    await BlazorStrap.Interop.RemovePopoverAsync(MyRef, DataId);

                if (Target != null)
                {
                    if (EventsSet)
                    {
                        await BlazorStrap.Interop.RemoveEventAsync(this, Target, EventType.Mouseenter);
                        await BlazorStrap.Interop.RemoveEventAsync(this, Target, EventType.Mouseleave);
                    }
                }
            }
            catch { }

            GC.SuppressFinalize(this);
        }
    }
}