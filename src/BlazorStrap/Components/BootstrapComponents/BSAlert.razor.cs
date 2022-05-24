using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap
{
    public partial class BSAlert : BlazorStrapBase
    {
        /// <summary>
        /// Color class of alert
        /// </summary>
        [Parameter] public BSColor Color { get; set; } = BSColor.Default;

        /// <summary>
        /// Alert body content
        /// </summary>
        [Parameter] public RenderFragment? Content { get; set; }

        /// <summary>
        /// Event triggered when alert is dismissed. Only called when <see cref="IsDismissible"/> is true
        /// </summary>
        [Parameter] public EventCallback Dismissed { get; set; }

        /// <summary>
        /// Sets whether or not an icon is shown
        /// </summary>
        [Parameter] public bool HasIcon { get; set; }

        /// <summary>
        /// Alert header content (optional)
        /// </summary>
        [Parameter] public RenderFragment? Header { get; set; }

        /// <summary>
        /// Heading size. Valid values are 1-6
        /// </summary>
        [Parameter] public int Heading { get; set; } = 1;

        /// <summary>
        /// Determines whether or not an alter is dismissible. See <see cref="Dismissed"/> for the callback
        /// </summary>
        [Parameter] public bool IsDismissible { get; set; }

        private bool _dismissed;

        private string? ClassBuilder => new CssBuilder("alert")
            .AddClass($"alert-{Color.NameToLower()}", Color != BSColor.Default)
            .AddClass("d-flex align-items-center", HasIcon)
            .AddClass("alert-dismissible", IsDismissible)
            .AddClass(LayoutClass, !string.IsNullOrEmpty(LayoutClass))
            .AddClass(Class, !string.IsNullOrEmpty(Class))
            .Build().ToNullString();

        /// <summary>
        /// Dismisses the alert
        /// </summary>
        /// <returns><see cref="Task"/></returns>
        public async Task CloseEventAsync()
        {
            await Dismissed.InvokeAsync();
            _dismissed = true;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Opens the alert
        /// </summary>
        public void Open()
        {
            _dismissed = false;
        }
    }
}
