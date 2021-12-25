using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSNavbarToggle : BlazorStrapBase
    {
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [CascadingParameter] public BSCollapse? CollapseParent { get; set; }

        private string? ClassBuilder => new CssBuilder("navbar-toggler")
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        public async Task ClickEvent()
        {   
            if (CollapseParent != null)
            {
                await CollapseParent.ToggleAsync();
            }
            await OnClick.InvokeAsync();
        }
    }
}