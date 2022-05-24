using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSCollapse : BlazorStrapToggleBase<BSCollapse>, IAsyncDisposable
    {
        private Func<Task>? _callback;
        private DotNetObjectReference<BSCollapse>? _objectRef;
        [CascadingParameter] BSCollapse? Parent { get; set; }
        [CascadingParameter] BSAccordionItem? AccordionParent { get; set; }

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

        [CascadingParameter] public BSNavbar? NavbarParent { get; set; }

        // Can be access by @ref
        public bool Shown { get; private set; }

        private string? ClassBuilder => new CssBuilder("collapse")
            .AddClass("show", Shown)
            .AddClass("navbar-collapse", IsInNavbar)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private ElementReference? MyRef { get; set; }
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
                await BlazorStrap.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, true);
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
                await BlazorStrap.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, false);
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
                BlazorStrap.OnEventForward += InteropEventCallback;
                if (Parent != null)
                    Parent.NestedHandler += NestedHandlerEvent;
                if (AccordionParent != null)
                    AccordionParent.NestedHandler += NestedHandlerEvent;

                if (IsInNavbar)
                {
                    await BlazorStrap.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Resize);
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

        private async Task OnResize(int width)
        {
            if (!IsInNavbar) return;
            if (width > 576 && NavbarParent?.Expand == Size.ExtraSmall ||
                width > 576 && NavbarParent?.Expand == Size.Small ||
                width > 768 && NavbarParent?.Expand == Size.Medium ||
                width > 992 && NavbarParent?.Expand == Size.Large ||
                width > 1200 && NavbarParent?.Expand == Size.ExtraLarge ||
                width > 1400 && NavbarParent?.Expand == Size.ExtraExtraLarge)
            {
                Shown = false;
                if (OnHidden.HasDelegate && !Shown)
                    _ = OnHidden.InvokeAsync();
                await InvokeAsync(StateHasChanged);
            }
        }

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
                    await BlazorStrap.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Resize);
                }
                catch { }
            }
            if (Parent?.NestedHandler != null)
                Parent.NestedHandler -= NestedHandlerEvent;
            if (AccordionParent?.NestedHandler != null)
                AccordionParent.NestedHandler -= NestedHandlerEvent;

            BlazorStrap.OnEventForward -= InteropEventCallback;
            _objectRef?.Dispose();
        }
    }
}
