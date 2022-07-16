using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Layout;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V4
{
    public partial class BSRow : BSRowBase
    {
        /// <summary>
        /// Justify
        /// </summary>
        [Parameter] public Justify Justify { get; set; }

        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder("row")
            .AddClass($"mx-n{Gutters.ToIndex()} mx-n{Gutters.ToIndex()}", Gutters != Gutters.Default)
            .AddClass($"mx-n{HorizontalGutters.ToIndex()}", HorizontalGutters != Gutters.Default)
            .AddClass($"my-n{VerticalGutters.ToIndex()}", VerticalGutters != Gutters.Default)
            .AddClass($"justify-content-{Justify.NameToLower()}", Justify != Justify.Default)
            .AddClass($"align-items-{Align.NameToLower()}", Align != Align.Default)
            .AddClass($"row-cols-{RowColumns}", RowColumns > 0)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}
