using BlazorComponentUtilities;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSOffCanvasHeader : LayoutBase
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