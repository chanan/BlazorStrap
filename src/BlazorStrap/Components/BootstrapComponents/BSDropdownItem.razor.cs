using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSDropdownItem : BlazorStrapBase, IDisposable
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        /// <summary>
        /// Display the Dropdown item as active.
        /// </summary>
        [Parameter] public bool? IsActive { get; set; }

        /// <summary>
        /// Renders the DropdownItem as a horizontal divider. <see cref="ChildContent"/> is ignored.
        /// </summary>
        [Parameter] public bool IsDivider { get; set; }

        /// <summary>
        /// Renders Dropdown item as a header and sets the size. Valid values are 1-6
        /// </summary>
        [Parameter] public int Header { get; set; }

        /// <summary>
        /// Sets the dropdown item as disabled.
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// Renders as HTML Button element.
        /// </summary>
        [Parameter] public bool IsButton { get; set; }

        /// <summary>
        /// Outputs correctly for a nested Dropdown
        /// </summary>
        [Parameter] public bool IsSubmenu { get; set; }

        /// <summary>
        /// Adds the dropdown-item-text css class.
        /// </summary>
        [Parameter] public bool IsText { get; set; }

        /// <summary>
        /// Submenu css class. Defaults to dropdown-submenu.
        /// </summary>
        [Parameter] public string? SubmenuClass { get; set; } = "dropdown-submenu";

        /// <summary>
        /// Event called when dropdown item is clicked.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [CascadingParameter] public BSDropdown? Parent { get; set; }

        [CascadingParameter] public BSDropdownItem? DropdownItem { get; set; }

        /// <summary>
        /// Prevents the default onclick behavior.
        /// </summary>
        [Parameter] public bool PreventDefault { get; set; }

        /// <summary>
        /// Url for dropdown. Defaults to <c>javascript:void(0)</c>
        /// </summary>
        [Parameter] public string? Url { get; set; } = "javascript:void(0)";

        private bool _canHandleActive;
        private string? ClassBuilder => new CssBuilder()
            .AddClass("dropdown-item-text", IsText)
            .AddClass("dropdown-item", !IsText && !IsSubmenu)
            .AddClass("dropdown-header", Header != 0)
            .AddClass("active", IsActive ?? false)
            .AddClass(SubmenuClass, IsSubmenu)
            .AddClass("disabled", IsDisabled)
            .AddClass("dropup", IsSubmenu && Parent?.Placement is Placement.Top or Placement.TopEnd or Placement.TopStart)
            .AddClass("dropstart", IsSubmenu && Parent?.Placement is Placement.Left or Placement.LeftEnd or Placement.LeftStart)
            .AddClass("dropend", IsSubmenu && Parent?.Placement is Placement.Right or Placement.RightEnd or Placement.RightStart)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        protected override void OnInitialized()
        {
            if (IsActive == null)
            {
                _canHandleActive = true;
                if (NavigationManager.Uri == NavigationManager.BaseUri + Url?.TrimStart('/'))
                {
                    IsActive = true;
                    IsActive = true;
                    Parent?.SetActive(true, this);
                }
                NavigationManager.LocationChanged += OnLocationChanged;
            }
        }
        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            if (!_canHandleActive) return;
            IsActive = false;
            if (NavigationManager.Uri == NavigationManager.BaseUri + Url?.TrimStart('/'))
            {
                IsActive = true;
                IsActive = true;
                Parent?.SetActive(true, this);
            }
            else
            {
                Parent?.SetActive(false, this);
                IsActive = false;
            }
        }


        private async Task ClickEvent()
        {
            if (Parent is { AllowOutsideClick: true, AllowItemClick: false })
            {
                await Parent.ToggleAsync();
            }
            if (OnClick.HasDelegate)
                await OnClick.InvokeAsync();
        }

        public void Dispose()
        {
            if (_canHandleActive)
                NavigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}