using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Extensions.DataComponents
{
    public partial class BSDataListGroup<TItem> : DataComponentBase<TItem>
    {
        // Pass through properties for the BSListGroup
        [Parameter] public ListGroupType ListGroupType { get; set; } = ListGroupType.List;
        [Parameter] public bool IsFlush { get; set; }
        [Parameter] public string Class { get; set; }
    }

}
