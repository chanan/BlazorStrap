using BlazorStrap.Extensions;
using BlazorStrap.InternalComponents;
using BlazorStrap.Shared.Components.Modal;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Collections.Concurrent;

namespace BlazorStrap.Shared.Components.OffCanvas
{
    public abstract class BSOffCanvasBase : BlazorStrapToggleBase<BSOffCanvasBase> , IDisposable
    {
        
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }

        protected ElementReference? MyRef { get; set; }

        private ConcurrentQueue<EventQue> _eventQue = new();
        private bool _secondRender;
        private bool _shown;
        private bool _leaveBodyAlone;
        
        /// <summary>
        /// Allows the page body to be scrolled while the OffCanvas is being shown.
        /// </summary>
        [Parameter] public bool AllowScroll { get; set; }

        /// <summary>
        /// Disables the escape key from closing the OffCanvas.
        /// </summary>
        [Parameter] public bool DisableEscapeKey { get; set; } = false;

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

        /// <summary>
        /// Setting this to false will hide the content of the offvanvas when it is hidden.
        /// </summary>
        [Parameter] public bool ContentAlwaysRendered { get; set; } = false;
        #region Render props
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? BodyClassBuilder { get; }
        protected abstract string? HeaderClassBuilder { get; }
        #endregion

        protected bool ShouldRenderContent { get; set; } = true;
        private string _style = string.Empty;

        protected override void OnInitialized()
        {
            BlazorStrapService.OnEvent += OnEventAsync;
            ShouldRenderContent = ContentAlwaysRendered;
            CanRefresh = true;
        }
        /// <inheritdoc/>
        public override async Task HideAsync()
        {
            if(!_shown) return ;
            await OnHide.InvokeAsync(this);
            //Kick off to event que
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                _shown = false;
                CanRefresh = false;

                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.HideOffCanvasAsync(MyRef.Value);
                    if (syncResult is not null)
                        Sync(syncResult);
                }


                CanRefresh = true;
                ShouldRenderContent = false;
                await InvokeAsync(StateHasChanged);

                await OnHidden.InvokeAsync(this);
                await BlazorStrapService.JavaScriptInterop.CheckBackdropsAsync();
                taskSource.SetResult(true);
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
            await OnShow.InvokeAsync(this);
            
            // Used to hide popovers
            BlazorStrapService.ForwardToggle("", this);
            
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
                    var syncResult = await BlazorStrapService.JavaScriptInterop.ShowOffCanvasAsync(MyRef.Value, ShowBackdrop);
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

        public override async Task OnEventAsync(string sender, string target, EventType type, object? data)
        {
            if (target != DataId) return;
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
        }

        //[JSInvokable]
        //public override async Task InteropEventCallback(string id, CallerName name, EventType type,
        //    Dictionary<string, string>? classList, JavascriptEvent? e)
        //{
        //    if (MyRef == null)
        //        return;
        //    else if (DataId == id && name.Equals(this) && type == EventType.Keyup && e?.Key == "Escape")
        //    {
        //        await HideAsync();
        //    }
        //    else if (DataId == id && name.Equals(this) && type == EventType.Click &&
        //             e?.Target.ClassList.Any(q => q.Value == "offcanvas-backdrop") == true)
        //    {
        //        await HideAsync();
        //    }
        //}

        protected void ClickEvent()
        {
            Toggle();
        }
        private void Toggle()
        {
            EventUtil.AsNonRenderingEventHandler(ToggleAsync).Invoke();
        }

        public void Dispose()
        {
            BlazorStrapService.OnEvent -= OnEventAsync;
            BlazorStrapService.OnEventForward -= InteropEventCallback;
            GC.SuppressFinalize(this);
        }
    }
}
