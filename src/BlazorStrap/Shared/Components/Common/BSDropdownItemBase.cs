using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSDropdownItemBase : BlazorStrapBase, IDisposable
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

        [CascadingParameter] public BSDropdownBase? Parent { get; set; }

        [CascadingParameter] public BSDropdownItemBase? DropdownItem { get; set; }

        /// <summary>
        /// Prevents the default onclick behavior.
        /// </summary>
        [Parameter] public bool PreventDefault { get; set; }

        /// <summary>
        /// Url for dropdown. Defaults to <c>javascript:void(0)</c>
        /// </summary>
        [Parameter] public string? Url { get; set; } = "javascript:void(0)";

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        private bool _canHandleActive;

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


        protected async Task ClickEvent()
        {
            if (Parent is { AllowItemClick: false })
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
