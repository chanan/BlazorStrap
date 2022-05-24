using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSCloseButton : BlazorStrapBase
    {
        /// <summary>
        /// Whether or not the button is disabled.
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// Adds the btn-close-white class.
        /// </summary>
        [Parameter] public bool IsWhite { get; set; }

        /// <summary>
        /// Event called when button is clicked.
        /// </summary>
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        private string? ClassBuilder => new CssBuilder("btn-close")
         .AddClass("btn-close-white", IsWhite)
         .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
         .AddClass(Class, !string.IsNullOrEmpty(Class))
         .Build().ToNullString();

        private async Task ClickEventAsync()
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync();
            }
        }
    }
}