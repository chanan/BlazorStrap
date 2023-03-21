using BlazorStrap.Extensions;
using BlazorStrap.InternalComponents;
using BlazorStrap.Shared.Components.Modal;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorStrap.Shared.Components.OffCanvas
{
    public abstract class BSOffCanvasBase : BlazorStrapToggleBase<BSOffCanvasBase>
    {
        
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }

        protected Backdrop? BackdropRef { get; set; }
        protected ElementReference? MyRef { get; set; }
        
        private IList<EventQue> _eventQue = new List<EventQue>();
        private DotNetObjectReference<BSOffCanvasBase>? _objectRef;
        private bool _shown;
        private bool _leaveBodyAlone;
        
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
        
          
        #region Render props
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? BodyClassBuilder { get; }
        protected abstract string? HeaderClassBuilder { get; }
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

                // Used to hide popovers
                BlazorStrapService.ForwardToggle("", this);
                //await BlazorStrapService.Interop.HideModalAsync(_objectRef, DataId, MyRef, !_leaveBodyAlone);

                //await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Keyup);
                //await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Click);

                //if (ShowBackdrop)
                //{
                //    if (!AllowScroll)
                //    {
                //        var scrollWidth = await BlazorStrapService.Interop.GetScrollBarWidth();
                //        await BlazorStrapService.Interop.SetBodyStyleAsync("overflow", "");
                //        await BlazorStrapService.Interop.SetBodyStyleAsync("paddingRight", "");
                //    }
                //}

                //// Used to hide popovers
                //BlazorStrapService.ForwardToggle("", this);

                //try
                //{
                //    await BlazorStrapService.Interop.RemoveClassAsync(MyRef, "show");
                //    await BlazorStrapService.Interop.WaitForTransitionEnd(MyRef, 300);
                //}

                //catch //Animation failed cleaning up
                //{
                //}

                if (BackdropRef != null)
                    await BackdropRef.HideAsync();

                _shown = false;
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
            
            // Used to hide popovers
            BlazorStrapService.ForwardToggle("", this);
            
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                CanRefresh = false;
                await BlazorStrapService.Interop.ShowOffcanvasAsync(_objectRef, DataId, MyRef, !AllowScroll, ShowBackdrop);
                //await BlazorStrapService.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Keyup);
                //await BlazorStrapService.Interop.AddDocumentEventAsync(_objectRef, DataId, EventType.Click);

                //if (BackdropRef != null)
                //    await BackdropRef.ShowAsync();

                //if (ShowBackdrop)
                //{
                //    if (!AllowScroll)
                //    {
                //        var scrollWidth = await BlazorStrapService.Interop.GetScrollBarWidth();
                //        await BlazorStrapService.Interop.SetBodyStyleAsync("overflow", "hidden");
                //        await BlazorStrapService.Interop.SetBodyStyleAsync("paddingRight", $"{scrollWidth}px");
                //    }
                //}

                //try
                //{
                //    await BlazorStrapService.Interop.AddClassAsync(MyRef, "show");
                //    await BlazorStrapService.Interop.WaitForTransitionEnd(MyRef, 300);

                //}
                //catch //Animation failed cleaning up
                //{
                //}
                _shown = true;
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
        public async Task ToggleBackdropAndModalChange()
        {
            Console.WriteLine("Here");
            if (BackdropRef != null)
                await BackdropRef.ShowAsync();
        }

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type,
            Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (MyRef == null)
                return;
            else if (DataId == id && name.Equals(this) && type == EventType.Keyup && e?.Key == "Escape")
            {
                await HideAsync();
            }
            else if (DataId == id && name.Equals(this) && type == EventType.Click &&
                     e?.Target.ClassList.Any(q => q.Value == "offcanvas-backdrop") == true)
            {
                await HideAsync();
            }
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
            _objectRef?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
