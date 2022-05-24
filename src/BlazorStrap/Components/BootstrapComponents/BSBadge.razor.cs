using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSBadge : BlazorStrapBase
    {
        /// <summary>
        /// Color class of badge
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Whether or not the badge is styled as a pill.
        /// </summary>
        [Parameter] public bool IsPill { get; set; }

        private string? ClassBuilder => new CssBuilder("badge")
            .AddClass("rounded-pill", IsPill)
            .AddClass($"bg-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
