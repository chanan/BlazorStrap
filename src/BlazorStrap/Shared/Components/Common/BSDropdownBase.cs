using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Concurrent;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSDropdownBase : BlazorStrapBase, IAsyncDisposable
    {
        private ConcurrentQueue<EventQue> _eventQue = new();

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
        private DotNetObjectReference<BSDropdownBase>? _objectRef;
        [CascadingParameter] public BSNavItemBase? DropdownItem { get; set; }
        [CascadingParameter] public BSButtonGroupBase? Group { get; set; }
        [CascadingParameter] public BSInputGroupBase? InputGroup { get; set; }
        [CascadingParameter] public BSNavItemBase? NavItem { get; set; }
        [CascadingParameter] public BSDropdownBase? Parent { get; set; }
        public bool Active { get; private set; }
        internal int ChildCount { get; set; }
        protected string DataRefId => (PopoverRef != null) ? PopoverRef.DataId : DataId;
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? GroupClassBuilder { get; }
        protected abstract string? IsDivClassBuilder { get; }

        protected ElementReference? MyRef { get; set; }
        internal Action<bool, BSDropdownItemBase>? OnSetActive { get; set; }
        protected BSPopoverBase? PopoverRef { get; set; }
        private bool _secondRender;
        /// <summary>
        /// Whether or not the dropdown is shown.
        /// </summary>
        public bool Shown
        {
            get => _shown;
            protected set => _shown = value;
        }

        private bool _shown;
        public async Task ShowAsync()
        {
            if (_shown) return;
            var taskSource = new TaskCompletionSource<bool>();

            var func = async () =>
            {
                await ShowActionsAsync();
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

        private async Task ShowActionsAsync()
        {
            _shown = true;

            if (((Group != null || InputGroup != null) && PopoverRef != null && !IsStatic) || (IsDiv || Parent != null || IsNavPopper))
            {
                if (PopoverRef != null) await PopoverRef.ShowAsync();
            }
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Hide the dropdown
        /// </summary>
        /// <returns>Completed task once hide is complete.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public async Task HideAsync()
        {
            if (!_shown) return;
            var taskSource = new TaskCompletionSource<bool>();
            var func = async () =>
            {
                await HideActionsAsync();
                taskSource.SetResult(true);
                if (Parent != null)
                    await Parent.HideActionsAsync();
            };
            _eventQue.Enqueue(new EventQue { TaskSource = taskSource, Func = func });

            // Run event que if only item.
            if (_eventQue.Count == 1)
            {
                await InvokeAsync(StateHasChanged);
            }
            await taskSource.Task;
        }

        private async Task HideActionsAsync()
        {
            _shown = false;

            if (((Group != null || InputGroup != null) && PopoverRef != null && !IsStatic) || (IsDiv || Parent != null || IsNavPopper))
            {
                if (PopoverRef != null)
                    await PopoverRef.HideAsync();
            }
            await InvokeAsync(StateHasChanged);
            
        }

   
        public override async Task InteropEventCallback(string id, CallerName name, EventType type)
        {
            if (id == DataId && name.Equals(typeof(ClickForward)) && type == EventType.Click)
            {
                await ToggleAsync();
            }
        }

        //[JSInvokable]
        //public override async Task InteropEventCallback(string id, CallerName name, EventType type,
        //    Dictionary<string, string>? classList, JavascriptEvent? e)
        //{
        //    if (Demo) return;
        //    // The if statement was getting hard to read so split into parts 
        //    if (id == DataRefId && name.Equals(this) && type == EventType.Click)
        //    {
        //        // Prevent Dropdown from closing with this. If it's a dropdown item.
        //        if (e?.Target.ClassList.Any(q => q.Value == "dropdown-item") == true)
        //            return;
        //        // If this dropdown toggle return
        //        if (e?.Target.ClassList.Any(q => q.Value == "dropdown-toggle") == true &&
        //            e.Target.TargetId == DataId) return;

        //        // If click element is inside this dropdown return
        //        // if (e?.Target.ChildrenId?.Any(q => q == DataId) == true && AllowItemClick) return;
        //        // If is Manual Return
        //        if (IsManual) return;
        //        await HideAsync();
        //    }
        //}

        /// <summary>
        /// Show the dropdown.
        /// </summary>
        /// <returns>Completed task when the dropdown is shown.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        

        /// <summary>
        /// Toggles dropdown open or closed.yuo
        /// </summary>
        /// <returns>Completed task once dropdown is open or closed.</returns>
        public Task ToggleAsync()
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
                if(!IsManual && !Demo && !AllowOutsideClick)
                    await BlazorStrapService.JavaScriptInterop.AddDocumentEventAsync(EventType.Click, DataRefId);
                _secondRender = true;
                _objectRef = DotNetObjectReference.Create<BSDropdownBase>(this);
                BlazorStrapService.OnEventForward += InteropEventCallback;
            }
            
        }

        public override async Task OnEventAsync(string sender, string target, EventType type, object? data)
        {
            if (Demo) return;
            if (IsManual) return;

            if (type == EventType.Click && target.Contains(DataRefId) && sender == "jsdocument" )
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
            _objectRef?.Dispose();
            BlazorStrapService.OnEventForward -= InteropEventCallback;
            GC.SuppressFinalize(this);
        }
    }
}
