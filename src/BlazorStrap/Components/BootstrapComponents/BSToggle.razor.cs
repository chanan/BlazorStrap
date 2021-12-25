using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSToggle : BlazorStrapBase
    {
        [Parameter] public bool IsButton { get; set; }
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsNavLink { get; set; }
        [Parameter] public bool IsOutlined { get; set; }
        [Parameter] public bool IsSplitButton { get; set; }
        [Parameter] public Size Size { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [CascadingParameter] public BSCollapse? CollapseParent { get; set; }
        [CascadingParameter] public BSDropdown? DropDownParent { get; set; }

        public string Target
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
            .AddClass("active", DropDownParent?.Active ?? false && IsNavLink)
            .AddClass("btn", IsButton)
            .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass(LayoutClass, !String.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !String.IsNullOrEmpty(Class))
            .AddClass("nav-link", !IsButton && IsNavLink)
            .AddClass("dropdown-item", DropDownParent?.Parent != null )
            .AddClass("dropdown-toggle", DropDownParent != null)
            .AddClass("dropdown-toggle-split", DropDownParent != null && IsSplitButton)
            .AddClass("collapsed", (!CollapseParent?.Shown ?? false) && DropDownParent == null)
            .Build().ToNullString();

        protected override void OnInitialized()
        {
            if (DropDownParent != null)
            {
                if (DropDownParent.Group != null)
                {
                    DataId = DropDownParent.Target;
                }
            }
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
                return DropDownParent.Shown;
            else
                return CollapseParent?.Shown ?? false;
        }
    }
}
