using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5
{
    public partial class BSLabel : BSLabelBase<Size>
    {
        [Parameter] public string? ColumnXXL { get; set; }
        [Parameter] public string? OffsetXXL { get; set; }
        [Parameter] public string? OrderXXL { get; set; }
        protected override string? LayoutClass => LayoutClassBuilder.Build(this);

        protected override string? ClassBuilder => new CssBuilder()
                .AddClass("visually-hidden", IsHidden)
                .AddClass("form-label", !IsColumn && !IsRadioLabel && !IsCheckLabel && !IsFloating)
                .AddClass("col-form-label", IsColumn && !IsRadioLabel && !IsCheckLabel)
                .AddClass("form-check-label", (IsCheckLabel || IsRadioLabel))
                .AddClass("col", Column == null && ColumnSmall == null && ColumnMedium == null && ColumnLarge == null && ColumnXL == null && ColumnXXL == null && IsColumn)
                .AddClass($"col-{Column}", Column.VaildGridSize() && IsColumn)
                .AddClass($"col-sm-{ColumnSmall}", ColumnSmall.VaildGridSize() && IsColumn)
                .AddClass($"col-md-{ColumnMedium}", ColumnMedium.VaildGridSize() && IsColumn)
                .AddClass($"col-lg-{ColumnLarge}", ColumnLarge.VaildGridSize() && IsColumn)
                .AddClass($"col-xl-{ColumnXL}", ColumnXL.VaildGridSize() && IsColumn)
                .AddClass($"col-xxl-{ColumnXXL}", ColumnXXL.VaildGridSize() && IsColumn)
                .AddClass($"order-{Order}", Order.VaildGridSize())
                .AddClass($"order-sm-{OrderSmall}", OrderSmall.VaildGridSize() && IsColumn)
                .AddClass($"order-md-{OrderMedium}", OrderMedium.VaildGridSize() && IsColumn)
                .AddClass($"order-lg-{OrderLarge}", OrderLarge.VaildGridSize() && IsColumn)
                .AddClass($"order-xl-{OrderXL}", OrderXL.VaildGridSize() && IsColumn)
                .AddClass($"order-xxl-{OrderXXL}", OrderXXL.VaildGridSize() && IsColumn)
                .AddClass("order-first", OrderFirst && IsColumn)
                .AddClass("order-last", OrderLast && IsColumn)
                .AddClass($"offset-{Offset}", Offset.VaildGridSize() && IsColumn)
                .AddClass($"offset-sm-{OffsetSmall}", OffsetSmall.VaildGridSize() && IsColumn)
                .AddClass($"offset-md-{OffsetMedium}", OffsetMedium.VaildGridSize() && IsColumn)
                .AddClass($"offset-lg-{OffsetLarge}", OffsetLarge.VaildGridSize() && IsColumn)
                .AddClass($"offset-xl-{OffsetXL}", OffsetXL.VaildGridSize() && IsColumn)
                .AddClass($"offset-xxl-{OffsetXXL}", OffsetXXL.VaildGridSize() && IsColumn)
                .AddClass($"align-self-{Align.NameToLower()}", Align != Align.Default)
                .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
                .AddClass(Class, !string.IsNullOrEmpty(Class))
                .Build().ToNullString();
    }
}
