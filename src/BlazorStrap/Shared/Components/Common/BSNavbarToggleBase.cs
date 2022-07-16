using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSNavbarToggleBase : BlazorStrapBase
    {
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public string? Target { get; set; }
        [CascadingParameter] public BSCollapseBase? CollapseParent { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }

        protected async Task ClickEvent()
        {
            if (!string.IsNullOrEmpty(Target))
                BlazorStrapService.ForwardClick(Target);
            if (CollapseParent != null)
                await CollapseParent.ToggleAsync();
            await OnClick.InvokeAsync();
        }
    }
}
