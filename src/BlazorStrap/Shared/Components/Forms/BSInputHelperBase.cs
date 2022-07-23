using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Forms
{
    public abstract class BSInputHelperBase : ComponentBase
    {
        [Parameter] public bool IsCustom { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public string Id { get; set; } = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).ToString().Replace("==.","");
    }
}
