using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.V5_1.Enums;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSToggle : LayoutBase, IDisposable
    {
        /// <summary>
        /// Renders as a HTML Button element.
        /// </summary>
        [Parameter] public bool IsButton { get; set; }

        /// <summary>
        /// Color of Toggle
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Use when in a nav bar.
        /// </summary>
        [Parameter] public bool IsNavLink { get; set; }

        /// <summary>
        /// Whether or not the toggle is active.
        /// </summary>
        [Parameter] public bool? IsActive { get; set; }

        /// <summary>
        /// Button rendered as an outline.
        /// </summary>
        [Parameter] public bool IsOutlined { get; set; }

        /// <summary>
        /// Dropdown arrow is separate from main button.
        /// </summary>
        [Parameter] public bool IsSplitButton { get; set; }

        /// <summary>
        /// Size of toggle.
        /// </summary>
        [Parameter] public Size Size { get; set; }

        /// <summary>
        /// Event called when toggle is clicked.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [CascadingParameter] public BSCollapse? CollapseParent { get; set; }
        [CascadingParameter] public BSDropdown? DropDownParent { get; set; }
        private ElementReference MyRef { get; set; }
        private string Element =>
            (CollapseParent != null) ? "collapse" : (DropDownParent != null) ? "dropdown" : "unknown";
        private bool _canHandleActive;
        private BSDropdownItem? _activeOwner;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "<Pending>")]
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
            .AddClass("dropdown-item", DropDownParent?.Parent != null && !IsNavLink)
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
                if (IsActive == active) return;

                IsActive = false;
            }
            if (active)
            {
                _activeOwner = item;
                if (IsActive == active) return;
                IsActive = true;
            }
            StateHasChanged();
        }
        //Event can't be access until it's rendered no callback needed.
        private async Task ClickEvent()
        {
            if (DropDownParent != null)
            {
                await BlazorStrapService.Interop.AddAttributeAsync(MyRef, "aria-expanded", (!Show()).ToString().ToLower());
                await DropDownParent.ToggleAsync();
            }
            else if (CollapseParent != null)
            {
                await BlazorStrapService.Interop.AddAttributeAsync(MyRef, "aria-expanded", (!Show()).ToString().ToLower());
                await CollapseParent.ToggleAsync();
            }
            else
            {
                await OnClick.InvokeAsync();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "<Pending>")]
        private bool Show()
        {
            if (DropDownParent != null)
            {
                return DropDownParent.Shown;
            }

            if (CollapseParent != null)
            {
                return CollapseParent.Shown;

            }
            return false;
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
