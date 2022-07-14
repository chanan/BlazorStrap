using BlazorStrap.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Shared.Components.Static.Base
{
    public abstract class BSToggleBase : BlazorStrapBase, IDisposable
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
        /// Event called when toggle is clicked.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [CascadingParameter] public IBSCollapse? CollapseParent { get; set; }
        [CascadingParameter] public IBSDropdown? DropDownParent { get; set; }
        private ElementReference MyRef { get; set; }
        private string Element =>
            (CollapseParent != null) ? "collapse" : (DropDownParent != null) ? "dropdown" : "unknown";
        private bool _canHandleActive;
        private IBSDropdownItem? _activeOwner;

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "<Pending>")]
        private string Target
        {
            get
            {
                if (CollapseParent != null) return CollapseParent.DataId;
                return DropDownParent != null ? DropDownParent.DataId : "";
            }
        }


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

        private void OnSetActive(bool active, IBSDropdownItem item)
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
        protected async Task ClickEvent()
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
        protected bool Show()
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
