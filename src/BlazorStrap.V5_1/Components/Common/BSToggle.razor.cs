using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5_1
{
    public partial class BSToggle : BSToggleBase
    {
        /// <summary>
        /// Size of toggle.
        /// </summary>
        [Parameter] public Size Size { get; set; }

        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
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
    }
}