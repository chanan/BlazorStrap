using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSNavbar : BlazorStrapBase
    {
        /// <summary>
        /// Removes the css <c>nav</c> class.
        /// </summary>
        [Parameter] public bool NoNavbarClass { get; set; }

        /// <summary>
        /// Sets navbar color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// See <see href="https://getbootstrap.com/docs/5.2/components/navbar/#how-it-works">Bootstrap Documentation</see>
        /// </summary>
        [Parameter] public Size Expand { get; set; } = Size.Medium;

        /// <summary>
        /// Sets the navbar to be dark.
        /// </summary>
        [Parameter] public bool IsDark { get; set; }

        /// <summary>
        /// Adds the <c>fixed-bottom</c> class to the nav bar.
        /// </summary>
        [Parameter] public bool IsFixedBottom { get; set; }

        /// <summary>
        /// Adds the <c>fixed-top</c> class to the nav bar.
        /// </summary>
        [Parameter] public bool IsFixedTop { get; set; }

        /// <summary>
        /// Uses the HTML &lt;Header&gt; element instead of the &lt;Nav&gt; element.
        /// </summary>
        [Parameter] public bool IsHeader { get; set; }

        /// <summary>
        /// Sets the navbar to be top sticky.
        /// </summary>
        [Parameter] public bool IsStickyTop { get; set; }

        private string? ClassBuilder => new CssBuilder()
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