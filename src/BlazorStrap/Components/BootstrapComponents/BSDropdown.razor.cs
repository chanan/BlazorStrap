using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSDropdown : BlazorStrapBase, IAsyncDisposable
    {
        [Parameter] public bool AllowItemClick { get; set; }
        [Parameter] public bool AllowOutsideClick { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public bool Demo { get; set; }
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsDiv { get; set; }
        [Parameter] public bool IsManual { get; set; }
        [Parameter] public bool IsNavPopper { get; set; }
        [Parameter] public bool IsStatic { get; set; }
        [Parameter] public string? Offset { get; set; }
        [Parameter] public Placement Placement { get; set; } = Placement.RightStart;
        [Parameter] public string? ShownAttribute { get; set; }
        [Parameter] public string Target { get; set; } = Guid.NewGuid().ToString();
        [Parameter] public RenderFragment? Toggler { get; set; }
        private bool _lastIsNavPopper;
        private DotNetObjectReference<BSDropdown> _objectRef;
        [CascadingParameter] public BSNavItem? DropdownItem { get; set; }
        [CascadingParameter] public BSButtonGroup? Group { get; set; }
        [CascadingParameter] public BSNavItem? NavItem { get; set; }
        [CascadingParameter] public BSDropdown? Parent { get; set; }
        internal bool Active { get; private set; }
        internal int ChildCount { get; set; }

        private string? ClassBuilder => new CssBuilder("dropdown-menu")
            .AddClass("dropdown-menu-dark", IsDark)
            .AddClass("show", Shown)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string DataRefId => (PopoverRef != null) ? PopoverRef.DataId : DataId;

        private string? GroupClassBuilder => new CssBuilder()
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string? IsDivClassBuilder => new CssBuilder()
            .AddClass("dropdown", Parent == null)
            .AddClass("dropup", Placement is Placement.Top or Placement.TopEnd or Placement.TopStart)
            .AddClass("dropstart", Placement is Placement.Left or Placement.LeftEnd or Placement.LeftStart)
            .AddClass("dropend", Placement is Placement.Right or Placement.RightEnd or Placement.RightStart)
            .Build().ToNullString();

        private ElementReference MyRef { get; set; }
        internal Action<bool, BSDropdownItem>? OnSetActive { get; set; }
        private BSPopover? PopoverRef { get; set; }
        internal bool Shown { get; private set; }

        // ReSharper disable once MemberCanBePrivate.Global
        public async Task HideAsync()
        {
            Shown = false;
            await BlazorStrap.Interop.RemoveDocumentEventAsync(this, DataRefId, EventType.Click);

            if ((Group != null && PopoverRef != null && !IsStatic) || (IsDiv || Parent != null || IsNavPopper))
            {
                if (PopoverRef != null)
                    await PopoverRef.HideAsync();
            }

            if (!string.IsNullOrEmpty(ShownAttribute))
            {
                await BlazorStrap.Interop.RemoveAttributeAsync(MyRef, ShownAttribute);
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

        [JSInvokable]
        public override async Task InteropEventCallback(string id, CallerName name, EventType type,
            Dictionary<string, string>? classList, JavascriptEvent? e)
        {
            // The if statement was getting hard to read so split into parts 
            if (id == DataRefId && name.Equals(this) && type == EventType.Click)
            {
                // If this dropdown toggle return
                if (e?.Target.ClassList.Any(q => q.Value == "dropdown-toggle") == true &&
                    e.Target.TargetId == DataId) return;

                // If click element is inside this dropdown return
                // if (e?.Target.ChildrenId?.Any(q => q == DataId) == true && AllowItemClick) return;
                // If is Manual Return
                if (IsManual) return;
                await HideAsync();
            }
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public async Task ShowAsync()
        {
            Shown = true;

            if (!AllowOutsideClick)
            {
                await BlazorStrap.Interop.AddDocumentEventAsync(_objectRef, DataRefId, EventType.Click, AllowItemClick);
            }
            
            if ((Group != null && PopoverRef != null && !IsStatic) || (IsDiv || Parent != null || IsNavPopper))
            {
                if (PopoverRef != null) await PopoverRef.ShowAsync();
            }

            if (!string.IsNullOrEmpty(ShownAttribute))
            {
                await BlazorStrap.Interop.AddAttributeAsync(MyRef, ShownAttribute, "blazorStrap");
            }

            await InvokeAsync(StateHasChanged);
        }

        public Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        protected override void OnInitialized()
        {
            _lastIsNavPopper = IsNavPopper;
            _objectRef = DotNetObjectReference.Create<BSDropdown>(this);
            BlazorStrap.OnEventForward += InteropEventCallback;
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

        internal void SetActive(bool active, BSDropdownItem item)
        {
            OnSetActive?.Invoke(active, item);
        }

        public async ValueTask DisposeAsync()
        {
            _objectRef.Dispose();
            await BlazorStrap.Interop.RemoveDocumentEventAsync(this, DataRefId, EventType.Click);
            BlazorStrap.OnEventForward -= InteropEventCallback;
            GC.SuppressFinalize(this);
        }
    }
}