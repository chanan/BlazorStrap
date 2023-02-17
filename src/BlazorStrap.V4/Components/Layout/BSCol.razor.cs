using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Layout;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V4
{
    public partial class BSCol : BSColBase
    {
        [CascadingParameter] public BSRow? BSRow { get; set; }

        protected override string? LayoutClass => LayoutClassBuilder.Build(this);
        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("col-auto", Auto)
                .AddClass("col", !Auto && Column == null && ColumnSmall == null && ColumnMedium == null && ColumnLarge == null && ColumnXL == null)
                .AddClass($"px-{BSRow?.Gutters.ToIndex()} py-{BSRow?.Gutters.ToIndex()}", BSRow?.Gutters != Gutters.Default)
                .AddClass($"px-{BSRow?.HorizontalGutters.ToIndex()}", BSRow?.HorizontalGutters != Gutters.Default)
                .AddClass($"py-{BSRow?.VerticalGutters.ToIndex()}", BSRow?.VerticalGutters != Gutters.Default)
                .AddClass($"col-{Column}", Column.VaildGridSize())
                .AddClass($"col-sm-{ColumnSmall}", ColumnSmall.VaildGridSize())
                .AddClass($"col-md-{ColumnMedium}", ColumnMedium.VaildGridSize())
                .AddClass($"col-lg-{ColumnLarge}", ColumnLarge.VaildGridSize())
                .AddClass($"col-xl-{ColumnXL}", ColumnXL.VaildGridSize())
                .AddClass($"order-{Order}", Order.VaildGridSize())
                .AddClass($"order-sm-{OrderSmall}", OrderSmall.VaildGridSize())
                .AddClass($"order-md-{OrderMedium}", OrderMedium.VaildGridSize())
                .AddClass($"order-lg-{OrderLarge}", OrderLarge.VaildGridSize())
                .AddClass($"order-xl-{OrderXL}", OrderXL.VaildGridSize())
                .AddClass("order-first", OrderFirst)
                .AddClass("order-last", OrderLast)
                .AddClass($"offset-{Offset}", Offset.VaildGridSize())
                .AddClass($"offset-sm-{OffsetSmall}", OffsetSmall.VaildGridSize())
                .AddClass($"offset-md-{OffsetMedium}", OffsetMedium.VaildGridSize())
                .AddClass($"offset-lg-{OffsetLarge}", OffsetLarge.VaildGridSize())
                .AddClass($"offset-xl-{OffsetXL}", OffsetXL.VaildGridSize())
                .AddClass($"align-self-{Align.NameToLower()}", Align != Align.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

    }
}
