using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Shared.Components.Common
{
    public abstract class BSListGroupItemBase : BlazorStrapBase
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

        /// <summary>
        /// Url to navigate to if link.
        /// </summary>
        [Parameter] public string? Url { get; set; }
        protected abstract string? LayoutClass { get; }
        protected abstract string? ClassBuilder { get; }
        protected async Task ClickEvent()
        {
            if (OnClick.HasDelegate)
                await OnClick.InvokeAsync();
        }
    }
}
