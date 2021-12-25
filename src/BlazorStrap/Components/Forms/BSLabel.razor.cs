using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSLabel : BlazorStrapBase
    {
        [Parameter] public Align Align { get; set; }
        [Parameter] public bool Auto { get; set; }
        [Parameter] public int Column { get; set; }
        [Parameter] public int ColumnLarge { get; set; }
        [Parameter] public int ColumnMedium { get; set; }
        [Parameter] public int ColumnSmall { get; set; }
        [Parameter] public int ColumnXL { get; set; }
        [Parameter] public int ColumnXXL { get; set; }
        [Parameter] public bool IsCheckLabel { get; set; }
        [Parameter] public bool IsColumn { get; set; }
        [Parameter] public bool IsRadioLabel { get; set; }
        [Parameter] public int Offset { get; set; } = -1;
        [Parameter] public int OffsetLarge { get; set; } = -1;
        [Parameter] public int OffsetMedium { get; set; } = -1;
        [Parameter] public int OffsetSmall { get; set; } = -1;
        [Parameter] public int OffsetXL { get; set; } = -1;
        [Parameter] public int OffsetXXL { get; set; } = -1;
        [Parameter] public int Order { get; set; }
        [Parameter] public bool OrderFirst { get; set; }
        [Parameter] public int OrderLarge { get; set; }
        [Parameter] public bool OrderLast { get; set; }
        [Parameter] public int OrderMedium { get; set; }
        [Parameter] public int OrderSmall { get; set; }
        [Parameter] public int OrderXL { get; set; }
        [Parameter] public int OrderXXL { get; set; }

        private string? ClassBuilder => new CssBuilder()
            .AddClass("form-label", !IsColumn && !IsRadioLabel && !IsCheckLabel)
            .AddClass("col-form-label", IsColumn && !IsRadioLabel && !IsCheckLabel)
            .AddClass("form-check-label", !IsCheckLabel || !IsRadioLabel)
            .AddClass("col-auto", Auto)
            .AddClass($"col-{Column}", Column > 0 && Column < 13)
            .AddClass($"col-sm-{ColumnSmall}", ColumnSmall > 0 && ColumnSmall < 13)
            .AddClass($"col-md-{ColumnMedium}", ColumnMedium > 0 && ColumnMedium < 13)
            .AddClass($"col-lg-{ColumnLarge}", ColumnLarge > 0 && ColumnLarge < 13)
            .AddClass($"col-xl-{ColumnXL}", ColumnXL > 0 && ColumnXL < 13)
            .AddClass($"col-xxl-{ColumnXXL}", ColumnXXL > 0 && ColumnXXL < 13)
            .AddClass($"order-{Order}", Order > 0 && Order < 13)
            .AddClass($"order-sm-{OrderSmall}", OrderSmall > 0 && OrderSmall < 13)
            .AddClass($"order-md-{OrderMedium}", OrderMedium > 0 && OrderMedium < 13)
            .AddClass($"order-lg-{OrderLarge}", OrderLarge > 0 && OrderLarge < 13)
            .AddClass($"order-xl-{OrderXL}", OrderXL > 0 && OrderXL < 13)
            .AddClass($"order-xxl-{OrderXXL}", OrderXXL > 0 && OrderXXL < 13)
            .AddClass($"order-first", OrderFirst)
            .AddClass($"order-last", OrderLast)
            .AddClass($"offset-{Offset}", Offset > -1 && Offset < 13)
            .AddClass($"offset-sm-{OffsetSmall}", OffsetSmall > -1 && OffsetSmall < 13)
            .AddClass($"offset-md-{OffsetMedium}", OffsetMedium > -1 && OffsetMedium < 13)
            .AddClass($"offset-lg-{OffsetLarge}", OffsetLarge > -1 && OffsetLarge < 13)
            .AddClass($"offset-xl-{OffsetXL}", OffsetXL > -1 && OffsetXL < 13)
            .AddClass($"offset-xxl-{OffsetXXL}", OffsetXXL > -1 && OffsetXXL < 13)
            .AddClass($"align-self-{Align.GetName<Align>(Align).ToLower()}", Align != Align.Default)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();
    }
}