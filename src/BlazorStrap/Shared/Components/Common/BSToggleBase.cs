using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Shared.Components.Common
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
        [Parameter] public bool NoDropdownToggleClass { get; set; }

        /// <summary>
        /// Event called when toggle is clicked.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [CascadingParameter] public BSCollapseBase? CollapseParent { get; set; }
        [CascadingParameter] public BSDropdownBase? DropDownParent { get; set; }
        protected ElementReference MyRef { get; set; }
        protected string Element =>
            CollapseParent != null ? "collapse" : DropDownParent != null ? "dropdown" : "unknown";
        private bool _canHandleActive;
        private BSDropdownItemBase? _activeOwner;

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "<Pending>")]
        protected string Target
        {
            get
            {
                if(DropDownParent != null) return DropDownParent.DataId;
                return (CollapseParent != null) ? CollapseParent.DataId : "";
            }
        }


        protected override void OnInitialized()
        {
            if (IsActive == null)
                _canHandleActive = true;
            if (DropDownParent == null) return;
            DropDownParent.OnSetActive += OnSetActive;
            if (DropDownParent is not null)
                DataId = DropDownParent.Target;
        }

        private void OnSetActive(bool active, BSDropdownItemBase item)
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
             //   await BlazorStrapService.Interop.AddAttributeAsync(MyRef, "aria-expanded", (!Show()).ToString().ToLower());
                await DropDownParent.ToggleAsync();
            }
            else if (CollapseParent != null)
            {
              //  await BlazorStrapService.Interop.AddAttributeAsync(MyRef, "aria-expanded", (!Show()).ToString().ToLower());
                await CollapseParent.ToggleAsync();
            }
            else
                await OnClick.InvokeAsync();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "<Pending>")]
        protected bool Show()
        {
            if (DropDownParent != null)
                return DropDownParent.Shown;

            if (CollapseParent != null)
                return CollapseParent.Shown;
            return false;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                if(DropDownParent is not null)
                if (DropDownParent.IsMouseover)
                {
                    await BlazorStrapService.JavaScriptInterop.AddEventAsync(DataId, DropDownParent.DataId, EventType.Mouseenter);
                    await BlazorStrapService.JavaScriptInterop.AddEventAsync(DataId, DropDownParent.DataId, EventType.Mouseleave);
                }
            }
        }
        public void Dispose()
        {
            if (DropDownParent != null)
                DropDownParent.OnSetActive -= OnSetActive;
        }
    }
}
