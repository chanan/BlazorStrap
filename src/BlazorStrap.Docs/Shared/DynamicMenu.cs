using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap_Docs.Shared
{
    public class DynamicMenu : ComponentBase
    {
        [Parameter] public string Version { get; set; }
 
        public async Task RefreshAsync()
        {
            await InvokeAsync(StateHasChanged);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var type = Type.GetType($"BlazorStrap_Docs.Shared.{Version}.NavMenu");
            
            if (type != null)
            {
                builder.OpenComponent(0, type);
                builder.CloseComponent();
            }
        }
    }
}
