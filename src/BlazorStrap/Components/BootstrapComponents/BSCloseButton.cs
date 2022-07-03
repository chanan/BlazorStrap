using BlazorStrap.Bootstrap.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap
{
    public partial class BSCloseButton : BlazorStrapBase, ICloseButton
    {
        private ICloseButton? _reference;
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

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, this.BuildRenderFragment("CloseButton", (int)BlazorStrap.bootStrapVersion, (c) => _reference = (ICloseButton) c));
        }
    }
}