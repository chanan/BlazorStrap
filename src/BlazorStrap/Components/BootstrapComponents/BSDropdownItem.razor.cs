using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSDropdownItem : BlazorStrapBase
    {
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDivider { get; set; }
        [Parameter] public int Header { get; set; } = 0;
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsButton { get; set; }
        [Parameter] public bool IsText { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [CascadingParameter] public BSDropdown? Parent { get; set; }
        [CascadingParameter] public BSPopover? Popper { get; set; }
        [Parameter] public bool PreventDefault { get; set; }
        [Parameter] public string? Url { get; set; } = "javascript:void(0)";

        private string? ClassBuilder => new CssBuilder()
            .AddClass("dropdown-item-text", IsText)
            .AddClass("dropdown-item", !IsText)
            .AddClass("dropdown-header", Header != 0)
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
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