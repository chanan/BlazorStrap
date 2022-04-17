using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSCollapse : BlazorStrapToggleBase<BSCollapse>, IAsyncDisposable
    {
        private DotNetObjectReference<BSCollapse> _objectRef;
        private bool _lock;
        [Parameter] public bool NoAnimations { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }

        [Parameter]
        public bool DefaultShown
        {
            get => _defaultShown;
            set { _defaultShown = value; if (!_hasRendered) Shown = value; }
        }

        [Parameter] public bool IsInNavbar { get; set; }
        [Parameter] public bool IsList { get; set; }
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

        private ElementReference MyRef { get; set; }
        protected override bool ShouldRender()
        {
            return !_lock;
        }

        public override async Task ShowAsync()
        {
            if (Shown) return;
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
        public override async Task HideAsync()
        {
            if (!Shown) return;
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
                if (IsInNavbar)
                {
                    await BlazorStrap.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Resize);
                }
                _hasRendered = true;
            }
        }

        protected override void OnInitialized()
        {
            _objectRef = DotNetObjectReference.Create(this);
            BlazorStrap.OnEventForward += InteropEventCallback;
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
                await BlazorStrap.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Resize);
            BlazorStrap.OnEventForward -= InteropEventCallback;
            _objectRef.Dispose();
        }
    }
}
