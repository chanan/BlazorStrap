using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSCloseButton : BlazorStrapBase
    {
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsWhite { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        private string? ClassBuilder => new CssBuilder("btn-close")
         .AddClass("btn-close-white", IsWhite)
         .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
         .AddClass(Class, !string.IsNullOrEmpty(Class))
         .Build().ToNullString();

        private async Task ClickEventAsync()
        {
            if(OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync();
            }
        }
    }
}