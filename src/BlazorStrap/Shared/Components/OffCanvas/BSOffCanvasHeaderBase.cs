using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.OffCanvas
{
    public abstract class BSOffCanvasHeaderBase : BlazorStrapBase
    {
        [Parameter] public string? ButtonClass { get; set; }
        [CascadingParameter] public BSOffCanvasBase? Parent { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        protected async Task ClickEvent()
        {
            if (Parent != null)
                await Parent.ToggleAsync();
        }
    }
}
