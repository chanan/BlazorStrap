using BlazorStrap.Extensions;
using BlazorStrap.InternalComponents;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace BlazorStrap.Shared.Components.Modal
{
    public abstract class BSModalBase : BlazorStrapToggleBase<BSModalBase>, IAsyncDisposable
    {
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }

        protected Backdrop? BackdropRef { get; set; }
        protected ElementReference? MyRef { get; set; }
        protected string Style = "display:none";
        private IList<EventQue> _eventQue = new List<EventQue>();
        private DotNetObjectReference<BSModalBase>? _objectRef;
        private bool _shown;
        private bool _leaveBodyAlone;

        #region "Parameters"
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
        #endregion

        
        
        #region Render props
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? BodyClassBuilder { get; }
        protected abstract string? ContentClassBuilder { get; }
        protected abstract string? DialogClassBuilder { get; }
        protected abstract string? HeaderClassBuilder { get; }
        protected abstract string? FooterClassBuilder { get; }
        #endregion
        /// <inheritdoc/>
        public override async Task HideAsync()
        {
            if(!_shown) return ;
            _ = Task.Run(() => { _ = OnHide.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                CanRefresh = false;
                await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Keyup);
                await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Click);

                // Used to hide popovers
                BlazorStrapService.ForwardToggle("", this);

                if (!_leaveBodyAlone)
                {
                    await BlazorStrapService.Interop.RemoveBodyClassAsync("modal-open");
                    await BlazorStrapService.Interop.SetBodyStyleAsync("overflow", "");
                    await BlazorStrapService.Interop.SetBodyStyleAsync("paddingRight", "");
                }

                try
                {
                    await BlazorStrapService.Interop.RemoveClassAsync(MyRef, "show");
                    await BlazorStrapService.Interop.WaitForTransitionEnd(MyRef, 300);
                }
                catch //Animation failed cleaning up
                {
                }

                if (BackdropRef != null)
                    await BackdropRef.HideAsync();

                _shown = false;
                Style = "display:none;";
                _leaveBodyAlone = false;
                await InvokeAsync(StateHasChanged);
                _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
                taskSource.SetResult(true);
                CanRefresh = true;

            };

            _eventQue.Add(new EventQue { TaskSource = taskSource, Func = func });
            // Run event que if only item.
            if (_eventQue.Count == 1) {
                await InvokeAsync(StateHasChanged);
            }
            await taskSource.Task;
        }
        /// <inheritdoc/>
        public override async Task ShowAsync()
        {
            
            if (_shown) return ;
            _ = Task.Run(() => { _ = OnShow.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                CanRefresh = false;
                await BlazorStrapService.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Keyup);
                await BlazorStrapService.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Click);

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
                    await BackdropRef.ShowAsync();

                try
                {
                    await BlazorStrapService.Interop.SetStyleAsync(MyRef, "display", "block", 50);
                    await BlazorStrapService.Interop.AddClassAsync(MyRef, "show");

                    await BlazorStrapService.Interop.WaitForTransitionEnd(MyRef, 300);
                    
                }
                catch //Animation failed cleaning up
                {
                }
                _shown = true;
                Style = "display:block;";
                await InvokeAsync(StateHasChanged);
                _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });
                taskSource.SetResult(true);
                CanRefresh = true;
            };
            _eventQue.Add(new EventQue { TaskSource = taskSource, Func = func});

            // Run event que if only item.
            if (_eventQue.Count == 1)
            {
                await InvokeAsync(StateHasChanged);
            }
            await taskSource.Task;
        }
        /// <inheritdoc/>
        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }
        protected override void OnInitialized()
        {
            CanRefresh = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                if (_eventQue.Count > 0)
                {
                    var eventItem = _eventQue.First();
                    if (eventItem != null)
                    {
                        _eventQue.Remove(eventItem);
                        await eventItem.Func.Invoke();
                    }
                }
            }
            else
            {
                _objectRef = DotNetObjectReference.Create(this);
                BlazorStrapService.OnEventForward += InteropEventCallback;
                BlazorStrapService.ModalChange += OnModalChange;
            }
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
                //await TransitionEndAsync();
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
        protected void ClickEvent()
        {
            Toggle();
        }
        private void Toggle()
        {
            EventUtil.AsNonRenderingEventHandler(ToggleAsync).Invoke();
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
    public class EventQue
    {
        public TaskCompletionSource<bool> TaskSource { get; set; }
        public Func<Task> Func { get; set; }
    }
}