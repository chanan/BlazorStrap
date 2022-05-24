using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSTable : BlazorStrapBase
    {
        /// <summary>
        /// Table background color.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Adds the <c>table-dark</c> class.
        /// </summary>
        [Parameter] public bool IsDark { get; set; }

        /// <summary>
        /// Adds the <c>table-hover</c> class.
        /// </summary>
        [Parameter] public bool IsHoverable { get; set; }

        /// <summary>
        /// Adds the <c>table-responsive</c> class.
        /// </summary>
        [Parameter] public bool IsResponsive { get; set; }

        /// <summary>
        /// Additional classes to add to the table wrapper.
        /// </summary>
        [Parameter] public string? ResponsiveWrapperClass { get; set; }

        /// <summary>
        /// Responsive table size. See <see href="https://getbootstrap.com/docs/5.2/content/tables/#responsive-tables">Bootstrap Documentation</see>.
        /// </summary>
        [Parameter] public Size ResponsiveSize { get; set; } = Size.None;

        /// <summary>
        /// Adds the <c>table-sm</c> class.
        /// </summary>
        [Parameter] public bool IsSmall { get; set; }

        /// <summary>
        /// Adds the <c>table-bordered</c> class.
        /// </summary>
        [Parameter] public bool IsBordered { get; set; }

        /// <summary>
        /// Adds the <c>table-borderless</c> class.
        /// </summary>
        [Parameter] public bool IsBorderLess { get; set; }

        /// <summary>
        /// Adds the <c>caption-top</c> class.
        /// </summary>
        [Parameter] public bool IsCaptionTop { get; set; }

        /// <summary>
        /// Adds the <c>table-striped</c> class.
        /// </summary>
        [Parameter] public bool IsStriped { get; set; }

        internal string? ClassBuilder => new CssBuilder("table")
         .AddClass("table-striped", IsStriped)
         .AddClass("table-dark", IsDark)
         .AddClass("table-hover", IsHoverable)
         .AddClass("table-sm", IsSmall)
         .AddClass("table-bordered", IsBordered)
         .AddClass("table-borderless", IsBorderLess)
         .AddClass($"table-{Color.NameToLower()}", Color != BSColor.Default)
         .AddClass("caption-top", IsCaptionTop)
         .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
         .AddClass(Class, !string.IsNullOrEmpty(Class))
         .Build().ToNullString();

        internal string? WrapperClassBuilder => new CssBuilder("bs-table-responsive")
            .AddClass("table-responsive", ResponsiveSize == Size.None)
            .AddClass($"table-responsive-{ResponsiveSize.ToDescriptionString()}", ResponsiveSize != Size.None)
            .AddClass(ResponsiveWrapperClass, !string.IsNullOrEmpty(ResponsiveWrapperClass))
            .Build().ToNullString();

    }
}
