using BlazorStrap.Extensions;
using BlazorStrap.InternalComponents;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Shared.Components.Modal
{
    public abstract class BSModalBase : BlazorStrapToggleBase<BSModalBase>, IAsyncDisposable
    {
        private Func<Task>? _callback;
        private DotNetObjectReference<BSModalBase>? _objectRef;
        private bool _lock;

        /// <summary>
        /// Color of modal. Defaults to <see cref="BSColor.Default"/>
        /// </summary>
        [Parameter] public BSColor ModalColor { get; set; } = BSColor.Default;

        /// <summary>
        /// Allows the page to be scrolled while the Modal is being shown.
        /// </summary>
        [Parameter] public bool AllowScroll { get; set; }

        /// <summary>
        /// CSS classes to be added to Modal activation button.
        /// </summary>
        [Parameter] public string? ButtonClass { get; set; }

        /// <summary>
        /// CSS classes to add to modal dialog
        /// </summary>
        [Parameter] public string? DialogClass { get; set; }

        /// <summary>
        /// Modal content.
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// CSS classes to apply to the modal content.
        /// </summary>
        [Parameter] public string? ContentClass { get; set; }

        /// <summary>
        /// Modal footer.
        /// </summary>
        [Parameter] public RenderFragment<BSModalBase>? Footer { get; set; }

        /// <summary>
        /// CSS classes to be applied to the modal footer.
        /// </summary>
        [Parameter] public string? FooterClass { get; set; }

        /// <summary>
        /// Modal header content.
        /// </summary>
        [Parameter] public RenderFragment? Header { get; set; }

        /// <summary>
        /// CSS classes to be applied to the modal header.
        /// </summary>
        [Parameter] public string? HeaderClass { get; set; }

        /// <summary>
        /// Centers the modal.
        /// </summary>
        [Parameter] public bool IsCentered { get; set; }

        /// <summary>
        /// Enables the modal to be full screen. Set the size with <see cref="FullScreenSize"/>
        /// </summary>
        [Parameter] public bool IsFullScreen { get; set; }

        /// <summary>
        /// Whether or not the modal is scrollable.
        /// </summary>
        [Parameter] public bool IsScrollable { get; set; }

        /// <summary>
        /// Adds a close button to the modal.
        /// </summary>
        [Parameter] public bool HasCloseButton { get; set; } = true;

        /// <summary>
        /// Enables the static backdrop. 
        /// See <see href="https://getbootstrap.com/docs/5.2/components/modal/#static-backdrop">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public bool IsStaticBackdrop { get; set; }

        /// <summary>
        /// Show backdrop. Defaults to true.
        /// </summary>
        [Parameter] public bool ShowBackdrop { get; set; } = true;


        private bool _leaveBodyAlone;

        private bool _shown;
        protected Backdrop? BackdropRef { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? BodyClassBuilder { get; }
        protected abstract string? ContentClassBuilder { get; }
        protected abstract string? DialogClassBuilder { get; }
        protected abstract string? HeaderClassBuilder { get; }
        protected abstract string? FooterClassBuilder { get; }

        protected override bool ShouldRender()
        {
            return !_lock;
        }

        protected ElementReference? MyRef { get; set; }

        /// <summary>
        /// Whether or not modal is shown.
        /// </summary>
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }

        protected string Style { get; set; } = "display: none;";

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
            CanRefresh = false;
            _lock = true;
            Shown = false;
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);
            await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Keyup);
            await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Click);

            if (!EventsSet)
            {
                await BlazorStrapService.Interop.AddEventAsync(_objectRef, DataId, EventType.TransitionEnd);
                EventsSet = true;
            }

            // Used to hide popovers
            BlazorStrapService.ForwardToggle("", this);

            if (!_leaveBodyAlone)
            {
                await BlazorStrapService.Interop.RemoveBodyClassAsync("modal-open");
                await BlazorStrapService.Interop.SetBodyStyleAsync("overflow", "");
                await BlazorStrapService.Interop.SetBodyStyleAsync("paddingRight", "");
            }

            await BlazorStrapService.Interop.RemoveClassAsync(MyRef, "show", 50);
            if (BackdropRef != null)
                await BackdropRef.ToggleAsync();

            if (await BlazorStrapService.Interop.TransitionDidNotStartAsync(MyRef))
            {
                await TransitionEndAsync();
            }

            _leaveBodyAlone = false;
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
            CanRefresh = false;
            _lock = true;
            Shown = true;
            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            await BlazorStrapService.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Keyup);
            await BlazorStrapService.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Click);
            if (!EventsSet)
            {
                await BlazorStrapService.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.TransitionEnd);
                EventsSet = true;
            }

            // Used to hide popovers
            BlazorStrapService.ForwardToggle("", this);

            await BlazorStrapService.Interop.AddBodyClassAsync("modal-open");

            if (!AllowScroll)
            {
                var scrollWidth = await BlazorStrapService.Interop.GetScrollBarWidth();
                var viewportHeight = await BlazorStrapService.Interop.GetWindowInnerHeightAsync();
                var peakHeight = await BlazorStrapService.Interop.PeakHeightAsync(MyRef);

                if (viewportHeight > peakHeight)
                {
                    await BlazorStrapService.Interop.SetBodyStyleAsync("overflow", "hidden");
                    if (scrollWidth != 0)
                        await BlazorStrapService.Interop.SetBodyStyleAsync("paddingRight", $"{scrollWidth}px");
                }
            }


            BlazorStrapService.ModalChanged(this);

            if (BackdropRef != null)
                await BackdropRef.ToggleAsync();
            await BlazorStrapService.Interop.SetStyleAsync(MyRef, "display", "block", 50);
            await BlazorStrapService.Interop.AddClassAsync(MyRef, "show");

            if (await BlazorStrapService.Interop.TransitionDidNotStartAsync(MyRef))
            {
                await TransitionEndAsync();
            }
        }

        private void Toggle()
        {
            EventUtil.AsNonRenderingEventHandler(ToggleAsync).Invoke();
        }


        private Task BackdropClicked()
        {
            if (IsStaticBackdrop)
            {
                _callback = async () =>
                {
                    await BlazorStrapService.Interop.AddClassAsync(MyRef, "modal-static");
                    await BlazorStrapService.Interop.RemoveClassAsync(MyRef, "modal-static", 250);
                };
                return TryCallback();
            }

            return ToggleAsync();
        }
        
        /// <inheritdoc/>
        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }
        
        private async Task TransitionEndAsync()
        {
            
            _callback = async () =>
            {
                await BlazorStrapService.Interop.RemoveEventAsync(this, DataId, EventType.TransitionEnd);
            };
            await TryCallback(false);

            //await BlazorStrapService.Interop.SetStyleAsync(MyRef, "display", "none", 50);
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
            CanRefresh = true;
        }

        protected void ClickEvent()
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
            if (MyRef == null)
                return;
            if (DataId == id && name.Equals(this) && type == EventType.TransitionEnd)
            {
                await TransitionEndAsync();
            }
            else if (DataId == id && name.Equals(this) && type == EventType.Keyup && e?.Key == "Escape")
            {
                if (IsStaticBackdrop)
                {
                    await BlazorStrapService.Interop.AddClassAsync(MyRef, "modal-static", 250);
                    await BlazorStrapService.Interop.RemoveClassAsync(MyRef, "modal-static");
                    return;
                }

                await HideAsync();
            }
            else if (DataId == id && name.Equals(this) && type == EventType.Click &&
                     e?.Target.ClassList.Any(q => q.Value == "modal") == true)
            {
                if (IsStaticBackdrop)
                {

                    await BlazorStrapService.Interop.AddClassAsync(MyRef, "modal-static", 250);
                    await BlazorStrapService.Interop.RemoveClassAsync(MyRef, "modal-static");
                    return;
                }

                await HideAsync();
            }
        }

        private async void OnModalChange(BSModalBase? model, bool fromJs)
        {
            if (fromJs)
            {
                if (_shown)
                    await HideAsync();
                return;
            }

            if (model == this || !_shown) return;
            _leaveBodyAlone = true;
            if (_shown)
                await HideAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _objectRef = DotNetObjectReference.Create(this);
                BlazorStrapService.OnEventForward += InteropEventCallback;
                BlazorStrapService.ModalChange += OnModalChange;
            }
            if (_callback != null)
            {
                await _callback.Invoke();
                _callback = null;
            }
        }
        public async ValueTask DisposeAsync()
        {
            try
            {
                await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Keyup);
                await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Click);
                if (EventsSet)
                    await BlazorStrapService.Interop.RemoveEventAsync(this, DataId, EventType.TransitionEnd);
            }
            catch { }
            BlazorStrapService.OnEventForward -= InteropEventCallback;
            BlazorStrapService.ModalChange -= OnModalChange;
            _objectRef?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
