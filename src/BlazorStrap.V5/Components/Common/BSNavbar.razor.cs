using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5
{
    public partial class BSNavbar : BSNavbarBase
    {
        /// <summary>
        /// See <see href="https://getbootstrap.com/docs/5.2/components/navbar/#how-it-works">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public Size Expand { get; set; } = Size.Medium;

        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("navbar", !NoNavbarClass)
                .AddClass("navbar-light", !IsDark)
                .AddClass("navbar-dark", IsDark)
                .AddClass("fixed-top", IsFixedTop)
                .AddClass("fixed-bottom", IsFixedBottom)
                .AddClass("sticky-top", IsStickyTop)
                .AddClass($"navbar-expand-{Expand.ToDescriptionString().ToLower()}", Expand != Size.None)
                .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}