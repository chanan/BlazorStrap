using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSTooltipBase : BlazorStrapToggleBase<BSTooltipBase>, IAsyncDisposable
    {
        private Func<Task>? _callback;

        /// <summary>
        /// Tooltip placement.
        /// </summary>
        [Parameter] public Placement Placement { get; set; }

        /// <summary>
        /// DataID of target.
        /// </summary>
        [Parameter] public string? Target { get; set; }

        /// <summary>
        /// Setting this to false will hide the content of the tooltip when it is hidden.
        /// </summary>
        [Parameter] public bool ContentAlwaysRendered { get; set; } = true;

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        private bool HasRender { get; set; }

        protected ElementReference? MyRef { get; set; }
        public override bool Shown { get; protected set; }
        protected string Style { get; set; } = "display:none";
        protected bool ShouldRenderContent { get; set; } = true;
        protected override void OnInitialized()
        {
            ShouldRenderContent = ContentAlwaysRendered;
        }
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

        /// <inheritdoc/>
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
            await BlazorStrapService.Interop.SetStyleAsync(MyRef, "display", "none");
            await BlazorStrapService.Interop.RemoveClassAsync(MyRef, "show");
            await BlazorStrapService.Interop.RemovePopoverAsync(MyRef, DataId);

            if (OnHidden.HasDelegate)
                _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
            if (!ContentAlwaysRendered)
            {
                ShouldRenderContent = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        /// <inheritdoc/>
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
            if (!ContentAlwaysRendered)
            {
                ShouldRenderContent = true;
                await InvokeAsync(StateHasChanged);
            }
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            Shown = true;
            await BlazorStrapService.Interop.SetStyleAsync(MyRef, "display", "block");

            if (Target != null)
                await BlazorStrapService.Interop.AddPopoverAsync(MyRef, Placement, Target);
            await BlazorStrapService.Interop.UpdatePopoverArrowAsync(MyRef, Placement, true);

            await BlazorStrapService.Interop.AddClassAsync(MyRef, "show");
            if (OnShown.HasDelegate)
                _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });
        }

        /// <inheritdoc/>
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
                    await BlazorStrapService.Interop.AddEventAsync(DotNetObjectReference.Create(this), Target,
                        EventType.Mouseenter);
                    await BlazorStrapService.Interop.AddEventAsync(DotNetObjectReference.Create(this), Target,
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
                    await BlazorStrapService.Interop.RemovePopoverAsync(MyRef, DataId);

                if (Target != null)
                {
                    if (EventsSet)
                    {
                        await BlazorStrapService.Interop.RemoveEventAsync(this, Target, EventType.Mouseenter);
                        await BlazorStrapService.Interop.RemoveEventAsync(this, Target, EventType.Mouseleave);
                    }
                }
            }
            catch { }

            GC.SuppressFinalize(this);
        }
    }
}
