using BlazorStrap.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Content
{
    public abstract class BSTableBase : BlazorStrapBase
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
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected abstract string? WrapperClassBuilder { get; }
    }
}
