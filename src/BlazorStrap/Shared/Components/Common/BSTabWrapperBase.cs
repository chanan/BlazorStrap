using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Common
{
    public class BSTabWrapperBase : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        public BSNavBase? Nav { get; set; }
    }
}