using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSDropdown : BlazorStrapBase, IAsyncDisposable
    {
        internal Action<bool, BSDropdownItem>? OnSetActive { get; set; }
        [Parameter] public bool AllowItemClick { get; set; }
        [Parameter] public bool AllowOutsideClick { get; set; }
        [Parameter] public RenderFragment? Content { get; set; }
        [Parameter] public bool Demo { get; set; }
        [Parameter] public bool IsDiv { get; set; }
        [Parameter] public bool IsDark { get; set; }
        [Parameter] public bool IsManual { get; set; }
        [Parameter] public bool IsStatic { get; set; }
        [Parameter] public bool IsCssHover { get; set; }
        [Parameter] public bool IsNavPopper { get; set; }
        [Parameter] public string? Offset { get; set; }
        [Parameter] public string? ShownAttribute { get; set; }
        [Parameter] public string Target { get; set; } = Guid.NewGuid().ToString();
        [Parameter] public RenderFragment? Toggler { get; set; }
        [CascadingParameter] public BSButtonGroup? Group { get; set; }
        [CascadingParameter] public BSNavItem? NavItem { get; set; }
        [CascadingParameter] public BSNavItem? DropdownItem { get; set; }
        [CascadingParameter] public BSDropdown? Parent { get; set; }
        [Parameter] public Placement Placement { get; set; } = Placement.RightStart;
        internal bool Active { get; private set; }
        internal int ChildCount { get; set; }

        private string? IsDivClassBuilder => new CssBuilder()
            .AddClass("dropdown", Parent == null)
            .AddClass("dropup", Placement is Placement.Top or Placement.TopEnd or Placement.TopStart)
            .AddClass("dropstart", Placement is Placement.Left or Placement.LeftEnd or Placement.LeftStart)
            .AddClass("dropend", Placement is Placement.Right or Placement.RightEnd or Placement.RightStart)
            .Build().ToNullString();

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

        private ElementReference MyRef { get; set; }
        private BSPopover? PopoverRef { get; set; }
        internal bool Shown { get; private set; }

        // ReSharper disable once MemberCanBePrivate.Global
        public async Task HideAsync()
        {
            Shown = false;
            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataRefId, "documentDropdown", "click", true);
            if (IsCssHover)
            {
                await InvokeAsync(StateHasChanged);
                return;
            }

            if (!AllowOutsideClick)
            {
                if ((Group != null && PopoverRef != null && !IsStatic) || (IsDiv || Parent != null || IsNavPopper))
                {
                    if (PopoverRef != null)
                        await PopoverRef.HideAsync();
                }
            }

            if (!string.IsNullOrEmpty(ShownAttribute))
            {
                await Js.InvokeVoidAsync("blazorStrap.RemoveAttribute", MyRef, ShownAttribute);
            }

            await InvokeAsync(StateHasChanged);
        }
        // ReSharper disable once MemberCanBePrivate.Global
        public async Task ShowAsync()
        {
            Shown = true;

            if (!AllowOutsideClick)
            {
                await Js.InvokeVoidAsync("blazorStrap.AddEvent", DataRefId, "documentDropdown", "click", true,
                    AllowItemClick);
            }

            if (IsCssHover)
            {
                await InvokeAsync(StateHasChanged);
                return;
            }


            if ((Group != null && PopoverRef != null && !IsStatic) || (IsDiv || Parent != null || IsNavPopper))
            {
                if (PopoverRef != null) await PopoverRef.ShowAsync();
            }

            if (!string.IsNullOrEmpty(ShownAttribute))
            {
                await Js.InvokeVoidAsync("blazorStrap.AddAttribute", MyRef, ShownAttribute, "blazorStrap");
            }

            await InvokeAsync(StateHasChanged);
        }

        public Task ToggleAsync()
        {
            return Shown ? HideAsync() : ShowAsync();
        }

        internal void SetActive(bool active, BSDropdownItem item)
        {
            OnSetActive?.Invoke(active, item);
        }

        protected override void OnInitialized()
        {
            JSCallback.EventHandler += OnEventHandler;
        }

        private async void OnEventHandler(string id, string name, string type, Dictionary<string, string>? classList,
            JavascriptEvent? e)
        {
            if (id == DataId && name == "clickforward" && type == "click")
            {
                await ToggleAsync();
            }

            // The if statement was getting hard to read so split into parts 
            if (id == DataRefId && name == "documentDropdown" && type == "click")
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


        public async ValueTask DisposeAsync()
        {
            await Js.InvokeVoidAsync("blazorStrap.RemoveEvent", DataRefId, "documentDropdown", "click", true);
            JSCallback.EventHandler -= OnEventHandler;

            GC.SuppressFinalize(this);
        }
    }
}