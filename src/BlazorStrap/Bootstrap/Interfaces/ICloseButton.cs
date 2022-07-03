using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorStrap.Bootstrap.Interfaces
{
    internal interface ICloseButton
    {
        /// <summary>
        /// Whether or not the button is disabled.
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Adds the btn-close-white class.
        /// </summary>
        public bool IsWhite { get; set; }

        /// <summary>
        /// Event called when button is clicked.
        /// </summary>
        public EventCallback<MouseEventArgs> OnClick { get; set; }
    }
}
