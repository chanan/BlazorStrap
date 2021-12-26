using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSLabel : BlazorStrapBase
    {
        [Parameter] public Size Size { get; set; }
        
        [Parameter] public Align Align { get; set; }
        [Parameter] public string? Column { get; set; }
        [Parameter] public string? ColumnXL { get; set; }
        [Parameter] public string? ColumnXXL { get; set; }
        [Parameter] public string? ColumnLarge { get; set; }
        [Parameter] public string? ColumnMedium { get; set; }
        [Parameter] public string? ColumnSmall { get; set; }
        [Parameter] public string? Offset { get; set; }
        [Parameter] public string? OffsetXXL { get; set; }
        [Parameter] public string? OffsetXL { get; set; }
        [Parameter] public string? OffsetLarge { get; set; }
        [Parameter] public string? OffsetMedium { get; set; }
        [Parameter] public string? OffsetSmall { get; set; }
        [Parameter] public string? Order { get; set; }
        [Parameter] public bool OrderFirst { get; set; }
        [Parameter] public string? OrderLarge { get; set; }
        [Parameter] public string? OrderXL { get; set; }
        [Parameter] public string? OrderXXL { get; set; }
        [Parameter] public bool OrderLast { get; set; }
        [Parameter] public string? OrderMedium { get; set; }
        [Parameter] public string? OrderSmall { get; set; }
        [Parameter] public bool IsHidden { get; set; }
        [Parameter] public bool IsColumn { get; set; }
        [Parameter] public bool IsCheckLabel { get; set; }
        [Parameter] public bool IsRadioLabel { get; set; }
        [Parameter] public bool IsFloating { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("visually-hidden", IsHidden)
            .AddClass("form-label", !IsColumn && !IsRadioLabel && !IsCheckLabel && !IsFloating)
            .AddClass("col-form-label", IsColumn && !IsRadioLabel && !IsCheckLabel)
            .AddClass("form-check-label", (IsCheckLabel || IsRadioLabel) == true)
            .AddClass("col", Column == null && ColumnSmall == null && ColumnMedium == null && ColumnLarge == null && ColumnXL == null && ColumnXXL == null && IsColumn)
            .AddClass($"col-{Column}", Column.VaildGridSize() && IsColumn)
            .AddClass($"col-sm-{ColumnSmall}", ColumnSmall.VaildGridSize() && IsColumn)
            .AddClass($"col-md-{ColumnMedium}", ColumnSmall.VaildGridSize() && IsColumn)
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
            .AddClass($"align-self-{Align.GetName<Align>(Align).ToLower()}", Align != Align.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}