using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Layout;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5_1
{
    public partial class BSRow : BSRowBase
    {
        /// <summary>
        /// Justify
        /// </summary>
        [Parameter] public Justify Justify { get; set; }

        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("row")
            .AddClass($"g-{Gutters.ToIndex()}", Gutters != Gutters.Default)
            .AddClass($"gx-{HorizontalGutters.ToIndex()}", HorizontalGutters != Gutters.Default)
            .AddClass($"gy-{VerticalGutters.ToIndex()}", VerticalGutters != Gutters.Default)
            .AddClass($"justify-content-{Justify.NameToLower()}", Justify != Justify.Default)
            .AddClass($"align-items-{Align.NameToLower()}", Align != Align.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass($"row-cols-{RowColumns}", RowColumns > 0)
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
