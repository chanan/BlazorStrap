using BlazorStrap.Extensions;
using BlazorComponentUtilities;
using BlazorStrap.Shared.Components.Layout.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5_1
{
    public partial class BSCol : BSColBase
    {
        [Parameter] public string? ColumnXXL { get; set; }
        [Parameter] public string? OffsetXXL { get; set; }
        [Parameter] public string? OrderXXL { get; set; }
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("col", Column == null && ColumnSmall == null && ColumnMedium == null && ColumnLarge == null && ColumnXL == null && ColumnXXL == null)
                .AddClass($"col-{Column}", Column.VaildGridSize())
                .AddClass($"col-sm-{ColumnSmall}", ColumnSmall.VaildGridSize())
                .AddClass($"col-md-{ColumnMedium}", ColumnMedium.VaildGridSize())
                .AddClass($"col-lg-{ColumnLarge}", ColumnLarge.VaildGridSize())
                .AddClass($"col-xl-{ColumnXL}", ColumnXL.VaildGridSize())
                .AddClass($"col-xxl-{ColumnXXL}", ColumnXXL.VaildGridSize())
                .AddClass($"order-{Order}", Order.VaildGridSize())
                .AddClass($"order-sm-{OrderSmall}", OrderSmall.VaildGridSize())
                .AddClass($"order-md-{OrderMedium}", OrderMedium.VaildGridSize())
                .AddClass($"order-lg-{OrderLarge}", OrderLarge.VaildGridSize())
                .AddClass($"order-xl-{OrderXL}", OrderXL.VaildGridSize())
                .AddClass($"order-xxl-{OrderXXL}", OrderXXL.VaildGridSize())
                .AddClass("order-first", OrderFirst)
                .AddClass("order-last", OrderLast)
                .AddClass($"offset-{Offset}", Offset.VaildGridSize())
                .AddClass($"offset-sm-{OffsetSmall}", OffsetSmall.VaildGridSize())
                .AddClass($"offset-md-{OffsetMedium}", OffsetMedium.VaildGridSize())
                .AddClass($"offset-lg-{OffsetLarge}", OffsetLarge.VaildGridSize())
                .AddClass($"offset-xl-{OffsetXL}", OffsetXL.VaildGridSize())
                .AddClass($"offset-xxl-{OffsetXXL}", OffsetXXL.VaildGridSize())
                .AddClass($"align-self-{Align.NameToLower()}", Align != Align.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();

        
    }
}
