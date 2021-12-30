using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSOffCanvas : BlazorStrapToggleBase<BSOffCanvas>, IAsyncDisposable
    {
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

        private bool Shown
        {
            get => _shown;
            set => _shown = value;
        }

        public override async Task ShowAsync()
        {
            _lock = true;
            if (!EventsSet)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataId, "bsOffCanvas", "transitionend");
                EventsSet = true;
            }
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            JSCallback.EventCallback("", "ModalorOffcanvas", "toggled");
            if (ShowBackdrop)
            {
                await Js.InvokeVoidAsync("blazorStrap.SetStyle", BackdropRef, "display", "block", 100);
                await Js.InvokeVoidAsync("blazorStrap.AddClass", BackdropRef, "show");
                BackdropStyle = "display: block;";
            
            }
            await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show");
            if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", MyRef))
            {
                await TransitionEndAsync();
            }
            Shown = true;
            await DoAnimationsAsync(_shown);
        }

        public override async Task HideAsync()
        {
            _lock = true;
            if (!EventsSet)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataId, "bsOffCanvas", "transitionend");
                EventsSet = true;
            }
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            JSCallback.EventCallback("", "ModalorOffcanvas", "toggled");
            if (ShowBackdrop)
            {
                await Js.InvokeVoidAsync("blazorStrap.RemoveClass", BackdropRef, "show", 100);
                BackdropStyle = "display: none;";
                
            }
            await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show");
            if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", MyRef))
            {
                await TransitionEndAsync();
            }
            Shown = false;
            await DoAnimationsAsync(_shown);
        }

        protected override void OnInitialized()
        {
            JSCallback.EventHandler += OnEventHandler;
        }
        private async void OnEventHandler(string id, string name, string type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (DataId == id && name == "bsOffCanvas" && type == "transitionend")
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
                await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "bsOffCanvas", "transitionend");
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
                        var scrollWidth = await Js.InvokeAsync<int>("blazorStrap.GetScrollBarWidth");
                        await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "overflow", "hidden");
                        await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "paddingRight", $"{scrollWidth}px");
                    }
                }
            }
            else
            {
                {
                    await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "overflow", "");
                    await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "paddingRight", "");
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!EventsSet)
            {
                await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "bsoffCanvas", "transitionend");
            }
            JSCallback.EventHandler -= OnEventHandler;
        }
    }
}