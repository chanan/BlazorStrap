using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSListGroupItem : BlazorStrapBase
    {
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;
        [Parameter] public bool IsButton { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public bool PreventDefault { get; set; }
        [Parameter] public Size Size { get; set; } = Size.None;
        [Parameter] public string? Url { get; set; }

        private string? ClassBuilder => new CssBuilder("list-group-item")
          .AddClass($"list-group-item-action", !string.IsNullOrEmpty(Url) || IsButton )
          .AddClass($"list-group-item-{Color.NameToLower()}", Color != BSColor.Default)
          .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
          .AddClass(Class, !string.IsNullOrEmpty(Class))
          .Build().ToNullString();

        private async Task ClickEvent()
        {
            if (OnClick.HasDelegate)
                await OnClick.InvokeAsync();
        }
    }
}