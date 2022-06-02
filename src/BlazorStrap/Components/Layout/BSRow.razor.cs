using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSRow : BlazorStrapBase
    {
        /// <summary>
        /// Align
        /// </summary>
        [Parameter] public Align Align { get; set; }

        /// <summary>
        /// Gutters
        /// </summary>
        [Parameter] public Gutters Gutters { get; set; }


        [Parameter] public Gutters HorizontalGutters { get; set; }

        /// <summary>
        /// Justify
        /// </summary>
        [Parameter] public Justify Justify { get; set; }

        /// <summary>
        /// Vertical Gutters
        /// </summary>
        [Parameter] public Gutters VerticalGutters { get; set; }

        private string? ClassBuilder => new CssBuilder("row")
            .AddClass($"g-{Gutters.ToIndex()}", Gutters != Gutters.Default)
            .AddClass($"gx-{HorizontalGutters.ToIndex()}", HorizontalGutters != Gutters.Default)
            .AddClass($"gy-{VerticalGutters.ToIndex()}", VerticalGutters != Gutters.Default)
            .AddClass($"justify-content-{Justify.NameToLower()}", Justify != Justify.Default)
            .AddClass($"align-items-{Align.NameToLower()}", Align != Align.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
