using Microsoft.AspNetCore.Components;

namespace BlazorStrap.Shared.Components.Modal
{
    public abstract class BSModalHeaderBase : BlazorStrapBase
    {
        [Parameter] public bool HasCloseButton { get; set; } = true;
        [Parameter] public string? ButtonClass { get; set; }
        [CascadingParameter] public BSModalBase? Parent { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        protected async Task ClickEvent()
        {
            if (Parent != null)
                await Parent.ToggleAsync();
        }
    }
}
