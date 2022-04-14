using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSOffCanvas : BlazorStrapToggleBase<BSOffCanvas>, IAsyncDisposable
    {
        private DotNetObjectReference<BSOffCanvas> _objectRef;
        [Parameter] public bool AllowScroll { get; set; }
        [Parameter] public string? BodyClass { get; set; }
        [Parameter] public string? ButtonClass { get; set; }
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public bool DisableBackdropClick { get; set; }
        [Parameter] public RenderFragment? Header { get; set; }
        [Parameter] public string? HeaderClass { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public Placement Placement { get; set; } = Placement.Left;
        [Parameter] public bool ShowBackdrop { get; set; } = true;

        private bool _lock;
        private bool _shown;

        private string? BackdropClass => new CssBuilder("offcanvas-backdrop fade")
            .AddClass("show", Shown)
            .Build().ToNullString();

        private ElementReference BackdropRef { get; set; }
        private string BackdropStyle { get; set; } = "display: none;";

        private string? BodyClassBuilder => new CssBuilder("offcanvas-body")
            .AddClass(BodyClass)
            .Build().ToNullString();

        private string? ClassBuilder => new CssBuilder("offcanvas")
            .AddClass($"offcanvas-{Placement.NameToLower().LeftRightToStartEnd()}")
            .AddClass("show", Shown)
            .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string? HeaderClassBuilder => new CssBuilder("offcanvas-header")
            .AddClass(HeaderClass)
            .Build().ToNullString();

        private ElementReference MyRef { get; set; }
        protected override bool ShouldRender()
        {
            return !_lock;
        }

        public bool Shown
        {
            get => _shown;
            private set => _shown = value;
        }

        public override async Task ShowAsync()
        {
            if (Shown) return;

            // Used to hide popovers
            BlazorStrap.ForwardToggle("", this);
            _lock = true;
            if (!EventsSet)
            {
                await BlazorStrap.Interop.AddEventAsync(_objectRef, DataId, EventType.TransitionEnd);
                EventsSet = true;
            }
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            BlazorStrap.ForwardToggle(DataId, this);
            if (ShowBackdrop)
            {
                await BlazorStrap.Interop.SetStyleAsync(BackdropRef, "display", "block", 100);
                await BlazorStrap.Interop.AddClassAsync(BackdropRef, "show");
                BackdropStyle = "display: block;";
            
            }
            await BlazorStrap.Interop.AddClassAsync(MyRef, "show");
            if(await BlazorStrap.Interop.TransitionDidNotStartAsync(MyRef))
            {
                await TransitionEndAsync();
            }
            Shown = true;
            await DoAnimationsAsync(_shown);
        }

        public override async Task HideAsync()
        {
            if (!Shown) return;

            // Used to hide popovers
            BlazorStrap.ForwardToggle("", this);
            _lock = true;
            if (!EventsSet)
            {
                await BlazorStrap.Interop.AddEventAsync(_objectRef, DataId, EventType.TransitionEnd);
                EventsSet = true;
            }
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            
            if (ShowBackdrop)
                await BlazorStrap.Interop.RemoveClassAsync(BackdropRef, "show", 100);{
                BackdropStyle = "display: none;";
            }

            await BlazorStrap.Interop.RemoveClassAsync(MyRef, "show");
            if(await BlazorStrap.Interop.TransitionDidNotStartAsync(MyRef))
            {
                await TransitionEndAsync();
            }
            Shown = false;
            await DoAnimationsAsync(_shown);
        }

        protected override void OnInitialized()
        {
            BlazorStrap.OnEventForward += InteropEventCallback;
            _objectRef = DotNetObjectReference.Create<BSOffCanvas>(this);
        }

        public override async Task InteropEventCallback(string id, CallerName name, EventType type)
        {
            if (id == DataId && name.Equals(typeof(ClickForward)) && type == EventType.Click)
            {
                await ToggleAsync();
            }
        }

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (DataId == id && name.Equals(this) && type == EventType.TransitionEnd)
            {
                await TransitionEndAsync();
            }
        }

        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        private async Task BackdropClicked()
        {
            if (DisableBackdropClick) return;
            await ToggleAsync();
        }

        private async Task ClickEvent()
        {
            if (!OnClick.HasDelegate)
                await ToggleAsync();
            await OnClick.InvokeAsync();
        }
        
        private async Task TransitionEndAsync()
        {
            _lock = false;
            await InvokeAsync(StateHasChanged);

            if (EventsSet)
            {
                await BlazorStrap.Interop.RemoveEventAsync(this, DataId, EventType.TransitionEnd);
                EventsSet = false;
            }
            _lock = false;
            await InvokeAsync(StateHasChanged);
            if (Shown)
            {
                if (OnShown.HasDelegate)
                    _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });
            }
            else
            {
                if (OnHidden.HasDelegate)
                    _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
            }
        }
        private async Task DoAnimationsAsync(bool value)
        {
            if (value)
            {
                if (ShowBackdrop)
                {
                    if (!AllowScroll)
                    {
                        var scrollWidth = await BlazorStrap.Interop.GetScrollBarWidth();
                        await BlazorStrap.Interop.SetBodyStyleAsync("overflow", "hidden");
                        await BlazorStrap.Interop.SetBodyStyleAsync("paddingRight",  $"{scrollWidth}px");
                    }
                }
            }
            else
            {
                {
                    await BlazorStrap.Interop.SetBodyStyleAsync("overflow", "");
                    await BlazorStrap.Interop.SetBodyStyleAsync("paddingRight",  "");
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            BlazorStrap.OnEventForward -= InteropEventCallback;
            if (!EventsSet)
            {
                await BlazorStrap.Interop.RemoveEventAsync(this, DataId, EventType.TransitionEnd);
            }
        }
    }
}