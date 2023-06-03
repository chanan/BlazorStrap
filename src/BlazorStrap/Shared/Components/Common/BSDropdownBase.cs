using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSDropdownBase : BlazorStrapToggleBase<BSDropdownBase>, IAsyncDisposable
    {
        private ConcurrentQueue<EventQue> _eventQue = new();

        /// <summary>
        /// Allows the dropdown to be opened when mouse is over it.
        /// </summary>
        [Parameter] public bool IsMouseover { get; set; }
        [Parameter] public string Style { get; set; } = string.Empty;
        /// <summary>
        /// Clicking inside the dropdown menu will not close it.
        /// </summary>
        [Parameter] public bool AllowItemClick { get; set; }

        /// <summary>
        /// Clicks outside of the dropdown will not cause the dropdown to close.
        /// </summary>
        [Parameter] public bool AllowOutsideClick { get; set; }

        /// <summary>
        /// Dropdown menu content.
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// Hides the dropdown button and only shows the content.
        /// </summary>
        [Parameter] public bool Demo { get; set; }

        /// <summary>
        /// Adds the <c>dropdown-menu-dark</c> css class making the dropdown content dark.
        /// </summary>
        [Parameter] public bool IsDark { get; set; }

        /// <summary>
        /// Renders the dropdown menu with a <c>div</c> and uses popper.js to create.
        /// </summary>
        [Parameter] public bool IsDiv { get; set; }

        /// <summary>
        /// A combination of <see cref="AllowItemClick"/> and <see cref="AllowOutsideClick"/>.
        /// Requires the dropdown to be closed by clicking the button again.
        /// </summary>
        [Parameter] public bool IsManual { get; set; }

        /// <summary>
        /// Renders dropdown as a <see cref="BSPopover"/> element and sets <see cref="BSPopover.IsNavItemList"/> true.
        /// </summary>
        [Parameter] public bool IsNavPopper { get; set; }

        /// <summary>
        /// Disables dynamic positioning.
        /// </summary>
        [Parameter] public bool IsStatic { get; set; }

        /// <summary>
        /// Dropdown offset.
        /// </summary>
        [Parameter] public string? Offset { get; set; }

        /// <summary>
        /// Dropdown placement.
        /// </summary>
        [Parameter] public Placement Placement { get; set; } = Placement.RightStart;

        /// <summary>
        /// Attribute to add when dropdown is shown.
        /// </summary>
        [Parameter] public string? ShownAttribute { get; set; }

        /// <summary>
        /// data-blazorstrap data Id of target element
        /// </summary>
        [Parameter] public string Target { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Element to be used to toggle the dropdown.
        /// </summary>
        [Parameter] public RenderFragment? Toggler { get; set; }

        private bool _lastIsNavPopper;
        [CascadingParameter] public BSNavItemBase? DropdownItem { get; set; }
        [CascadingParameter] public BSButtonGroupBase? Group { get; set; }
        [CascadingParameter] public BSInputGroupBase? InputGroup { get; set; }
        [CascadingParameter] public BSNavItemBase? NavItem { get; set; }
        [CascadingParameter] public BSDropdownBase? Parent { get; set; }
        public bool Active { get; private set; }
        internal int ChildCount { get; set; }
        public string DataRefId => (PopoverRef != null) ? PopoverRef.DataId : DataId;
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? StyleBuilder { get; }
        protected abstract string? GroupClassBuilder { get; }
        protected abstract string? IsDivClassBuilder { get; }

        protected ElementReference? MyRef { get; set; }
        internal Action<bool, BSDropdownItemBase>? OnSetActive { get; set; }
        protected BSPopoverBase? PopoverRef { get; set; }
        private bool _secondRender;
        /// <summary>
        /// Whether or not the dropdown is shown.
        /// </summary>
        public override bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }

        private bool _shown;
        public override async Task ShowAsync()
        {
            if (_shown) return;
            await OnShow.InvokeAsync(this);
            var isPopper = ((Group != null || InputGroup != null) && PopoverRef != null && !IsStatic) || (IsDiv || Parent != null || IsNavPopper);
            var taskSource = new TaskCompletionSource<bool>();

            var func = async () =>
            {
                _shown = true;
                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.ShowDropdownAsync(MyRef.Value, isPopper, Placement, Target);
                    if (syncResult is not null)
                        Sync(syncResult);
                }

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
        /// Hide the dropdown
        /// </summary>
        /// <returns>Completed task once hide is complete.</returns>
        public override async Task HideAsync()
        {
            if (!_shown) return;
            await OnHide.InvokeAsync(this);
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                _shown = false;
                if (MyRef is not null)
                {
                    var syncResult = await BlazorStrapService.JavaScriptInterop.HideDropdownAsync(MyRef.Value);
                    if (syncResult is not null)
                        Sync(syncResult);
                }

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
   
        public override async Task InteropEventCallback(string id, CallerName name, EventType type)
        {
            if (id == DataId && name.Equals(typeof(ClickForward)) && type == EventType.Click)
            {
                await ToggleAsync();
            }
        }

        /// <summary>
        /// Toggles dropdown open or closed.yuo
        /// </summary>
        /// <returns>Completed task once dropdown is open or closed.</returns>
        public override Task ToggleAsync()
        {
            return _shown ? HideAsync() : ShowAsync();
        }

        protected override void OnInitialized()
        {
            BlazorStrapService.OnEvent += OnEventAsync;
            _lastIsNavPopper = IsNavPopper;
        }

        protected override void OnParametersSet()
        {
            if (IsNavPopper == false)
            {
                if (_lastIsNavPopper != IsNavPopper)
                {
                    PopoverRef = null;
                }
            }
            else
            {
                if (IsNavPopper != _lastIsNavPopper == false)
                {
                    Shown = false;
                    StateHasChanged();
                }
            }

            _lastIsNavPopper = IsNavPopper;
        }

        internal void SetActive(bool active, BSDropdownItemBase item)
        {
            OnSetActive?.Invoke(active, item);
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
                if (IsMouseover && Toggler == null)
                {
                    await BlazorStrapService.JavaScriptInterop.AddEventAsync(Target, DataId, EventType.Mouseenter);
                    await BlazorStrapService.JavaScriptInterop.AddEventAsync(Target, DataId, EventType.Mouseleave);
                }
                _secondRender = true;
                BlazorStrapService.OnEventForward += InteropEventCallback;
            }
            
        }
        public override async Task OnEventAsync(string sender, string target, EventType type, object? data)
        {
            if(target != DataId && target != Target) return;
            if (Demo) return;
            if (IsManual) return;

            if (type == EventType.Click && target == DataRefId && sender == "javascript")
            {
                await HideAsync();
            }
            if(IsMouseover && sender == "javascript" && target == Target && type == EventType.Mouseenter)
            {
                await ShowAsync();
            }
            if (IsMouseover && sender == "javascript" && target == Target && type == EventType.Mouseleave  )
            {
                await HideAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                await BlazorStrapService.JavaScriptInterop.RemoveDocumentEventAsync(EventType.Click, DataId);
            }
            catch { }
            BlazorStrapService.OnEvent -= OnEventAsync;
            BlazorStrapService.OnEventForward -= InteropEventCallback;
            GC.SuppressFinalize(this);
        }
    }
}
