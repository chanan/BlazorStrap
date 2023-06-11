using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSTooltipBase : BlazorStrapToggleBase<BSTooltipBase>, IAsyncDisposable
    {
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }
        private bool _shown;
        private ConcurrentQueue<EventQue> _eventQue = new();
        /// <summary>
        /// Tooltip placement.
        /// </summary>
        [Parameter] public Placement Placement { get; set; }

        /// <summary>
        /// DataID of target.
        /// </summary>
        [Parameter] public string? Target { get; set; }

        /// <summary>
        /// Setting this to false will hide the content of the tooltip when it is hidden.
        /// </summary>
        [Parameter] public bool ContentAlwaysRendered { get; set; } = true;
        /// <summary>
        /// Sets additional popper.js options.
        /// </summary>
        [Parameter] public object? PopperOptions { get; set; } = null;

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        private bool HasRender { get; set; }

        protected ElementReference? MyRef { get; set; }
        [Parameter]
        public string Style { get; set; } = string.Empty;
        protected bool ShouldRenderContent { get; set; } = false;
        private bool _secondRender;
        protected override void OnInitialized()
        {
            BlazorStrapService.OnEvent += OnEventAsync;
            ShouldRenderContent = ContentAlwaysRendered;
        }
        
        /// <inheritdoc/>
        public override async Task HideAsync()
        {
            if (!_shown) return;
            await OnHide.InvokeAsync(this);
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                _shown = false;
                CanRefresh = false;

                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.HideTooltipAsync(MyRef.Value);
                    if (syncResult is not null)
                        Sync(syncResult);
                }

                CanRefresh = true;
                ShouldRenderContent = false;

                await InvokeAsync(StateHasChanged);
                await OnHidden.InvokeAsync(this);
                taskSource.SetResult(true);
            };

            _eventQue.Enqueue(new EventQue { TaskSource = taskSource, Func = func });
            // Run event que if only item.
            if (_eventQue.Count == 1)
            {
                await InvokeAsync(StateHasChanged);
            }
            await taskSource.Task;
        }

        /// <inheritdoc/>
        public override async Task ShowAsync()
        {
            if (Target is null) throw new InvalidOperationException("A taget is required to show the tooltip");
            if (_shown) return;
            ShouldRenderContent = true;
            await OnShow.InvokeAsync(this);
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                if (!ShouldRenderContent)
                {
                    ShouldRenderContent = true;
                    _shown = false;
                    await ShowAsync();
                    return;
                }

                _shown = true;
                CanRefresh = false;

                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.ShowTooltipAsync(MyRef.Value, Placement, Target, options: PopperOptions);
                    if (syncResult is not null)
                        Sync(syncResult);
                }

                CanRefresh = true;
                await InvokeAsync(StateHasChanged);
                taskSource.SetResult(true);
                await OnShown.InvokeAsync(this);
            };

            _eventQue.Enqueue(new EventQue { TaskSource = taskSource, Func = func });

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
            return !Shown ? ShowAsync() : HideAsync();
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
                HasRender = true;
                if (Target is not null)
                {
                    await BlazorStrapService.JavaScriptInterop.AddEventAsync(Target, DataId, EventType.Mouseenter);
                    await BlazorStrapService.JavaScriptInterop.AddEventAsync(Target, DataId, EventType.Mouseleave);
                }
            }
        }
        public override async Task OnEventAsync(string sender, string target, EventType type, object? data)
        {
            if (target != DataId && target != Target) return;
            if (sender == "javascript" && target == Target && type == EventType.Mouseenter)
            {
                await ShowAsync();
            }
            else if (sender == "javascript" && target == Target && type == EventType.Mouseleave)
            {
                await HideAsync();
            }
            else if (sender == "javascript" && target == DataId && type == EventType.Hide)
            {
                await HideAsync();
            }
            else if (sender == "javascript" && target == DataId && type == EventType.Show)
            {
                await ShowAsync();
            }
            else if (sender == "javascript" && target == DataId && type == EventType.Sync)
            {
                if(data is InteropSyncResult syncResult)
                {
                    Sync(syncResult);
                    await InvokeAsync(StateHasChanged);
                }
            }
        }

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type,
            Dictionary<string, string>? classList = null, JavascriptEvent? e = null)
        { 
            if (id == Target && name.Equals(this) && type == EventType.Mouseenter)
            {
                await ShowAsync();
            }
            else if (id == Target && name.Equals(this) && type == EventType.Mouseleave)
            {
                await HideAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                BlazorStrapService.OnEvent -= OnEventAsync;
                if (Target is not null)
                {
                    await BlazorStrapService.JavaScriptInterop.RemoveEventAsync(Target, DataId, EventType.Mouseenter);
                    await BlazorStrapService.JavaScriptInterop.RemoveEventAsync(Target, DataId, EventType.Mouseleave);
                }
            }
            catch { }
            GC.SuppressFinalize(this);
        }
    }
}
