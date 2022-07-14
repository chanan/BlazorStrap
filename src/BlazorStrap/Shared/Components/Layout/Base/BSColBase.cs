using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Layout.Base
{
    public abstract class BSColBase : BlazorStrapBase
    {
        [Parameter] public Align Align { get; set; }
        [Parameter] public string? Column { get; set; }
        [Parameter] public string? ColumnXL { get; set; }
        [Parameter] public string? ColumnLarge { get; set; }
        [Parameter] public string? ColumnMedium { get; set; }
        [Parameter] public string? ColumnSmall { get; set; }
        [Parameter] public string? Offset { get; set; }
        [Parameter] public string? OffsetXL { get; set; }
        [Parameter] public string? OffsetLarge { get; set; }
        [Parameter] public string? OffsetMedium { get; set; }
        [Parameter] public string? OffsetSmall { get; set; }
        [Parameter] public string? Order { get; set; }
        [Parameter] public bool OrderFirst { get; set; }
        [Parameter] public string? OrderLarge { get; set; }
        [Parameter] public string? OrderXL { get; set; }
        [Parameter] public bool OrderLast { get; set; }
        [Parameter] public string? OrderMedium { get; set; }
        [Parameter] public string? OrderSmall { get; set; }

        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
    }
}