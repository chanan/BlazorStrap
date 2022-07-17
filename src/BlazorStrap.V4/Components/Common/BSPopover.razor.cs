using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V4
{
    public partial class BSPopover : BSPopoverBase
    {
        /// <summary>
        /// Alignment of content.
        /// </summary>
        [Parameter] public Justify Justify { get; set; }
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("popover", !IsDropdown)
                .AddClass("fade", !IsDropdown)
                .AddClass("dropdown-menu-end", Placement == Placement.BottomEnd && IsDropdown)
                .AddClass($"bs-popover-{Placement.NameToLower().PurgeStartEnd().LeftRightToStartEnd()}", !IsDropdown)
                .AddClass("dropdown-menu", IsDropdown)
                .AddClass($"show", Shown)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        protected override string? HeaderClass => new CssBuilder("popover-header")
                .AddClass($"bg-{HeaderColor.NameToLower()}", HeaderColor != BSColor.Default)
                .Build().ToNullString();
    }
}