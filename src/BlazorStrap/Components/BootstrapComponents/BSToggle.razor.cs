using BlazorComponentUtilities;
using BlazorStrap.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorStrap
{
    public partial class BSToggle : BlazorStrapBase, IDisposable
    {
        [Parameter] public bool IsButton { get; set; }
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsNavLink { get; set; }
        [Parameter] public bool? IsActive { get; set; }
        [Parameter] public bool IsOutlined { get; set; }
        [Parameter] public bool IsSplitButton { get; set; }
        [Parameter] public Size Size { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [CascadingParameter] public BSCollapse? CollapseParent { get; set; }
        [CascadingParameter] public BSDropdown? DropDownParent { get; set; }
        private bool _canHandleActive;
        private bool _childSetActive;
        private BSDropdownItem? _activeOwner;
        private string Target
        {
            get
            {
                if (CollapseParent != null) return CollapseParent.DataId;
                return DropDownParent != null ? DropDownParent.DataId : "";
            }
        }

        private string? ClassBuilder => new CssBuilder()
            .AddClass($"btn-outline-{Color.NameToLower()}", IsOutlined && IsButton)
            .AddClass($"btn-{Color.NameToLower()}", Color != BSColor.Default && !IsOutlined && IsButton)
            .AddClass("active", (DropDownParent?.Active ?? false) && IsNavLink)
            .AddClass("btn", IsButton)
            .AddClass("active", IsActive ?? false)
            .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .AddClass("nav-link", !IsButton && IsNavLink)
            .AddClass("dropdown-item", DropDownParent?.Parent != null && !IsNavLink )
            .AddClass("dropdown-toggle", DropDownParent != null)
            .AddClass("dropdown-toggle-split", DropDownParent != null && IsSplitButton)
            .AddClass("collapsed", (!CollapseParent?.Shown ?? false) && DropDownParent == null)
            .Build().ToNullString();

        
        protected override void OnInitialized()
        {
            if (IsActive == null)
                _canHandleActive = true;
            if (DropDownParent == null) return;
            DropDownParent.OnSetActive += OnSetActive;
            if (DropDownParent.Group != null || DropDownParent.IsDiv || DropDownParent.IsNavPopper || DropDownParent.Parent != null)
            {
                DataId = DropDownParent.Target;
            }
        }

        private void OnSetActive(bool active, BSDropdownItem item)
        {
            if (!_canHandleActive) return;
            if (_activeOwner == item && !active)
            {
                if(IsActive == active) return;
                
                IsActive = false;
            }
            if (active)
            {
                _activeOwner = item;
                if(IsActive == active) return;
                IsActive = true;
            }
            StateHasChanged();
        }

        private async Task ClickEvent()
        {
            if (DropDownParent != null)
            {
                await DropDownParent.ToggleAsync();
            }
            else if (CollapseParent != null)
            {
                await CollapseParent.ToggleAsync();
            }
            else
            {
                await OnClick.InvokeAsync();
                await InvokeAsync(StateHasChanged);
            }
        }

        private bool Show()
        {
            if (DropDownParent != null)
            {
                return DropDownParent.Shown;
            }
            else
                return CollapseParent?.Shown ?? false;
        }

        public void Dispose()
        {
            if (DropDownParent != null)
            {
                DropDownParent.OnSetActive -= OnSetActive;
            }
        }
    }
}
