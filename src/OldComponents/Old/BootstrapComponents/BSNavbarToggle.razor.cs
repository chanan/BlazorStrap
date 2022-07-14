using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSNavbarToggle : LayoutBase
    {
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public string? Target { get; set; }
        [CascadingParameter] public BSCollapse? CollapseParent { get; set; }

        private string? ClassBuilder => new CssBuilder("navbar-toggler")
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private async Task ClickEvent()
        {   
            if(!string.IsNullOrEmpty(Target))
                BlazorStrapService.ForwardClick(Target);
            if (CollapseParent != null)
                await CollapseParent.ToggleAsync();
            await OnClick.InvokeAsync();
        }
    }
}