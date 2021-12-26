using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSButton : BlazorStrapBase
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsActive { get; set; }
        [Parameter] public bool IsDisabled { get; set; }
        [Parameter] public bool IsLink { get; set; }
        [Parameter] public bool IsOutlined { get; set; }
        [Parameter] public bool IsReset { get; set; }
        [Parameter] public bool IsSubmit { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string? Target { get; set; }
        [Parameter] public string? Url { get; set; }

        private string? ClassBuilder => new CssBuilder("btn")
            .AddClass($"btn-outline-{Color.NameToLower()}", IsOutlined)
            .AddClass($"btn-{Color.NameToLower()}", Color != BSColor.Default && !IsOutlined)
            .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        private string? ButtonType()
        {
            if(IsSubmit) return "Submit";
            return IsReset ? "Reset" : "button";
        }

        private async Task ClickEvent()
        {
            if(!string.IsNullOrEmpty(Target))
                BlazorStrap.OnForwardClick(Target);
            await OnClick.InvokeAsync();
        }
    }
}
