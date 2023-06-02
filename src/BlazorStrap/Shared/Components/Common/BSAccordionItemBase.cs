using BlazorStrap.Extensions;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Concurrent;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSAccordionItemBase : BlazorStrapToggleBase<BSAccordionItemBase>, IDisposable
    {
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }

        private bool _shown;

        private ConcurrentQueue<EventQue> _eventQue = new();

        private DotNetObjectReference<BSAccordionItemBase>? _objectRef;

        /// <summary>
        /// Disables animations during collapsing of the accoridion item.
        /// </summary>
        [Parameter]
        public bool NoAnimations { get; set; }

        /// <summary>
        /// Makes accordion item stay open when another item is opened.
        /// See <see href="https://getbootstrap.com/docs/5.2/components/accordion/#always-open">Bootstrap Documentation</see>
        /// </summary>
        [Parameter]
        public bool AlwaysOpen { get; set; }

        /// <summary>
        /// </summary>
        [Parameter]
        public RenderFragment? Content { get; set; }

        /// <summary>
        /// Accordion item is shown by default.
        /// </summary>
        [Parameter]
        public bool? DefaultShown { get; set; } = null;

        /// <summary>
        /// Accordion item header content.
        /// </summary>
        [Parameter]
        public RenderFragment? Header { get; set; }

        /// <summary>
        /// Sets the style of the accordion item collapse
        /// </summary>
        [Parameter]
        public string Style { get; set; } = string.Empty;

        [CascadingParameter] public BSAccordionBase? Parent { get; set; }
        protected ElementReference? ButtonRef { get; set; }

        public ElementReference? MyRef { get; set; }

        /// <summary>
        /// Returns whether or not the accordion item is shown.
        /// </summary>
        /// <remarks>Can be accessed using @ref</remarks>
        protected abstract string? LayoutClass { get; }
        protected abstract string? StyleBuilder { get; }
        protected abstract string? ClassBuilder { get; }
        private bool _secondRender;

        protected override void OnInitialized()
        {
            BlazorStrapService.OnEvent += OnEventAsync;
            if (Parent is not null)
            {
                if (Parent.Children.Count == 0)
                {
                    if (DefaultShown is null)
                        _shown = true;
                }
                else
                {
                    if (DefaultShown is not null)
                        _shown = DefaultShown.Value;
                }
                Parent.AddChild(this);
            }
        }

        /// <inheritdoc/>
        public override async Task ShowAsync()
        {
            if (_shown) return;
            await OnShow.InvokeAsync(this);
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                _shown = true;
                CanRefresh = false;
                if (MyRef is not null && Parent is not null)
                {
                    Parent.UpdateChild(this);
                    var toClose = Parent.Children.Values.FirstOrDefault(x => x != this && x.Shown && x.AlwaysOpen == false);
                    var syncResults = await BlazorStrapService.JavaScriptInterop.ShowAccordionAsync(MyRef.Value, toClose?.MyRef ?? null);
                    if (syncResults.Count > 1)
                    {
                        Sync(syncResults[0]);
                        if (toClose is not null)
                            await BlazorStrapService.InvokeEvent(this.DataId, toClose.DataId, EventType.Sync, syncResults[1]);
                    }
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
        public override async Task HideAsync()
        {
            if (!_shown) return;
            _ = Task.Run(() => { _ = OnHide.InvokeAsync(this); });
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                _shown = false;
                //Lock Rendering
                CanRefresh = false;

                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.HideCollapseAsync(MyRef.Value, false);
                    if (syncResult is not null)
                        Sync(syncResult);
                }

                CanRefresh = true;
                await InvokeAsync(StateHasChanged);
                taskSource.SetResult(true);
                await OnHidden.InvokeAsync(this);
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
            }
        }

        public override async Task OnEventAsync(string sender, string target, EventType type, object? data)
        {
            if (sender == "javascript" && target == DataId && type == EventType.Hide)
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
            else if(target == DataId && type == EventType.Sync)
            {
                if (data is InteropSyncResult syncResult)
                {
                    if (!syncResult.ClassList.Contains("open"))
                        _shown = false;
                    Sync(syncResult);
                    if(Parent is not null)
                        Parent.UpdateChild(this);
                    await InvokeAsync(StateHasChanged);
                }
            }
        }

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type, Dictionary<string, string>? classList,
            JavascriptEvent? e)
        {
        }

        public void Dispose()
        {
            BlazorStrapService.OnEvent -= OnEventAsync;
            if (Parent is not null)
                Parent.RemoveChild(this);
            _objectRef?.Dispose();
        }
    }
}