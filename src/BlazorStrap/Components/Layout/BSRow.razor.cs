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

        internal string? ClassBuilder => new CssBuilder("row")
            .AddClass($"g-{HorizontalGutters.ToIndex()}", HorizontalGutters != Gutters.Default)
            .AddClass($"gx-{VerticalGutters.ToIndex()}", VerticalGutters != Gutters.Default)
            .AddClass($"gy-{Gutters.ToIndex()}", Gutters != Gutters.Default)
            .AddClass($"justify-content-{Justify.GetName<Justify>(Justify).ToLower()}", Justify != Justify.Default)
            .AddClass($"align-items-{Align.GetName<Align>(Align).ToLower()}", Align != Align.Default)
            .AddClass(LayoutClass, !String.IsNullOrEmpty(LayoutClass))
            .Build().ToNullString();
    }
}
