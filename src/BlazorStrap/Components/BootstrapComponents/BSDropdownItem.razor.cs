using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSDropdownItem : BlazorStrapBase
    {
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDivider { get; set; }
        [Parameter] public int Header { get; set; } 
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsButton { get; set; }
        [Parameter] public bool IsSubmenu { get; set; }
        [Parameter] public bool IsText { get; set; }
        [Parameter] public string? SubmenuClass { get; set; } = "dropdown-submenu";
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [CascadingParameter] public BSDropdown? Parent { get; set; }
        [Parameter] public bool PreventDefault { get; set; }
        [Parameter] public string? Url { get; set; } = "javascript:void(0)";

        private string? ClassBuilder => new CssBuilder()
            .AddClass("dropdown-item-text", IsText)
            .AddClass("dropdown-item", !IsText && !IsSubmenu)
            .AddClass("dropdown-header", Header != 0)
            .AddClass("active", IsActive)
            .AddClass(SubmenuClass, IsSubmenu)
            .AddClass("disabled", IsDisabled)
            .AddClass("dropup", IsSubmenu && Parent?.Placement is Placement.Top or Placement.TopEnd or Placement.TopStart)
            .AddClass("dropstart", IsSubmenu && Parent?.Placement is Placement.Left or Placement.LeftEnd or Placement.LeftStart)
            .AddClass("dropend", IsSubmenu && Parent?.Placement is Placement.Right or Placement.RightEnd or Placement.RightStart)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
 
        private async Task ClickEvent()
        {
            if (Parent is { AllowOutsideClick: true, AllowItemClick: false })
            {
                await Parent.ToggleAsync();
            }
            if (OnClick.HasDelegate)
                await OnClick.InvokeAsync();
        }
    }
}