using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSOffCanvasHeader : BlazorStrapBase
    {
        [Parameter] public string? ButtonClass { get; set; }
        [CascadingParameter] public BSOffCanvas? Parent { get; set; }
        private string? ClassBuilder => new CssBuilder("offcanvas-header")
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private async Task ClickEvent()
        {
            if (Parent != null)
                await Parent.ToggleAsync();
        }
    }
}