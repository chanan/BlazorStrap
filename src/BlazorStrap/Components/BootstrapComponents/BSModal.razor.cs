using BlazorComponentUtilities;
using BlazorStrap.InternalComponents;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSModal : BlazorStrapToggleBase<BSModal>, IAsyncDisposable
    {
        [Parameter] public bool AllowScroll { get; set; }
        [Parameter] public string? ButtonClass { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public string? ContentClass { get; set; }
        [Parameter] public RenderFragment<BSModal>? Footer { get; set; }
        [Parameter] public Size FullScreenSize { get; set; } = Size.None;
        [Parameter] public RenderFragment? Header { get; set; }
        [Parameter] public string? HeaderClass { get; set; }
        [Parameter] public bool IsCentered { get; set; }
        [Parameter] public bool IsFullScreen { get; set; }
        [Parameter] public bool IsScrollable { get; set; }
        [Parameter] public bool IsStaticBackdrop { get; set; }
        [Parameter] public bool ShowBackdrop { get; set; } = true;
        [Parameter] public Size Size { get; set; } = Size.None;
        private bool _leaveBodyAlone;

        private bool _shown;

        private Backdrop? BackdropRef { get; set; }

        private string? ClassBuilder => new CssBuilder("modal")
            .AddClass("fade")
            .AddClass("show", Shown)
            .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass("modal-fullscreen", IsFullScreen && FullScreenSize == Size.None)
            .AddClass($"modal-fullscreen-{FullScreenSize.ToDescriptionString()}-down", FullScreenSize != Size.None)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string? ContentClassBuilder => new CssBuilder("modal-body")
            .AddClass(ContentClass)
            .Build().ToNullString();

        private string? DialogClass => new CssBuilder("modal-dialog")
            .AddClass("modal-dialog-scrollable", IsScrollable)
            .AddClass("modal-dialog-centered", IsCentered)
            .AddClass((IsScrollable ? "modal-dialog-scrollable" : string.Empty))
            .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass("modal-dialog-centered", IsCentered)
            .Build().ToNullString();

        private string? HeaderClassBuilder => new CssBuilder("modal-header")
            .AddClass(HeaderClass)
            .Build().ToNullString();

        private ElementReference MyRef { get; set; }

        private bool Shown
        {
            get => _shown;
            set => _shown = value;
        }

        private string Style { get; set; } = "display: none;";

        public override async Task HideAsync()
        {
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);

            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "document", "keyup", true);
            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "document", "click", true);
            Shown = false;
            if (!EventsSet)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataId, "bsModal", "transitionend");
                EventsSet = true;
            }

            JSCallback.EventCallback("", "ModalorOffcanvas", "toggled");

            if (!_leaveBodyAlone)
            {
                await Js.InvokeVoidAsync("blazorStrap.RemoveBodyClass", "modal-open");
                await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "overflow", "");
                await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "paddingRight", "");
            }

            await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "show", 50);
            if (BackdropRef != null)
                await BackdropRef.ToggleAsync();

            if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", MyRef))
            {
                await TransitionEndAsync();
            }

            _leaveBodyAlone = false;
        }

        public override async Task ShowAsync()
        {
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataId, "document", "keyup", true);
            await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataId, "document", "click", true);
            Shown = true;
            if (!EventsSet)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataId, "bsModal", "transitionend");
                EventsSet = true;
            }

            JSCallback.EventCallback("", "ModalorOffcanvas", "toggled");

            await Js.InvokeVoidAsync("blazorStrap.AddBodyClass", "modal-open");

            if (!AllowScroll)
            {
                var scrollWidth = await Js.InvokeAsync<int>("blazorStrap.GetScrollBarWidth");
                var viewportHeight = await Js.InvokeAsync<int>("blazorStrap.GetInnerHeight");
                var peakHeight = await Js.InvokeAsync<int>("blazorStrap.PeakHeight", MyRef);

                if (viewportHeight > peakHeight)
                {
                    await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "overflow", "hidden");
                    if (scrollWidth != 0)
                        await Js.InvokeVoidAsync("blazorStrap.SetBodyStyle", "paddingRight", $"{scrollWidth}px");
                }
            }


            BlazorStrapService.ModalChanged(this);

            if (BackdropRef != null)
                await BackdropRef.ToggleAsync();
            await Js.InvokeVoidAsync("blazorStrap.SetStyle", MyRef, "display", "block", 50);
            await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "show");

            if (await Js.InvokeAsync<bool>("blazorStrap.TransitionDidNotStart", MyRef))
            {
                await TransitionEndAsync();
            }
        }

        private void Toggle()
        {
            EventUtil.AsNonRenderingEventHandler(ToggleAsync).Invoke();
        }

        protected override void OnInitialized()
        {
            JSCallback.EventHandler += OnEventHandler;
            BlazorStrapService.ModalChange += OnModalChange;
        }

        private async Task BackdropClicked()
        {
            if (IsStaticBackdrop)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "modal-static");
                await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "modal-static", 250);
                return;
            }

            await ToggleAsync();
        }


        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        private async Task TransitionEndAsync()
        {
            Style = Shown ? "display: block;" : "display: none;";
            await InvokeAsync(StateHasChanged);
            if (Shown)
            {
                if (OnShown.HasDelegate)
                    await OnShown.InvokeAsync(this);
            }
            else
            {
                if (OnHidden.HasDelegate)
                    await OnHidden.InvokeAsync(this);
            }
        }

        private void ClickEvent()
        {
            Toggle();
        }

        private async void OnEventHandler(string id, string name, string type, Dictionary<string, string>? classList,
            JavascriptEvent? e)
        {
            if (DataId == id && name == "clickforward" && type == "click")
            {
                await ToggleAsync();
            }
            else if (DataId == id && name == "bsModal" && type == "transitionend")
            {
                await TransitionEndAsync();
            }
            else if (DataId == id && name == "document" && type == "keyup" && e?.Key == "Escape")
            {
                if (IsStaticBackdrop)
                {
                    await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "modal-static", 250);
                    await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "modal-static");
                    return;
                }

                await HideAsync();
            }
            else if (DataId == id && name == "document" && type == "click" &&
                     e?.Target.ClassList.Any(q => q.Value == "modal") == true)
            {
                if (IsStaticBackdrop)
                {
                    await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "modal-static", 250);
                    await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "modal-static");
                    return;
                }

                await HideAsync();
            }
        }

        private async void OnModalChange(BSModal? model, bool fromJs)
        {
            if (fromJs)
            {
                if (IsStaticBackdrop)
                {
                    await Js.InvokeVoidAsync("blazorStrap.AddClass", MyRef, "modal-static", 250);
                    await Js.InvokeVoidAsync("blazorStrap.RemoveClass", MyRef, "modal-static");
                    return;
                }

                if (_shown)
                    await HideAsync();
                return;
            }

            if (model == this || !_shown) return;
            _leaveBodyAlone = true;
            if (_shown)
                await HideAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "document", "click", true);
            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "document", "keyup", true);
            if (EventsSet)
                await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataId, "bsModal", "transitionend");
            JSCallback.EventHandler -= OnEventHandler;
            BlazorStrapService.ModalChange -= OnModalChange;
            GC.SuppressFinalize(this);
        }
    }
}