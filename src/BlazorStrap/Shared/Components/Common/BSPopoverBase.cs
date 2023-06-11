using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Modal;
using BlazorStrap.Shared.Components.OffCanvas;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Concurrent;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSPopoverBase : BlazorStrapToggleBase<BSPopoverBase>, IAsyncDisposable
    {
        private Func<Task>? _callback;
        private DotNetObjectReference<BSPopoverBase>? _objectRef;
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }
        private bool _shown;
        private ConcurrentQueue<EventQue> _eventQue = new();
        /// <summary>
        /// Popover content.
        /// </summary>
        [Parameter]
        public RenderFragment? Content { get; set; }

        private bool _called;

        /// <summary>
        /// This Parameter is intended for internal use 
        /// </summary>
        [Parameter]
        public string? DropdownOffset { get; set; }

        /// <summary>
        /// For use when the popover is in a nav list.
        /// </summary>
        [Parameter] public bool IsNavItemList { get; set; }

        /// <summary>
        /// Popover header content.
        /// </summary>
        [Parameter]
        public RenderFragment? Header { get; set; }

        /// <summary>
        /// Background color of header.
        /// </summary>
        [Parameter] public BSColor HeaderColor { get; set; }

        /// <summary>
        /// This Parameter is intended for internal use 
        /// </summary>
        [Parameter]
        public bool IsDropdown { get; set; }

        /// <summary>
        /// Whether or not the popover is shown on mouseover
        /// </summary>
        [Parameter] public bool MouseOver { get; set; }

        /// <summary>
        /// Popover placement.
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Top;

        /// <summary>
        /// Data-Blazorstrap attribute value to target.
        /// </summary>
        [Parameter] public string? Target { get; set; }

        /// <summary>
        /// Setting this to true will disable the javascript click event on the target.
        /// </summary>
        [Parameter] public bool NoClickEvent { get; set; }

        /// <summary>
        /// Setting this to false will hide the content of the popover when it is hidden.
        /// </summary>
        [Parameter] public bool ContentAlwaysRendered { get; set; } = true;
        /// <summary>
        /// Sets additional popper.js options.
        /// </summary>
        [Parameter] public object? PopperOptions { get; set; } = null;

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? HeaderClass { get; }

        private bool HasRender { get; set; }

        protected ElementReference? MyRef { get; set; }

        [Parameter]
        public string Style { get; set; } = string.Empty;

        protected bool ShouldRenderContent { get; set; } = true;
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
        /// <summary>
        /// Method used to dynamically create and show popover element.
        /// </summary>
        /// <param name="target">Data-Blazorstrap attribute value to target.</param>
        /// <param name="content">Popover content.</param>
        /// <param name="placement">Popover placement. See <see cref="Placement"/></param>
        /// <param name="header">Header content</param>
        /// <returns>Completed task when popover is shown.</returns>
        /// <exception cref="NullReferenceException">When <paramref name="target"/> or <paramref name="content"/> is null</exception>
        public async Task ShowAsync(string? target, string? content, Placement placement, string? header = null)
        {

            if (target == null || content == null)
            {
                throw new NullReferenceException("Target and Content cannot be null");
            }
            Placement = placement;
            Target = target;
            Content = CreateFragment(content);
            if (header != null)
                Header = CreateFragment(header);

            //Hides the old pop up. Placed here allows sizing to work properly don't move
            if (Shown)
                await HideAsync();
            else
                await InvokeAsync(StateHasChanged);
            await ShowAsync();
        }

        /// <inheritdoc/>
        public override Task ToggleAsync()
        {
            return !Shown ? ShowAsync() : HideAsync();
        }

        /// <summary>
        /// Dynamically creates a popover and shows or closes if it's open.
        /// </summary>
        /// <param name="target">Data-Blazorstrap attribute value to target.</param>
        /// <param name="content">Popover content.</param>
        /// <param name="placement">Popover placement. See <see cref="Placement"/></param>
        /// <param name="header">Header content</param>
        /// <returns>Completed task when render is complete.</returns>
        public Task ToggleAsync(string? target, string? content, Placement placement, string? header = null)
        {
            return target == Target && Shown ? HideAsync() : ShowAsync(target, content, placement, header);
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
                _objectRef = DotNetObjectReference.Create<BSPopoverBase>(this);
                BlazorStrapService.OnEventForward += InteropEventCallback;
                HasRender = true;
                if (Target != null)
                {
                    if (!IsDropdown)
                    {
                        if (!NoClickEvent)
                            await BlazorStrapService.JavaScriptInterop.AddEventAsync(Target, DataId, EventType.Click);
                    }
                    if (MouseOver)
                    {
                        await BlazorStrapService.JavaScriptInterop.AddEventAsync(Target, DataId, EventType.Mouseenter);
                        await BlazorStrapService.JavaScriptInterop.AddEventAsync(Target, DataId, EventType.Mouseleave);
                    }
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
            else if(sender == "javascript" && target == Target && type == EventType.Click)
            {
                await ToggleAsync();
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
                if (data is InteropSyncResult syncResult)
                {
                    Sync(syncResult);
                    await InvokeAsync(StateHasChanged);
                }
            }
        }

        public override async Task InteropEventCallback(string id, CallerName name, EventType type)
        {
            if (id == Target && name.Equals(typeof(ClickForward)) && type == EventType.Click)
            {
                await ToggleAsync();
            }
            else if ((name.Equals(typeof(BSModalBase)) || name.Equals(typeof(BSOffCanvasBase))) && type == EventType.Toggle)
            {
                await HideAsync();
            }
        }

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type, Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            if (id == Target && name.Equals(this) && type == EventType.Click)
            {
                await ToggleAsync();
            }
            else if (id == Target && name.Equals(this) && type == EventType.Mouseenter)
            {
                await ShowAsync();
            }
            else if (id == Target && name.Equals(this) && type == EventType.Mouseleave)
            {
                await HideAsync();
            }
            else if (id == DataId && name.Equals(this) && type == EventType.Click)
            {
                await ToggleAsync();
            }

        }

        #region Dispose

        public async ValueTask DisposeAsync()
        {
            try
            {
                BlazorStrapService.OnEvent -= OnEventAsync;
                if (Target is not null)
                {
                    if (!IsDropdown)
                    {
                        if (!NoClickEvent)
                            await BlazorStrapService.JavaScriptInterop.RemoveEventAsync(Target, DataId, EventType.Click);
                    }
                    if (MouseOver)
                    {
                        await BlazorStrapService.JavaScriptInterop.RemoveEventAsync(Target, DataId, EventType.Mouseenter);
                        await BlazorStrapService.JavaScriptInterop.RemoveEventAsync(Target, DataId, EventType.Mouseleave);
                    }
                }
            }
            catch { }
            GC.SuppressFinalize(this);
        }
        private RenderFragment CreateFragment(string value) => (builder) => builder.AddMarkupContent(0, value);
        #endregion
    }
}
