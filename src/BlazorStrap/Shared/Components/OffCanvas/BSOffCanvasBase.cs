using BlazorStrap.Extensions;
using BlazorStrap.Interfaces;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorStrap.Shared.Components.OffCanvas
{
    public abstract class BSOffCanvasBase : BlazorStrapToggleBase<BSOffCanvasBase>, IBSOffCanvas
    {
        private Func<Task>? _callback;
        private DotNetObjectReference<BSOffCanvasBase>? _objectRef;

        /// <summary>
        /// Allows the page body to be scrolled while the OffCanvas is being shown.
        /// </summary>
        [Parameter] public bool AllowScroll { get; set; }

        /// <summary>
        /// CSS classes to be added to the OffCanvas body.
        /// </summary>
        [Parameter] public string? BodyClass { get; set; }

        /// <summary>
        /// CSS classes to be added to OffCanvas activation button.
        /// </summary>
        [Parameter] public string? ButtonClass { get; set; }

        /// <summary>
        /// Color of OffCanvas element. Defaults to <see cref="BSColor.Default"/>
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Content of OffCanvas element.
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// Disables dismissing the element if the backdrop is clicked.
        /// </summary>
        [Parameter] public bool DisableBackdropClick { get; set; }

        /// <summary>
        /// Header content.
        /// </summary>
        [Parameter] public RenderFragment? Header { get; set; }

        /// <summary>
        /// CSS classes to apply to the header.
        /// </summary>
        [Parameter] public string? HeaderClass { get; set; }

        /// <summary>
        /// Can override the default activation button click event.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        /// <summary>
        /// OffCanvas element placement. Defaults to <see cref="Placement.Left"/>
        /// </summary>
        [Parameter] public Placement Placement { get; set; } = Placement.Left;

        /// <summary>
        /// Whether or not to show backdrop. Defaults to true.
        /// </summary>
        [Parameter] public bool ShowBackdrop { get; set; } = true;

        private bool _lock;
        private bool _shown;
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? BackdropClass { get; }
        protected abstract string? BodyClassBuilder { get; }
        protected abstract string? HeaderClassBuilder { get; }

        protected ElementReference BackdropRef { get; set; }
        protected string BackdropStyle { get; set; } = "display: none;";

        
        protected ElementReference? MyRef { get; set; }
        protected override bool ShouldRender()
        {
            return !_lock;
        }

        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }
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
            // Used to hide popovers
            BlazorStrapService.ForwardToggle("", this);
            _lock = true;

            if (!EventsSet)
            {
                await BlazorStrapService.Interop.AddEventAsync(_objectRef, DataId, EventType.TransitionEnd);
                EventsSet = true;
            }

            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this);
            BlazorStrapService.ForwardToggle(DataId, this);

            if (ShowBackdrop)
            {
                await BlazorStrapService.Interop.SetStyleAsync(BackdropRef, "display", "block", 100);
                await BlazorStrapService.Interop.AddClassAsync(BackdropRef, "show");
                BackdropStyle = "display: block;";

            }

            await BlazorStrapService.Interop.AddClassAsync(MyRef, "show");
            if (await BlazorStrapService.Interop.TransitionDidNotStartAsync(MyRef))
            {
                await TransitionEndAsync();
            }
            Shown = true;
            await DoAnimationsAsync(_shown);
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
            // Used to hide popovers
            BlazorStrapService.ForwardToggle("", this);

            _lock = true;
            if (!EventsSet)
            {
                await BlazorStrapService.Interop.AddEventAsync(_objectRef, DataId, EventType.TransitionEnd);
                EventsSet = true;
            }
            if (OnHide.HasDelegate)
                await OnHide.InvokeAsync(this);

            if (ShowBackdrop)
                await BlazorStrapService.Interop.RemoveClassAsync(BackdropRef, "show", 100);
            {
                BackdropStyle = "display: none;";
            }

            await BlazorStrapService.Interop.RemoveClassAsync(MyRef, "show");

            if (await BlazorStrapService.Interop.TransitionDidNotStartAsync(MyRef))
            {
                await TransitionEndAsync();
            }
            Shown = false;
            await DoAnimationsAsync(_shown);
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

        /// <inheritdoc/>
        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        protected async Task BackdropClicked()
        {
            if (DisableBackdropClick) return;
            await ToggleAsync();
        }

        protected async Task ClickEvent()
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
                _callback = async () =>
                {
                    await BlazorStrapService.Interop.RemoveEventAsync(this, DataId, EventType.TransitionEnd);
                };
                await TryCallback(false);
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
            CanRefresh = true;
        }
        private async Task DoAnimationsAsync(bool value)
        {
            if (value)
            {
                if (ShowBackdrop)
                {
                    if (!AllowScroll)
                    {
                        var scrollWidth = await BlazorStrapService.Interop.GetScrollBarWidth();
                        await BlazorStrapService.Interop.SetBodyStyleAsync("overflow", "hidden");
                        await BlazorStrapService.Interop.SetBodyStyleAsync("paddingRight", $"{scrollWidth}px");
                    }
                }
            }
            else
            {
                {
                    await BlazorStrapService.Interop.SetBodyStyleAsync("overflow", "");
                    await BlazorStrapService.Interop.SetBodyStyleAsync("paddingRight", "");
                }
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                BlazorStrapService.OnEventForward += InteropEventCallback;
                _objectRef = DotNetObjectReference.Create(this);
            }
            if (_callback != null)
            {
                await _callback.Invoke();
                _callback = null;
            }
        }
        public async ValueTask DisposeAsync()
        {
            _objectRef?.Dispose();
            try
            {
                BlazorStrapService.OnEventForward -= InteropEventCallback;
                if (!EventsSet)
                {
                    await BlazorStrapService.Interop.RemoveEventAsync(this, DataId, EventType.TransitionEnd);
                }
            }
            catch { }
        }
    }
}
