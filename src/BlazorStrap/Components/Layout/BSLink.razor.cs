using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSLink : BlazorStrapBase
    {
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public string? Target { get; set; }
        [Parameter] public string? Url { get; set; }

        private string? ClassBuilder => new CssBuilder("")
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private async Task ClickEvent()
        {
            if(!string.IsNullOrEmpty(Target))
                BlazorStrap.OnForwardClick(Target);
            await OnClick.InvokeAsync();
        }
    }
}
