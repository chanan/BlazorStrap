using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSCollapseBase : BlazorStrapToggleBase<BSCollapseBase>, IAsyncDisposable
    {
        public override bool Shown { get; protected set; }
      

        private bool _shown;
        private ConcurrentQueue<EventQue> _eventQue = new();

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
            set
            {
                _defaultShown = value;
                if (!_hasRendered)
                {
                    Shown = value;
                    _shown = value;
                }
            }
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
        private bool _secondRender;
        public override async Task ShowAsync()
        {
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
                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.ShowCollapseAsync(MyRef.Value, IsHorizontal);
                    if (syncResult is not null)
                        Sync(syncResult);
                }
                //Unlock Rendering
                CanRefresh = true;
                await InvokeAsync(StateHasChanged);
                taskSource.SetResult(true);
                await OnShown.InvokeAsync(this);
                Shown = true;
            };
            _eventQue.Enqueue(new EventQue { TaskSource = taskSource, Func = func});

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
            await OnHide.InvokeAsync(this); 
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                _shown = false;
                //Lock Rendering
                CanRefresh = false;

                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.HideCollapseAsync(MyRef.Value, IsHorizontal);
                    if (syncResult is not null)
                        Sync(syncResult);
                }

                CanRefresh = true;
                await InvokeAsync(StateHasChanged);
                taskSource.SetResult(true);
                await OnHidden.InvokeAsync(this);
                Shown = false;
            };

            _eventQue.Enqueue(new EventQue { TaskSource = taskSource, Func = func });
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
            BlazorStrapService.OnEvent += OnEventAsync;
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
                if (IsInNavbar)
                {
                    await BlazorStrapService.JavaScriptInterop.AddDocumentEventAsync(EventType.Resize, DataId);
                }
                _secondRender = true;
                _hasRendered = true;
                _objectRef = DotNetObjectReference.Create(this);
            }
        }
        
        protected abstract Task OnResizeAsync(int width);

        public override async Task OnEventAsync(string sender, string target, EventType type, object? data)
        {
            if(sender == "jsdocument" && target.Contains(DataId) && type == EventType.Resize)
            {
                if(data is int width)
                    await OnResizeAsync(width);
            }
        }
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

        public async ValueTask DisposeAsync()
        {
            BlazorStrapService.OnEvent -= OnEventAsync;
            if (IsInNavbar)
            {
                try
                {
                    await BlazorStrapService.JavaScriptInterop.RemoveDocumentEventAsync(EventType.Resize, DataId);
                }
                catch { }
            }

            BlazorStrapService.OnEventForward -= InteropEventCallback;
            _objectRef?.Dispose();
        }
    }
}
