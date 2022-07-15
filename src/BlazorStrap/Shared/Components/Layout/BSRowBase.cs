using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Layout
{
    public abstract class BSRowBase : BlazorStrapBase
    {
        /// <summary>
        /// Align
        /// </summary>
        [Parameter] public Align Align { get; set; }

        /// <summary>
        /// Align
        /// </summary>
        [Parameter] public int RowColumns { get; set; }

        /// <summary>
        /// Gutters
        /// </summary>
        [Parameter] public Gutters Gutters { get; set; }

        [Parameter] public Gutters HorizontalGutters { get; set; }

        /// <summary>
        /// Vertical Gutters
        /// </summary>
        [Parameter] public Gutters VerticalGutters { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}