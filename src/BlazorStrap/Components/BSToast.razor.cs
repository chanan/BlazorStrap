using Microsoft.AspNetCore.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public class CodeBSToast : BootstrapComponentBase
    {
        protected string classname =>
        new CssBuilder("toast fade")
            .AddClass("show", IsVisible && !IsDismissed)
            .AddClass("hide", !IsVisible || IsDismissed)
            .AddClass(Class)
        .Build();

        [Parameter] protected string Class { get; set; }
        [Parameter] protected bool IsVisible { get; set; }
        [Parameter] protected RenderFragment BSToastHeader { get; set; }
        [Parameter] protected RenderFragment BSToastBody { get; set; }

        /// <summary>
        /// Gets or sets an action to be invoked when the alert is dismissed.
        ///
        ///</summary>
        [Parameter] protected EventCallback OnDismiss { get; set; }

        private bool IsDismissed { get; set; } = false;

        protected async Task onclick()
        {
            IsDismissed = true;
            await OnDismiss.InvokeAsync(null);
            StateHasChanged();
        }
    }
}
