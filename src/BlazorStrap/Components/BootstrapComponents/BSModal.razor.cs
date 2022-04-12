using BlazorComponentUtilities;
using BlazorStrap.InternalComponents;
using BlazorStrap.Service;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSModal : BlazorStrapToggleBase<BSModal>, IAsyncDisposable
    {
        private DotNetObjectReference<BSModal> _objectRef;
        private bool _lock;
        private bool _called;
        [Parameter] public BSColor ModalColor { get; set; } = BSColor.Default;
        [Parameter] public bool AllowScroll { get; set; }
        [Parameter] public string? ButtonClass { get; set; }
        [Parameter] public string? DialogClass { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public string? ContentClass { get; set; }
        [Parameter] public RenderFragment<BSModal>? Footer { get; set; }
        [Parameter] public string? FooterClass { get; set; }
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

        protected override bool ShouldRender()
        {
            return !_lock;
        }

        private string? ClassBuilder => new CssBuilder("modal")
            .AddClass("fade")
            .AddClass("show", Shown)
            //     .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string? BodyClassBuilder => new CssBuilder("modal-body")
            .AddClass(ContentClass)
            .Build().ToNullString();

        private string? ContentClassBuilder => new CssBuilder("modal-content")
            .AddClass($"bg-{ModalColor.NameToLower()}", ModalColor != BSColor.Default)
            .Build().ToNullString();

        private string? DialogClassBuilder => new CssBuilder("modal-dialog")
            .AddClass("modal-fullscreen", IsFullScreen && FullScreenSize == Size.None)
            .AddClass($"modal-fullscreen-{FullScreenSize.ToDescriptionString()}-down", FullScreenSize != Size.None)
            .AddClass("modal-dialog-scrollable", IsScrollable)
            .AddClass("modal-dialog-centered", IsCentered)
            .AddClass((IsScrollable ? "modal-dialog-scrollable" : string.Empty))
            .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass("modal-dialog-centered", IsCentered)
            .AddClass(DialogClass)
            .Build().ToNullString();

        private string? HeaderClassBuilder => new CssBuilder("modal-header")
            .AddClass(HeaderClass)
            .Build().ToNullString();

        private string? FooterClassBuilder => new CssBuilder("modal-footer")
            .AddClass(FooterClass)
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
            _called = true;
            _lock = true;
            Shown = false;
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            await BlazorStrap.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Keyup);
            await BlazorStrap.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Click);

            if (!EventsSet)
            {
                await BlazorStrap.Interop.AddEventAsync(_objectRef, DataId, EventType.TransitionEnd);
                EventsSet = true;
            }

            // Used to hide popovers
            BlazorStrap.ForwardToggle("", this);

            if (!_leaveBodyAlone)
            {
                await BlazorStrap.Interop.RemoveBodyClassAsync("modal-open");
                await BlazorStrap.Interop.SetBodyStyleAsync("overflow", "");
                await BlazorStrap.Interop.SetBodyStyleAsync("paddingRight", "");
            }

            await BlazorStrap.Interop.RemoveClassAsync(MyRef, "show", 50);
            if (BackdropRef != null)
                await BackdropRef.ToggleAsync();

            if (await BlazorStrap.Interop.TransitionDidNotStartAsync(MyRef))
            {
                await TransitionEndAsync();
            }

            _leaveBodyAlone = false;
        }

        public override async Task ShowAsync()
        {
            _called = true;
            _lock = true;
            Shown = true;
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            await BlazorStrap.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Keyup);
            await BlazorStrap.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Click);

            if (!EventsSet)
            {
                await BlazorStrap.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.TransitionEnd);
                EventsSet = true;
            }

            // Used to hide popovers
            BlazorStrap.ForwardToggle("", this);

            await BlazorStrap.Interop.AddBodyClassAsync("modal-open");

            if (!AllowScroll)
            {
                var scrollWidth = await BlazorStrap.Interop.GetScrollBarWidth();
                var viewportHeight = await BlazorStrap.Interop.GetWindowInnerHeightAsync();
                var peakHeight = await BlazorStrap.Interop.PeakHeightAsync(MyRef);

                if (viewportHeight > peakHeight)
                {
                    await BlazorStrap.Interop.SetBodyStyleAsync("overflow", "hidden");
                    if (scrollWidth != 0)
                        await BlazorStrap.Interop.SetBodyStyleAsync("paddingRight", $"{scrollWidth}px");
                }
            }


            BlazorStrapCore.ModalChanged(this);

            if (BackdropRef != null)
                await BackdropRef.ToggleAsync();
            await BlazorStrap.Interop.SetStyleAsync(MyRef, "display", "block", 50);
            await BlazorStrap.Interop.AddClassAsync(MyRef, "show");

            if (await BlazorStrap.Interop.TransitionDidNotStartAsync(MyRef))
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
            _objectRef = DotNetObjectReference.Create<BSModal>(this);
            BlazorStrap.OnEventForward += InteropEventCallback;
            BlazorStrapCore.ModalChange += OnModalChange;
        }

        private async Task BackdropClicked()
        {
            if (IsStaticBackdrop)
            {
                await BlazorStrap.Interop.AddClassAsync(MyRef, "modal-static");
                await BlazorStrap.Interop.RemoveClassAsync(MyRef, "modal-static", 250);
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
            await BlazorStrap.Interop.RemoveEventAsync(this, DataId, EventType.TransitionEnd);
            Style = Shown ? "display: block;" : "display: none;";
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

        private void ClickEvent()
        {
            Toggle();
        }

        public override async Task InteropEventCallback(string id, CallerName name, EventType type)
        {
            if (DataId == id && name.Equals(typeof(ClickForward)) && type == EventType.Click)
            {
                await ToggleAsync();
            }
        }

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type,
            Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (DataId == id && name.Equals(this) && type == EventType.TransitionEnd)
            {
                await TransitionEndAsync();
            }
            else if (DataId == id && name.Equals(this) && type == EventType.Keyup && e?.Key == "Escape")
            {
                if (IsStaticBackdrop)
                {
                    await BlazorStrap.Interop.AddClassAsync(MyRef, "modal-static", 250);
                    await BlazorStrap.Interop.RemoveClassAsync(MyRef, "modal-static");
                    return;
                }

                await HideAsync();
            }
            else if (DataId == id && name.Equals(this) && type == EventType.Click &&
                     e?.Target.ClassList.Any(q => q.Value == "modal") == true)
            {
                if (IsStaticBackdrop)
                {
                    await BlazorStrap.Interop.AddClassAsync(MyRef, "modal-static", 250);
                    await BlazorStrap.Interop.RemoveClassAsync(MyRef, "modal-static");
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
                    await BlazorStrap.Interop.AddClassAsync(MyRef, "modal-static", 250);
                    await BlazorStrap.Interop.RemoveClassAsync(MyRef, "modal-static");
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
            await BlazorStrap.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Keyup);
            await BlazorStrap.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Click);
            if (EventsSet)
                await BlazorStrap.Interop.RemoveEventAsync(this, DataId, EventType.TransitionEnd);
            BlazorStrap.OnEventForward -= InteropEventCallback;
            BlazorStrapCore.ModalChange -= OnModalChange;
            _objectRef.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}