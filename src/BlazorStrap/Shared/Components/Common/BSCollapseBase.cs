using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

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
        [Parameter] public string Style { get; set; } = string.Empty;

        private bool _defaultShown;

        //Prevents the default state from overriding current state
        private bool _hasRendered;
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? StyleBuilder { get; }

        protected ElementReference? MyRef { get; set; }
        protected override bool ShouldRender() => CanRefresh;
        System.Diagnostics.Stopwatch _stopwatch = new();
        public override async Task ShowAsync()
        {
            _stopwatch.Restart();
            if (MyRef is null) throw new NullReferenceException("ElementReference is null");
            if (_shown) return ;
            var taskSource = new TaskCompletionSource<bool>();

            if (OnShow.HasDelegate)
                await OnShow.InvokeAsync(this); 

            // Que Event
            var func = async () =>
            {
                _shown = true;
                //Lock Rendering
                CanRefresh = false;
                var syncResult = await BlazorStrapService.JavaScriptInterop.ShowCollapseAsync(MyRef.Value, IsHorizontal);
                if(syncResult is not null)
                    Sync(syncResult);

                //Unlock Rendering
                CanRefresh = true;
                await InvokeAsync(StateHasChanged);
                taskSource.SetResult(true);
                _ = Task.Run(() => { _ = OnShown.InvokeAsync(this); });
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
            if (MyRef is null) throw new NullReferenceException("ElementReference is null");
            if (!_shown) return ;
            _ = Task.Run(() => { _ = OnHide.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                _shown = false;
                //Lock Rendering
                CanRefresh = false;

                var syncResult = await BlazorStrapService.JavaScriptInterop.HideCollapseAsync(MyRef.Value, IsHorizontal);
                if (syncResult is not null)
                    Sync(syncResult);

                CanRefresh = true;
                await InvokeAsync(StateHasChanged);
                taskSource.SetResult(true);
                _ = Task.Run(() => { _ = OnHidden.InvokeAsync(this); });
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
                    _stopwatch.Stop();
                    Console.WriteLine($"Time to render: {_stopwatch.ElapsedMilliseconds}ms");
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
