using BlazorStrap.Extensions;
using BlazorStrap.InternalComponents;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;

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
        //private IList<EventQue> _eventQue = new List<EventQue>();
        private DotNetObjectReference<BSModalBase>? _objectRef;
        private bool _shown;
        private bool _leaveBodyAlone;
        private ConcurrentQueue<EventQue> _eventQue = new();
        [Parameter] public string Style { get; set; } = string.Empty;

        #region "Parameters"
        /// <summary>
        /// Color of modal. Defaults to <see cref="BSColor.Default"/>
        /// </summary>
        [Parameter] public BSColor ModalColor { get; set; } = BSColor.Default;

        /// <summary>
        /// Disables the escape key from closing the modal.
        /// </summary>
        [Parameter] public bool DisableEscapeKey { get; set; } = false;

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
        /// CSS classes to apply to the modal body.
        /// </summary>
        [Parameter] public string? BodyClass { get; set; }

        /// <summary>
        /// CSS classes to apply to the modal content.
        /// </summary>
        [Parameter] public string? ModalContentClass { get; set; }

        /// <summary>
        /// Modal content.
        /// </summary>
        [Parameter] public RenderFragment<BSModalBase>? Content { get; set; }

        /// <summary>
        /// CSS classes to apply to the modal content.
        /// </summary>
        [Obsolete("ContentClass is deprecated, please use BodyClass instead.")]
        [Parameter] public string? ContentClass { get => BodyClass; set => BodyClass = value; }

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

        /// <summary>
        /// Hides the modal on valid submit
        /// </summary>
        [Parameter] public bool HideOnValidSubmit { get; set; } = false;

        /// <summary>
        /// Hides the modal on submit
        /// </summary>
        [Parameter] public bool HideOnSubmit { get; set; } = false;
        #endregion

        /// <summary>
        /// Setting this to false will hide the content of the modal when it is hidden.
        /// </summary>
        [Parameter] public bool ContentAlwaysRendered { get; set; } = false;

        /// <summary>
        /// Using this will allow you to manually control the modal show/hide state. It ignores all forwarded events and only responds to the Show/Hide methods.
        /// </summary>
        [Parameter] public bool IsManual { get; set; } 

        #region Render props
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? StyleBuilder { get; }
        protected abstract string? BodyClassBuilder { get; }
        protected abstract string? ContentClassBuilder { get; }
        protected abstract string? DialogClassBuilder { get; }
        protected abstract string? HeaderClassBuilder { get; }
        protected abstract string? FooterClassBuilder { get; }
        #endregion

        protected bool ShouldRenderContent { get; set; } = false;
        private bool _secondRender;

        protected override void OnInitialized()
        {
            BlazorStrapService.OnEvent += OnEventAsync;
            ShouldRenderContent = ContentAlwaysRendered;
            CanRefresh = true;
        }
        /// <inheritdoc/>
        public override async Task HideAsync()
        {
            if (!_shown) return ;
            _ = Task.Run(() => { _ = OnHide.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                _shown = false;
                CanRefresh = false;

                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.HideModalAsync(MyRef.Value);
                    if (syncResult is not null)
                        Sync(syncResult);
                }

                CanRefresh = true;
                ShouldRenderContent = false;
                
                await InvokeAsync(StateHasChanged);
                taskSource.SetResult(true);
                _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
            };

            _eventQue.Enqueue(new EventQue { TaskSource = taskSource, Func = func });
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
            ShouldRenderContent = true;
            _ = Task.Run(() => { _ = OnShow.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                _shown = true;
                CanRefresh = false;
           
                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.ShowModalAsync(MyRef.Value, ShowBackdrop);
                    if (syncResult is not null)
                        Sync(syncResult);
                }
             
                CanRefresh = true;
                await InvokeAsync(StateHasChanged);
                taskSource.SetResult(true);
                _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });
            };
            
            _eventQue.Enqueue(new EventQue { TaskSource = taskSource, Func = func});

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender && _secondRender)
            {
                if (_eventQue.TryDequeue(out var eventItem))
                {
                    await eventItem.Func.Invoke();
                }
            }
            else
            {
                _secondRender = true;
                _objectRef = DotNetObjectReference.Create(this);
                BlazorStrapService.OnEventForward += InteropEventCallback;
                BlazorStrapService.ModalChange += OnModalChange;
            }
        }


        public override async Task InteropEventCallback(string id, CallerName name, EventType type)
        {
            if (DataId == id && name.Equals(typeof(ClickForward)) && type == EventType.Click && !IsManual)
            {
                await ToggleAsync();
            }
        }


        public override async Task OnEventAsync(string sender, string target, EventType type, object data)
        {
            if(sender == "javascript" && target == DataId && type == EventType.Hide)
            {
                await HideAsync();
            }
            else if (sender == "javascript" && target == DataId && type == EventType.Show)
            {
                await ShowAsync();
            }
            else if (sender == "javascript" && target == DataId && type == EventType.Toggle)
            {
                await ToggleAsync();
            }
        }

        [JSInvokable]
        public async Task ToggleBackdropAndModalChange()
        {
            BlazorStrapService.ModalChanged(this);

            if (BackdropRef != null)
                await BackdropRef.ShowAsync();
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
            else if (DataId == id && name.Equals(this) && type == EventType.Keyup && e?.Key == "Escape" && !IsManual)
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
                     e?.Target.ClassList.Any(q => q.Value == "modal") == true && !IsManual)
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
            if (IsManual) return;
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
            BlazorStrapService.OnEvent -= OnEventAsync;
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