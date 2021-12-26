using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Extensions.TreeView
{
    public partial class BSTree : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool Expand { get; set; }
        [Parameter] public bool DoubleClickToOpen { get; set; }
    }
}
