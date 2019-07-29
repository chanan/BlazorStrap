using Microsoft.AspNetCore.Components;
using BlazorStrap.Util.Components;
using BlazorStrap.Util;
using BlazorComponentUtilities;
using System;

namespace BlazorStrap
{
    public class CodeBSAlert : BootstrapComponentBase
    {
        protected string classname =>
       new CssBuilder().AddClass("alert")
           .AddClass($"alert-{Color.ToDescriptionString()}")
           .AddClass(Class)
       .Build();

        [Parameter] protected Color Color { get; set; } = Color.Primary;
        [Parameter] protected bool IsDismissible { get; set; }
        [Parameter] protected string Class { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets an action to be invoked when the alert is dismissed.
        /// </summary>
        [Parameter] protected EventCallback OnDismiss { get; set; }

        protected bool IsOpen { get; set; } = true;

        protected void onclick()
        {
            IsOpen = false;
            OnDismiss.InvokeAsync(EventCallback.Empty);
        }
    }
}
