using Microsoft.AspNetCore.Components;

namespace BlazorStrap.InternalComponents
{
    public partial class TabContentRender : ComponentBase
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        internal void Render()
        {
            StateHasChanged();
        }
    }
}