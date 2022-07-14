using BlazorComponentUtilities;
using BlazorStrap.Bootstrap.V5_1.Enums;
using BlazorStrap.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSListGroupItem : LayoutBase
    {
        /// <summary>
        /// Sets color of item.
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Renders as button
        /// </summary>
        [Parameter] public bool IsButton { get; set; }

        /// <summary>
        /// Event called when item is clicked.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        /// <summary>
        /// Prevents default event from firing when clicked.
        /// </summary>
        [Parameter] public bool PreventDefault { get; set; }

        [Obsolete("Parameter no longer used.")]
        [Parameter]
        public Size Size { get; set; } = Size.None;

        /// <summary>
        /// Url to navigate to if link.
        /// </summary>
        [Parameter] public string? Url { get; set; }

        private string? ClassBuilder => new CssBuilder("list-group-item")
          .AddClass($"list-group-item-action", !string.IsNullOrEmpty(Url) || IsButton)
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