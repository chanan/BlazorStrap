using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSCollapseBase : BlazorStrapToggleBase<BSCollapseBase>, IAsyncDisposable
    {
        private Func<Task>? _callback;
        private DotNetObjectReference<BSCollapseBase>? _objectRef;
        [CascadingParameter] BSCollapseBase? Parent { get; set; }
        [CascadingParameter] BSAccordionItemBase? AccordionParent { get; set; }

        internal Action? NestedHandler { get; set; }

        private bool _lock;

        /// <summary>
        /// Disables animations when set.
        /// </summary>
        [Parameter] public bool NoAnimations { get; set; }

        /// <summary>
        /// Content of collapse.
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// Collapse content is shown by default.
        /// </summary>
        [Parameter]
        public bool DefaultShown
        {
            get => _defaultShown;
            set { _defaultShown = value; if (!_hasRendered) Shown = value; }
        }

        /// <summary>
        /// Adds the navbar-collapse class to the item.
        /// </summary>
        [Parameter] public bool IsInNavbar { get; set; }

        /// <summary>
        /// Sets if the collarpse should be rendered with <c>&lt;ul&gt;</c>
        /// </summary>
        [Parameter] public bool IsList { get; set; }

        /// <summary>
        /// Fragment used as the toggler.
        /// </summary>
        [Parameter] public RenderFragment? Toggler { get; set; }

        private bool _defaultShown;

        //Prevents the default state from overriding current state
        private bool _hasRendered;

        

        // Can be access by @ref
        public override bool Shown { get; protected set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        protected ElementReference? MyRef { get; set; }
        private async Task TryCallback(bool renderOnFail = true)
        {
            try
            {
                // Check if objectRef set if not callback will be handled after render.
                // If anything fails callback will will be handled after render.
                if (_objectRef != null)
                {
                    if (_callback != null)
                    {
                        await _callback();
                        _callback = null;
                    }
                }
                else
                {
                    throw new InvalidOperationException("No object ref");
                }
            }
            catch
            {
                if (renderOnFail)
                    await InvokeAsync(StateHasChanged);
            }
        }
        protected override bool ShouldRender()
        {
            return !_lock;
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
            NestedHandler?.Invoke();
            CanRefresh = false;
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);

            if (_lock) return;
            _lock = true;
            if (!NoAnimations)
                await BlazorStrapService.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, true);
            Shown = true;
            if (NoAnimations)
                await TransitionEndAsync();
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
            NestedHandler?.Invoke();
            CanRefresh = false;
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            if (_lock) return;
            _lock = true;
            if (!NoAnimations)
                await BlazorStrapService.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, false);
            Shown = false;
            if (NoAnimations)
                await TransitionEndAsync();
        }

        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _objectRef = DotNetObjectReference.Create(this);
                BlazorStrapService.OnEventForward += InteropEventCallback;
                if (Parent != null)
                    Parent.NestedHandler += NestedHandlerEvent;
                if (AccordionParent != null)
                    AccordionParent.NestedHandler += NestedHandlerEvent;

                if (IsInNavbar)
                {
                    await BlazorStrapService.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Resize);
                }
                _hasRendered = true;
            }
            if (_callback != null)
            {
                await _callback.Invoke();
                _callback = null;
            }
        }

        protected override void OnInitialized()
        {

        }

        private async void NestedHandlerEvent()
        {
            if (!_lock) return;
            await TransitionEndAsync();
        }
        private async Task TransitionEndAsync()
        {
            _lock = false;
            await InvokeAsync(StateHasChanged);

            if (OnShown.HasDelegate && Shown)
                _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });
            if (OnHidden.HasDelegate && !Shown)
                _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
            CanRefresh = true;
        }

        protected abstract Task OnResize(int width);

        public override async Task InteropEventCallback(string id, CallerName name, EventType type)
        {
            if (id.Contains(",") && name.Equals(typeof(ClickForward)) && type == EventType.Click)
            {
                var ids = id.Split(",");
                if (ids.Any(q => q == DataId))
                {
                    await ToggleAsync();
                }
            }
            else if (DataId == id && name.Equals(typeof(ClickForward)) && type == EventType.Click)
            {
                await ToggleAsync();
            }
        }

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (id == DataId && name.Equals(this) && type == EventType.Resize)
            {
                await OnResize(e?.ClientWidth ?? 0);
            }

            else if (DataId == id && name.Equals(this) && type == EventType.TransitionEnd)
            {
                await TransitionEndAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (IsInNavbar)
            {
                try
                {
                    await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Resize);
                }
                catch { }
            }
            if (Parent?.NestedHandler != null)
                Parent.NestedHandler -= NestedHandlerEvent;
            if (AccordionParent?.NestedHandler != null)
                AccordionParent.NestedHandler -= NestedHandlerEvent;

            BlazorStrapService.OnEventForward -= InteropEventCallback;
            _objectRef?.Dispose();
        }
    }
}
