using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap
{
    public class BSRow : BlazorStrapBase
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
        /// Justify
        /// </summary>
        [Parameter] public Justify Justify { get; set; }

        /// <summary>
        /// Vertical Gutters
        /// </summary>
        [Parameter] public Gutters VerticalGutters { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (BlazorStrap.BootstrapVersion == BootstrapVersion.Bootstrap4)
                Version4RenderBuilder(builder);
            else
                Version5RenderBuilder(builder);
        }

        #region Bootstrap render support methods
        protected override string? ClassBuilder()
        {
            return BlazorStrap.BootstrapVersion == BootstrapVersion.Bootstrap4 ? Version4ClassBuilder() : Version5ClassBuilder();
        }

        #region Bootstrap 4
        protected override string? Version4ClassBuilder()
        {
            return new CssBuilder("row")
            .AddClass($"mx-n{Gutters.ToIndex()} mx-n{Gutters.ToIndex()}", Gutters != Gutters.Default)
            .AddClass($"mx-n{HorizontalGutters.ToIndex()}", HorizontalGutters != Gutters.Default)
            .AddClass($"my-n{VerticalGutters.ToIndex()}", VerticalGutters != Gutters.Default)
            .AddClass($"justify-content-{Justify.NameToLower()}", Justify != Justify.Default && Justify != Justify.Evenly)
            .AddClass($"align-items-{Align.NameToLower()}", Align != Align.Default)
            .AddClass($"row-cols-{RowColumns}", RowColumns > 0)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
        }

        protected override void Version4RenderBuilder(RenderTreeBuilder builder)
        {
            var s = 0;
            builder.OpenComponent<CascadingValue<BSRow>>(0);
            builder.AddAttribute(1, "Value", this);
            builder.AddAttribute(2, "ChildContent", (RenderFragment)(builder2 =>
            {
                builder2.OpenElement(s, "div");
                builder2.AddAttribute(s++, "class", ClassBuilder());
                builder2.AddMultipleAttributes(s++, Attributes);
                builder2.AddContent(s++, ChildContent);
                builder2.CloseElement();
            }));

            builder.CloseComponent();
        }
        #endregion

        #region Bootstrap 5
        protected override string? Version5ClassBuilder()
        {
            return new CssBuilder("row")
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
        protected override void Version5RenderBuilder(RenderTreeBuilder builder)
        {
            var s = 0;
            builder.OpenElement(s, "div");
            builder.AddAttribute(s++, "class", ClassBuilder());
            builder.AddMultipleAttributes(s++, Attributes);
            builder.AddContent(s++, ChildContent);
            builder.CloseElement();
        }
        #endregion
        #endregion
    }
}