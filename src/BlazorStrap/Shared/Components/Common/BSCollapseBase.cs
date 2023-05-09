using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSCollapseBase : BlazorStrapToggleBase<BSCollapseBase>, IAsyncDisposable
    {
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }

        private bool _shown;
        private IList<EventQue> _eventQue = new List<EventQue>();
                
        private DotNetObjectReference<BSCollapseBase>? _objectRef;
        [CascadingParameter] BSCollapseBase? Parent { get; set; }
        [CascadingParameter] BSAccordionItemBase? AccordionParent { get; set; }

      //  internal Action? NestedHandler { get; set; }
        
        /// <summary>
        /// Disables animations when set.
        /// </summary>
        [Parameter] public bool NoAnimations { get; set; }

        /// <summary>
        /// Content of collapse.
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// Collapse content is shown by default.
        /// </summary>
        [Parameter]
        public bool DefaultShown
        {
            get => _defaultShown;
            set { _defaultShown = value; if (!_hasRendered) Shown = value; }
        }

        /// <summary>
        /// Adds the navbar-collapse class to the item.
        /// </summary>
        [Parameter] public bool IsInNavbar { get; set; }

        /// <summary>
        /// Sets if the collarpse should be rendered with <c>&lt;ul&gt;</c>
        /// </summary>
        [Parameter] public bool IsList { get; set; }

        /// <summary>
        /// Fragment used as the toggler.
        /// </summary>
        [Parameter] public RenderFragment? Toggler { get; set; }

        [Parameter] public bool IsHorizontal { get; set; }

        private bool _defaultShown;

        //Prevents the default state from overriding current state
        private bool _hasRendered;

        

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        protected ElementReference? MyRef { get; set; }
        protected override bool ShouldRender()
        {
            return CanRefresh;
        }
        public override async Task ShowAsync()
        {
            if (_shown) return ;
            _ = Task.Run(() => { _ = OnShow.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                CanRefresh = false;
                
                try
                {
                    if (!NoAnimations)
                    {
                        if(IsHorizontal)
                        {
                            await BlazorStrapService.Interop.AnimateHorizontalCollapseAsync(_objectRef, MyRef, DataId, true);
                        }
                        else
                        {
                            await BlazorStrapService.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, true);
                        }
                        await BlazorStrapService.Interop.WaitForTransitionEnd(MyRef, 500);
                        await BlazorStrapService.Interop.SetStyleAsync(MyRef, "height", "");
                    }
                }
                catch //Animation failed cleaning up
                {
                }
                _shown = true;
                CanRefresh = true;
                await InvokeAsync(StateHasChanged);
                _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });
                taskSource.SetResult(true);
            };
            _eventQue.Add(new EventQue { TaskSource = taskSource, Func = func});

            // Run event que if only item.
            if (_eventQue.Count == 1)
            {
                await InvokeAsync(StateHasChanged);
            }
            await taskSource.Task;
        }
        
        public override async Task HideAsync()
        {
            if (!_shown) return ;
            _ = Task.Run(() => { _ = OnHide.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                CanRefresh = false;

                try
                {
                    if (!NoAnimations)
                    {
                        if (IsHorizontal)
                        {
                            await BlazorStrapService.Interop.AnimateHorizontalCollapseAsync(_objectRef, MyRef, DataId, false);
                            await BlazorStrapService.Interop.WaitForTransitionEnd(MyRef, 500);
                        }
                        else
                        {
                            await BlazorStrapService.Interop.AnimateCollapseAsync(_objectRef, MyRef, DataId, false);
                            await BlazorStrapService.Interop.WaitForTransitionEnd(MyRef, 500);
                        }
                    }
                }
                catch //Animation failed cleaning up
                {
                }

                CanRefresh = true;
                _shown = false;
                await InvokeAsync(StateHasChanged);
                _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
                taskSource.SetResult(true);
            };

            _eventQue.Add(new EventQue { TaskSource = taskSource, Func = func });
            // Run event que if only item.
            if (_eventQue.Count == 1) {
                await InvokeAsync(StateHasChanged);
            }
            await taskSource.Task;

        }
        
        public override Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        protected override void OnInitialized()
        {
            CanRefresh = true;
            BlazorStrapService.OnEventForward += InteropEventCallback;
            // if (Parent?.NestedHandler != null)
            //     Parent.NestedHandler += NestedHandlerEvent;
            // if (AccordionParent?.NestedHandler != null)
            //     AccordionParent.NestedHandler += NestedHandlerEvent;
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
                _hasRendered = true;
                _objectRef = DotNetObjectReference.Create(this);
            }
        }
        
        /// <summary>
        /// TODO Test
        /// </summary>
        private async void NestedHandlerEvent()
        {
          //  if (!_lock) return;
           // await TransitionEndAsync();
        }

        protected abstract Task OnResize(int width);

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
        }

        public async ValueTask DisposeAsync()
        {
            if (IsInNavbar)
            {
                try
                {
                    await BlazorStrapService.Interop.RemoveDocumentEventAsync(this, DataId, EventType.Resize);
                }
                catch { }
            }
            
            // if (Parent?.NestedHandler != null)
            //     Parent.NestedHandler -= NestedHandlerEvent;
            // if (AccordionParent?.NestedHandler != null)
            //     AccordionParent.NestedHandler -= NestedHandlerEvent;

            BlazorStrapService.OnEventForward -= InteropEventCallback;
            _objectRef?.Dispose();
        }
    }
}
