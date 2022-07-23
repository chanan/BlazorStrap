using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V4
{
    public partial class BSNav : BSNavBase
    {
        /// <summary>
        /// Alignment of content.
        /// </summary>
        [Parameter] public Justify Justify { get; set; }
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
            .AddClass("nav", Navbar == null && !NoNav)
            .AddClass("navbar-nav", Navbar != null && !NoNavbarNav)
            .AddClass("nav-tabs", IsTabs)
            .AddClass("nav-pills", IsPills)
            .AddClass("flex-column", IsVertical)
            .AddClass("nav-fill", IsFill)
            .AddClass("nav-justified", IsJustified)
            .AddClass($"justify-content-{Justify.NameToLower()}", Justify != Justify.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}