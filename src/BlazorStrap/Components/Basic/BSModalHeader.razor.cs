using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSModalHeader : BlazorStrapBase
    {
        [Parameter] public bool HasCloseButton { get; set; } = true;
        [Parameter] public string? ButtonClass { get; set; }
        [CascadingParameter] public BSModal? Parent { get;set;}
        private string? ClassBuilder => new CssBuilder("modal-header")
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private async Task ClickEvent()
        {
            if(Parent != null)
                await Parent.ToggleAsync();
        }
    }
}